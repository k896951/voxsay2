using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;


namespace voxsay2
{
    internal class Opts
    {
        public bool IsSafe { get; private set; } = false;
        public bool IsRequestSpeakerList { get; private set; } = false;
        public bool IsRequestActiveProductList { get; private set; } = false;
        public bool IsRequestSingTeacherList { get; private set; } = false;
        public bool ExportNote { get; private set; } = false;
        public bool PrintNote { get; private set; } = false;

        public string? SpecifiedProduct { get; private set; } = "voicevox";
        public string? SpecifiedHost { get; private set; }
        public int? SpecifiedPort { get; private set; }

        public double? SpeedScale { get; private set; }
        public double? PitchScale { get; private set; }
        public double? IntonationScale { get; private set; }
        public double? VolumeScale { get; private set; }
        public double? PrePhonemeLength { get; private set; }
        public double? PostPhonemeLength { get; private set; }
        public int? OutputSamplingRate { get; set; }
        public int? Index { get; private set; }
        public string? RenderingMode { get; private set; } = "talk";
        public int? TeacherIndex { get; private set; } = 6000;

        public string? TalkText { get; private set; }
        public string? SaveFile { get; private set; }

        public string? Inputfilename { get; private set; }
        public SingWavGenTypeEnum SingWaveGenType { get; private set; } = SingWavGenTypeEnum.allnote;
        public TalkWavGenTypeEnum TalkWaveGenType { get; private set; } = TalkWavGenTypeEnum.allline;

        public SoundSettings SoundSetting { get; private set; }

        private string ConfFileNamee = @".\voxsay2conf.json";
        private string VersionStr = "2.0.1";

