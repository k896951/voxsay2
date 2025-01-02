
using System.Runtime.Serialization;

namespace voxsay2.AivisSpeech
{
    [DataContract]
    public class AivisSpeechSpeakerSupportedFeatures
    {
        [DataMember(Name = "permitted_synthesis_morphing")]
        public string Permitted_synthesis_morphing { get; set; }
    }
}
