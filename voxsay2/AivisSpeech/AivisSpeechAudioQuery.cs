using System.Runtime.Serialization;
using voxsay2.common;

namespace voxsay2.AivisSpeech
{
    [DataContract]
    internal class AivisSpeechAudioQuery : AudioQuery
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int? id { get; set; }

        [DataMember]
        public AivisSpeechAccentPhrase[] accent_phrases { get; set; }

        [DataMember]
        public double? tempoDynamicsScale { get; set; }

        [DataMember]
        public double? pauseLength { get; set; } = null;  // コマンドラインから制御しない

        [DataMember]
        public double? pauseLengthScale { get; set; }     // コマンドラインの -pauselength はこちらの事

        [DataMember]
        public bool outputStereo { get; set; }

        [DataMember]
        public string kana { get; set; }
    }

}