        public Opts(string[] args)
        {
            bool tonly = false;

            if (args.Length == 0)
            {
                Help();
                return;
            }

            ConfigLoad();

            IsSafe = true;

            for(int i=0; i<args.Length; i++)
            {
                if(tonly)
                {
                    TalkText += args[i];
                    continue;
                }

                switch (args[i].ToLower())
                {
                    case "-t":
                        tonly = true;
                        break;

                    case "-prod":
                        if (i + 1 >= args.Length)
                        {
                            Console.WriteLine(@"Error: Need Product.");
                            IsSafe = false;
                            break;
                        }

                        SpecifiedProduct = args[i + 1].ToLower();
                        i++;

                        break;

                    case "-host":
                    case "-port":
                        if (SpecifiedProduct == "")
                        {
                            Console.WriteLine(@"Error: -prod option not specified.");
                            IsSafe = false;
                            break;
                        }
                        if (i + 1 >= args.Length)
                        {
                            Console.WriteLine(@"Error: Need Hostname or Portnumber.");
                            IsSafe = false;
                            break;
                        }

                        switch (args[i])
                        {
                            case "-host":
                                SpecifiedHost = args[i + 1];
                                break;

                            case "-port":
                                if (int.TryParse(args[i + 1], out int port))
                                {
                                    SpecifiedPort = port;
                                }
                                else
                                {
                                    Console.WriteLine(@"Error: Incorrect port number.");
                                    IsSafe = false;
                                    break;
                                }
                                break;
                        }
                        i++;
                        break;

                    case "-list":
                        IsRequestSpeakerList = true;
                        break;

                    case "-prodlist":
                        IsRequestActiveProductList = true;
                        i = args.Length;
                        break;

                    case "-teacherlist":
                        IsRequestSingTeacherList = true;
                        i = args.Length;
                        break;

                    case "-save":
                        if (i + 1 >= args.Length)
                        {
                            Console.WriteLine(@"Error: Incorrect save specification.");
                            IsSafe = false;
                            break;
                        }

                        SaveFile = args[i + 1];
                        i++;
                        break;

                    case "-mf":
                        if (i + 1 >= args.Length)
                        {
                            Console.WriteLine(@"Error: Incorrect mf specification.");
                            IsSafe = false;
                            break;
                        }

                        Inputfilename = args[i + 1];
                        SingWaveGenType = SingWavGenTypeEnum.allnote;
                        TalkWaveGenType = TalkWavGenTypeEnum.allline;
                        i++;
                        break;

                    case "-sf":
                        if (i + 1 >= args.Length)
                        {
                            Console.WriteLine(@"Error: Incorrect sf specification.");
                            IsSafe = false;
                            break;
                        }

                        Inputfilename = args[i + 1];
                        SingWaveGenType = SingWavGenTypeEnum.splitnote;
                        TalkWaveGenType = TalkWavGenTypeEnum.splitline;
                        i++;
                        break;


                    case "-renderingmode":
                        if (i + 1 >= args.Length)
                        {
                            Console.WriteLine(@"Error: Incorrect renderingmode specification.");
                            IsSafe = false;
                            break;
                        }
                        {
                            var mode = args[i + 1].ToLower();
                            if ((mode != "talk") && (mode != "sing"))
                            {
                                Console.WriteLine(@"Error: unknown renderingmode specification.");
                                IsSafe = false;
                                break;
                            }
                        }
                        RenderingMode = args[i + 1];  // "sing" or "talk" 
                        i++;
                        break;

                    case "-teacherindex":
                    case "-ti":
                        if (i + 1 < args.Length)
                        {
                            int result;
                            Index = null;
                            if (int.TryParse(args[i + 1], out result))
                            {
                                TeacherIndex = result;
                            }
                            else
                            {
                                Console.WriteLine(@"Error: Invalid teacherindex specification.");
                                IsSafe = false;
                            }
                            i++;
                        }
                        else
                        {
                            Console.WriteLine(@"Error: Incorrect teacherindex specification.");
                            IsSafe = false;
                        }
                        break;

                    case "-exportnote":
                        ExportNote = true;
                        break;

                    case "-printnote":
                        PrintNote = true;
                        break;

                    case "-samplingrate":
                    case "-rsr":
                        if (i + 1 < args.Length)
                        {
                            int result;
                            OutputSamplingRate = null;
                            if (int.TryParse(args[i + 1], out result))
                            {
                                OutputSamplingRate = result;
                            }
                            else
                            {
                                Console.WriteLine(@"Error: Invalid samplingrate specification.");
                                IsSafe = false;
                            }
                            i++;
                        }
                        else
                        {
                            Console.WriteLine(@"Error: Incorrect samplingrate specification.");
                            IsSafe = false;
                        }
                        break;

                    case "-index":
                    case "-i":
                        if (i + 1 < args.Length)
                        {
                            int result;
                            Index = null;
                            if (int.TryParse(args[i + 1], out result))
                            {
                                Index = result;
                            }
                            else
                            {
                                Console.WriteLine(@"Error: Invalid index specification.");
                                IsSafe = false;
                            }
                            i++;
                        }
                        else
                        {
                            Console.WriteLine(@"Error: Incorrect index specification.");
                            IsSafe = false;
                        }
                        break;

                    case "-speed":
                    case "-spd":
                        if (i + 1 < args.Length)
                        {
                            double result;
                            SpeedScale = null;
                            if (double.TryParse(args[i + 1], out result))
                            {
                                SpeedScale = result;
                            }
                            else
                            {
                                Console.WriteLine(@"Error: Invalid speed specification.");
                                IsSafe = false;
                            }
                            i++;
                        }
                        else
                        {
                            Console.WriteLine(@"Error: Incorrect speed specification.");
                            IsSafe = false;
                        }
                        break;

                    case "-pitch":
                    case "-pch":
                        if (i + 1 < args.Length)
                        {
                            double result;
                            PitchScale = null;
                            if (double.TryParse(args[i + 1], out result))
                            {
                                SpeedScale = result;
                            }
                            else
                            {
                                Console.WriteLine(@"Error: Invalid pitch specification.");
                                IsSafe = false;
                            }
                            i++;
                        }
                        else
                        {
                            Console.WriteLine(@"Error: Incorrect pitch specification.");
                            IsSafe = false;
                        }
                        break;

                    case "-intonation":
                    case "-itn":
                        if (i + 1 < args.Length)
                        {
                            double result;
                            IntonationScale = null;
                            if (double.TryParse(args[i + 1], out result))
                            {
                                IntonationScale = result;
                            }
                            else
                            {
                                Console.WriteLine(@"Error: Invalid intonation specification.");
                                IsSafe = false;
                            }
                            i++;
                        }
                        else
                        {
                            Console.WriteLine(@"Error: Incorrect intonation specification.");
                            IsSafe = false;
                        }
                        break;

                    case "-volume":
                    case "-vol":
                        if (i + 1 < args.Length)
                        {
                            double result;
                            VolumeScale = null;
                            if (double.TryParse(args[i + 1], out result))
                            {
                                VolumeScale = result;
                            }
                            else
                            {
                                Console.WriteLine(@"Error: Invalid volume specification.");
                                IsSafe = false;
                            }
                            i++;
                        }
                        else
                        {
                            Console.WriteLine(@"Error: Incorrect volume specification.");
                            IsSafe = false;
                        }
                        break;

                    case "-prephonemelength":
                    case "-prelen":
                        if (i + 1 < args.Length)
                        {
                            double result;
                            PrePhonemeLength = null;
                            if (double.TryParse(args[i + 1], out result))
                            {
                                PrePhonemeLength = result;
                            }
                            else
                            {
                                Console.WriteLine(@"Error: Invalid prephonemelength specification.");
                                IsSafe = false;
                            }
                            i++;
                        }
                        else
                        {
                            Console.WriteLine(@"Error: Incorrect prephonemelength specification.");
                            IsSafe = false;
                        }
                        break;

                    case "-postphonemelength":
                    case "-postlen":
                        if (i + 1 < args.Length)
                        {
                            double result;
                            PostPhonemeLength = null;
                            if (double.TryParse(args[i + 1], out result))
                            {
                                PostPhonemeLength = result;
                            }
                            else
                            {
                                Console.WriteLine(@"Error: Invalid postphonemelength specification.");
                                IsSafe = false;
                            }
                            i++;
                        }
                        else
                        {
                            Console.WriteLine(@"Error: Incorrect postphonemelength specification.");
                            IsSafe = false;
                        }
                        break;

                    case "-help":
                        if (i + 1 >= args.Length)
                        {
                            Help();
                            IsSafe = false;
                            break;
                        }

                        {
                            var mode = args[i + 1].ToLower();
                            switch (args[i + 1])
                            {
                                case "talk":
                                    TalkHelp();
                                    IsSafe = false;
                                    break;

                                case "sing":
                                    SingHelp();
                                    IsSafe = false;
                                    break;

                                default:
                                    Console.WriteLine(@"Error: Incorrect help specification.");
                                    Help();
                                    IsSafe = false;
                                    break;
                            }
                        }
                        i++;

                        break;
                }

            }

        }

