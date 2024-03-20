using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class VoiceVoxSpeaker
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string speaker_uuid { get; set; }
        [DataMember]
        public VoiceVoxSpeakerStyle[] styles { get; set; }
        [DataMember]
        public string version { get; set; }
    }
}
