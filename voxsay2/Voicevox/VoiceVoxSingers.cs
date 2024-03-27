using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class VoiceVoxSingers
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Speaker_uuid { get; set; }

        [DataMember]
        public VoiceVoxSpeakerStyle[] Styles { get; set; }

        [DataMember]
        public string Version { get; set; }
    }
}