        private void ConfigLoad()
        {
            if (File.Exists(ConfFileNamee))
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(UserConfig));
                UserConfig? json = null;

                try
                {
                    using (var fs = new FileStream(ConfFileNamee, FileMode.Open))
                    {
                        json = (UserConfig)js.ReadObject(fs);
                        fs.Close();
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (json != null)
                {
                    SpecifiedProduct = json.DefaultSetting.SpecifiedProduct;
                    SpecifiedHost = json.DefaultSetting.SpecifiedHost;
                    SpecifiedPort = json.DefaultSetting.SpecifiedPort;
                    SpeedScale = json.DefaultSetting.SpeedScale;
                    PitchScale = json.DefaultSetting.PitchScale;
                    IntonationScale = json.DefaultSetting.IntonationScale;
                    VolumeScale = json.DefaultSetting.VolumeScale;
                    PrePhonemeLength = json.DefaultSetting.PrePhonemeLength;
                    PostPhonemeLength = json.DefaultSetting.PostPhonemeLength;
                    OutputSamplingRate = json.DefaultSetting.OutputSamplingRate;
                    Index = json.DefaultSetting.Index;
                    RenderingMode = json.DefaultSetting.RenderingMode;
                    TeacherIndex = json.DefaultSetting.TeacherIndex;

                    // -sf は -mf が定義されたら上書きされる
                    if (json.DefaultSetting.InputfilenameS != "")
                    {
                        Inputfilename = json.DefaultSetting.InputfilenameS;
                        SingWaveGenType = SingWavGenTypeEnum.splitnote;
                        TalkWaveGenType = TalkWavGenTypeEnum.splitline;
                    }
                    // -mf が優先される
                    if (json.DefaultSetting.InputfilenameM != "")
                    {
                        Inputfilename = json.DefaultSetting.InputfilenameM;
                        SingWaveGenType = SingWavGenTypeEnum.allnote;
                        TalkWaveGenType = TalkWavGenTypeEnum.allline;
                    }

                    SoundSetting = json.SoundSetting;
                }
            }
        }

