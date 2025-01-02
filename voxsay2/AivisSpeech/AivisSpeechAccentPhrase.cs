using System.Runtime.Serialization;

namespace voxsay2.AivisSpeech
{
    [DataContract]
    internal class AivisSpeechAccentPhrase
    {
        [DataMember]
        public AivisSpeechMora[] moras { get; set; }

        [DataMember]
        public int accent { get; set; }

        [DataMember]
        public AivisSpeechPauseMora pause_mora { get; set; }

        [DataMember]
        public bool is_interrogative { get; set; }
    }


}
