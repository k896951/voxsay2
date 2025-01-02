using System.Runtime.Serialization;

namespace voxsay2.AivisSpeech
{
    [DataContract]
    internal class AivisSpeechSpeaker
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "speaker_uuid")]
        public string SpeakerUuid { get; set; }

        [DataMember(Name = "styles")]
        public AivisSpeechSpeakerStyle[] Styles { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; }

        [DataMember(Name = "supported_features")]
        public AivisSpeechSpeakerSupportedFeatures SupportedFeatures { get; set; }
    }
}