        private void Help()
        {
            Console.WriteLine(
                @"
voxsay command {0} (c)2024 by k896951

talk command line exsamples:
    voxsay -prodlist
    voxsay <-prod TTS> [Options1] -list
    voxsay <-prod TTS> [Options1] [-save FILENAME] <-index N> [Options2] -t  TALKTEXT
    voxsay <-prod TTS> [Options1] [-save FILENAME] <-index N> [Options2] [ -mf | -sf ] TEXTFILE

sing command line exsamples (VOICEVOX ONLY):
    voxsay -prod voicevox -renderingmode sing [Options1] -list
    voxsay -prod voicevox -renderingmode sing [Options1] -teacherlist
    voxsay -prod voicevox -renderingmode sing [Options1] [-save FILENAME] <-index N> [-teacherindex N] [Options2] -t  TALKTEXT
    voxsay -prod voicevox -renderingmode sing [Options1] [-save FILENAME] <-index N> [-teacherindex N] [Options2] [ -mf | -sf ] TEXTFILE

Note:
    * The ""-renderingmode sing"" option is only for VOICEVOX.


-t,-mf,-sf and -save combination:
    -save sample.wav -t text       : Output sample.wav
    -save sample.wav -mf textfile  : Output sample.wav
    -save sample.wav -sf textfile  : Output sample_000001.wav, sample_000002.wav, …　Outputs a wav file for the number of lines in the textfile.


help command line for Options1, Options2:
    voxsay -help talk
    voxsay -help sing
"
                , VersionStr );
        }

