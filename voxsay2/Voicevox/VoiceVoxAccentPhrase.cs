using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class VoiceVoxAccentPhrase
    {
        [DataMember]
        public VoiceVoxMora[] moras { get; set; }

        [DataMember]
        public int accent { get; set; }

        [DataMember]
        public VoiceVoxPauseMora pause_mora { get; set; }

        [DataMember]
        public bool is_interrogative { get; set; }
    }
}
