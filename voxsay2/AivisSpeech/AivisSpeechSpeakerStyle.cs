using System.Runtime.Serialization;

namespace voxsay2.AivisSpeech
{
    [DataContract]
    internal class AivisSpeechSpeakerStyle
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "type")]
        public string @Type { get; set; }
    }

}