        private void TalkHelp()
        {
            Console.WriteLine(
                @"
talk mode help

talk command line exsamples:
    voxsay -prodlist
    voxsay [-renderingmode talk] <-prod TTS> [Options1] -list
    voxsay [-renderingmode talk] <-prod TTS> [Options1] [-save FILENAME] <-index N> [Options2] -t TALKTEXT
    voxsay [-renderingmode talk] <-prod TTS> [Options1] [-save FILENAME] <-index N> [Options2] [ -mf | -sf ] TEXTFILE

Options:
    -prodlist             : List available local TTS products.
    -renderingmode talk   : Select talk rendering mode. *default is ""talk"".
    -prod TTS             : Select tts product. TTS := <sapi | voicevox | voicevoxnemo | coeiroink | coeiroinkv2 | lmroid | sharevox | itvoice>
    -list                 : List speakers for a given product.
    -save FILENAME        : Save audio with specified file name.
                            Example: -save Hellow  -> Output audio to file ""Hellow.wav"".
                            Note: No audio playback with this option.
    -index N              : specify the speaker index.
                            Example: -index 4 -> use speaker index number 4.
    -t TalkText           : TalkText to output in tts.
                            Example : -t Hellow world! -> say ""Hello world!""
    -mf TEXTFILE          : Output the contents of TEXTFILE in tts.
                            Example : -mf comment.txt -> say all the contents of comment.txt.
    -sf TEXTFILE          : Output the contents of TEXTFILE in tts.
                            Example : -sf comment.txt -> say the contents of comment.txt by line.
Options1:
    -host                 : Host name of TTS service running.
    -port                 : Port number of TTS service running.

Options2:
    -samplingrate Hz      : Change audio sampling rate. Default is 44100 (44.1kHz).
                            Example : -samplingrate 8000 -> Change the sampling rate to 8kHz.
                            Note: Quantization bit number is 16bit only.

    -speed P              : specify the speedScale.        Default: 1    Range:  0.5  .. 2    Step: 0.01
                                                           Default: 100  Range:  0    .. 100  Step: 1.00 * sapi 
    -pitch P              : specify the pitchScale.        Default: 0    Range: -0.15 .. 0.15 Step: 0.01
    -intonation P         : specify the intonationScale.   Default: 1    Range:  0    .. 2    Step: 0.01
    -volume P             : specify the volumeScale.       Default: 1    Range:  0    .. 2    Step: 0.01
                                                           Default: 0    Range: -10   .. 10   Step: 1.00 * sapi
    -prephonemelength P   : specify the prephonemelength.  Default: 0.1  Range:  0    .. 1.5  Step: 0.01
    -postphonemelength P  : specify the postphonemelength. Default: 0.1  Range:  0    .. 1.5  Step: 0.01

Note:
    - Anything specified after -t is treated as tts text.
    - Please refer to the value of the editor for each product for the range of P.
"
            );
        }


        private void SingHelp()
        {
            Console.WriteLine(
                @"
sing mode help

sing command line exsamples (VOICEVOX ONLY):
    voxsay -prod voicevox -renderingmode sing [Options1] -list
    voxsay -prod voicevox -renderingmode sing [Options1] -teacherlist
    voxsay -prod voicevox -renderingmode sing [Options1] [-save FILENAME] <-index N> [-teacherindex N] [Options2] -t TALKTEXT
    voxsay -prod voicevox -renderingmode sing [Options1] [-save FILENAME] <-index N> [-teacherindex N] [Options2] [ -mf | -sf ] TEXTFILE

Options:
    -renderingmode sing   : Select sing rendering mode.
    -list                 : List singers for a given product.
    -teacherlist          : List song teachers for a given product.
    -save FILENAME        : Save audio with specified file name.
                            Example: -save Hellow  -> Output audio to file ""Hellow.wav"".
                            Note: No audio playback with this option.
    -index N              : specify the singer index.
                            Example: -index 3003 -> use singer index number 3003.
    -teacherindex N       : specify the song teacher index.
                            Example: -teacherindex 6000 -> use song teacher index number 6000.
    -t MMLtext            : MMLtext to output in VOICEVOX.
                            Example : -t CDEF -> sing ""Do Re Mi Fa""
    -mf MMLTEXTFILE       : Output the contents of MMLTEXTFILE in VOICEVOX.
                            Example : -mf comment.txt -> sing the contents of the comment.txt.
    -sf MMLTEXTFILE       : Output the contents of MMLTEXTFILE in VOICEVOX.
                            Example : -sf comment.txt -> sing the contents of comment.txt by line.

Options1:
    -host                 : Host name of VOICEVOX service running.
    -port                 : Port number of VOICEVOX service running.

Options2:
    -samplingrate Hz      : Change audio sampling rate. Default is 44100 (44.1kHz).
                            Example : -samplingrate 8000 -> Change the sampling rate to 8kHz.
                            Note: Quantization bit number is 16bit only.

    -volume P             : specify the volumeScale. Default: 1 Range:0 .. 2 Step: 0.01

Note:
    - Anything specified after -t is treated as MML text.
    - The renderingmode option is only for VOICEVOX.
    - Please refer to the value of the editor for each product for the range of P.
"
            );
        }


    }
}
