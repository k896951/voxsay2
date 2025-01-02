using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using voxsay2.common;

namespace voxsay2
{
    [DataContract]
    public class VoiceVoxAudioQuery : AudioQuery
    {
        [DataMember]
        public VoiceVoxAccentPhrase[] accent_phrases { get; set; }

        [DataMember]
        public bool outputStereo { get; set; }

        [DataMember]
        public string kana { get; set; }
    }

}
