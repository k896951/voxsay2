﻿using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using voxsay2.AivisSpeech;
using voxsay2.common;

namespace voxsay2
{
    internal class Program
    {
        static int Main(string[] args)
        {
            int rcd = 0;
            Opts opt = new Opts(args);


            if (!opt.IsSafe) return 8;

            // 稼働している音声合成製品一覧表示（標準指定）
            if (opt.IsRequestActiveProductList)
            {
                foreach (var item in ApiProxy.GetConnectivityList())
                {
                    Console.WriteLine(string.Format(@"product: {0}", item.ToString()));
                }

                return 0;
            }

            // 知らない製品が指定された
            if ((opt.SpecifiedProduct is null)||(!ApiProxy.IsValidProduct(opt.SpecifiedProduct)))
            {
                Console.WriteLine(String.Format(@"Error: Unknown product {0}", opt.SpecifiedProduct));
                return 8;
            }

            var soundsetting = opt.SoundSetting;
            ApiProxy api = new ApiProxy(opt.SpecifiedProduct, opt.SpecifiedHost, opt.SpecifiedPort, ref soundsetting);


            // 製品への接続性確認
            if (!api.CheckConnectivity())
            {
                Console.WriteLine(String.Format(@"Error: Unable to connect to {0}", opt.SpecifiedProduct));
                return 8;
            }

            // 話者一覧表示
            if (opt.IsRequestSpeakerList)
            {
                switch (opt.RenderingMode)
                {
                    case "sing":
                        foreach (var item in api.GetAvailableSingers())
                        {
                            Console.WriteLine(string.Format(@"index: {0},  speaker:{1}", item.Key, item.Value));
                        }
                        break;

                    case "talk":
                    default:
                        foreach (var item in api.GetAvailableCasts())
                        {
                            Console.WriteLine(string.Format(@"index: {0},  speaker:{1}", item.Key, item.Value));
                        }
                        break;
                }

                return 0;
            }

            // SingTeacher一覧表示
            if (opt.IsRequestSingTeacherList)
            {
                foreach (var item in api.GetAvailableSingTeachers())
                {
                    Console.WriteLine(string.Format(@"index: {0},  speaker:{1}", item.Key, item.Value));
                }

                return 0;
            }

            // 発声もしくは保存処理
            if (opt.Index != null)
            {
                SpeakerParams pm = CopyParamsFromOpts(ref api, ref opt);

                if (opt.RenderingMode == "sing")
                {
                    // 歌唱時
                    try
                    {
                        rcd = SingProcessing(ref api, ref opt, ref pm);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format(@"sing error: {0}", e.Message));
                    }
                }

                if (opt.RenderingMode == "talk")
                {
                    // トーク時
                    try
                    {
                        rcd = TalkProcessing(ref api, ref opt, ref pm);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format(@"talk error: {0}", e.Message));
                    }
                }

            }

            return rcd;
        }

        private static SpeakerParams CopyParamsFromOpts(ref ApiProxy api, ref Opts opt)
        {
            SpeakerParams pm = api.GetAvatorParams();
            if (opt.SpeedScale != null) pm.speedScale = (double)opt.SpeedScale;
            if (opt.PitchScale != null) pm.pitchScale = (double)opt.PitchScale;
            if (opt.VolumeScale != null) pm.volumeScale = (double)opt.VolumeScale;
            if (opt.IntonationScale != null) pm.intonationScale = (double)opt.IntonationScale;
            if (opt.PrePhonemeLength != null) pm.prePhonemeLength = (double)opt.PrePhonemeLength;
            if (opt.PostPhonemeLength != null) pm.postPhonemeLength = (double)opt.PostPhonemeLength;
            if (opt.OutputSamplingRate != null) pm.outputSamplingRate = (int)opt.OutputSamplingRate;

            if (opt.SpecifiedProduct == "aivisspeech")
            {
                if (opt.TempoDynamicsScale != null) ((AivisSpeechParams)pm).tempodynamicsScale = (double)opt.TempoDynamicsScale;
                if (opt.PauselengthScale != null) ((AivisSpeechParams)pm).pauselengthScale = (double)opt.PauselengthScale;
            }

            return pm;
        }

        private static string GenFilename(string filename)
        {
            Regex ext = new Regex(@"\.[wW][aA][vV][eE]{0,1}$");

            return !ext.IsMatch(filename) ? String.Format(@"{0}.wav", filename) : filename;
        }

        private static string  GenFilename(string filename, int linecounter)
        {
            Regex ext = new Regex(@"\.[wW][aA][vV][eE]{0,1}$");

            string f = ext.IsMatch(filename) ? ext.Replace(filename, "") : filename;

            return string.Format(@"{0}_{1:D8}.wav", f, linecounter);
        }

        private static int TalkProcessing(ref ApiProxy api, ref Opts opt, ref SpeakerParams pm)
        {
            int rcd = 0;

            if (("" + opt.Inputfilename) == "")
            {
                if ((opt.SaveFile != null)&&(opt.SaveFile != ""))
                {
                    if (!api.Save((int)opt.Index, pm, opt.TalkText, GenFilename(opt.SaveFile))) rcd = 8;
                }
                else
                {
                    if (!api.Speak((int)opt.Index, pm, opt.TalkText)) rcd = 8;
                }
            }
            else
            {
                Stream fp;

                if ("-" == opt.Inputfilename)
                {
                    fp = Console.OpenStandardInput();
                }
                else
                {
                    fp = new FileStream(opt.Inputfilename, FileMode.Open, FileAccess.Read, FileShare.Read);
                }

                using (StreamReader sr = new StreamReader(fp))
                {
                    switch (opt.TalkWaveGenType)
                    {
                        case TalkWavGenTypeEnum.allline:
                            StringBuilder sb = new StringBuilder();
                            sb.Clear();

                            while (!sr.EndOfStream)
                            {
                                sb.AppendLine(sr.ReadLine());
                            }

                            if ((opt.SaveFile != null) && (opt.SaveFile != ""))
                            {
                                if (!api.Save((int)opt.Index, pm, sb.ToString(), GenFilename(opt.SaveFile))) rcd = 8;
                            }
                            else
                            {
                                if (!api.Speak((int)opt.Index, pm, sb.ToString())) rcd = 8;
                            }

                            break;

                        case TalkWavGenTypeEnum.splitline:
                            int fileTailNumber = 1;

                            while (!sr.EndOfStream)
                            {
                                if ((opt.SaveFile != null) && (opt.SaveFile != ""))
                                {
                                    if (!api.Save((int)opt.Index, pm, sr.ReadLine(), GenFilename(opt.SaveFile, fileTailNumber))) rcd = 8;
                                }
                                else
                                {
                                    if (!api.Speak((int)opt.Index, pm, sr.ReadLine())) rcd = 8;
                                }

                                fileTailNumber++;
                            }

                            break;
                    }

                    sr?.Close();
                }

                fp?.Close();
            }

            return rcd;

        }

        private static int SingProcessing(ref ApiProxy api, ref Opts opt, ref SpeakerParams pm)
        {
            var mmlObj = new VoiceVoxNoteGenerator();
            string scorefilename = @".\MyScore.json";
            int rcd = 0;

            if (("" + opt.Inputfilename) == "")
            {
                var parseInfo = mmlObj.ParseSingString(opt.TalkText);

                var notes = mmlObj.ConvertScoreInfo(parseInfo);

                if (opt.ExportNote) File.WriteAllText(scorefilename, mmlObj.ExportNotes(notes));
                if (opt.PrintNote) mmlObj.PrintAssignInfo(notes);

                if ((opt.SaveFile != null) && (opt.SaveFile != ""))
                {
                    if (!api.SaveSong((int)opt.Index, (int)opt.TeacherIndex, pm, notes, GenFilename(opt.SaveFile))) rcd = 8;
                }
                else
                {
                    if (!api.Sing((int)opt.Index, (int)opt.TeacherIndex, pm, notes)) rcd = 8;
                }
            }
            else
            {
                var parseInfoList = mmlObj.ParseSingFile(opt.Inputfilename);

                switch (opt.SingWaveGenType)
                {
                    case SingWavGenTypeEnum.allnote:
                        var notes = mmlObj.ConvertScoreInfo(parseInfoList);

                        if (opt.ExportNote) File.WriteAllText(scorefilename, mmlObj.ExportNotes(notes));
                        if (opt.PrintNote) mmlObj.PrintAssignInfo(notes);

                        if ((opt.SaveFile != null) && (opt.SaveFile != ""))
                        {
                            if (!api.SaveSong((int)opt.Index, (int)opt.TeacherIndex, pm, notes, GenFilename(opt.SaveFile))) rcd = 8;
                        }
                        else
                        {
                            if (!api.Sing((int)opt.Index, (int)opt.TeacherIndex, pm, notes)) rcd = 8;
                        }

                        break;

                    case SingWavGenTypeEnum.splitnote:
                        int fileTailNumber = 1;
                        foreach (var noteItem in parseInfoList)
                        {
                            var note = mmlObj.ConvertScoreInfo(noteItem);

                            if (note.Notes.Count > 1)
                            {
                                if (opt.PrintNote) mmlObj.PrintAssignInfo(note, fileTailNumber);

                                if ((opt.SaveFile != null) && (opt.SaveFile != ""))
                                {
                                    if (!api.SaveSong((int)opt.Index, (int)opt.TeacherIndex, pm, note, GenFilename(opt.SaveFile, fileTailNumber))) rcd = 8;
                                }
                                else
                                {
                                    if (!api.Sing((int)opt.Index, (int)opt.TeacherIndex, pm, note)) rcd = 8;
                                }
                            }

                            fileTailNumber++;
                        }

                        break;
                }

            }

            return rcd;
        }
    }
}
