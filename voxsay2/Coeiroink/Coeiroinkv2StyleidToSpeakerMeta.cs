using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class Coeiroinkv2StyleidToSpeakerMeta
    {
        [DataMember]
        public string speakerUuid;

        [DataMember]
        public string speakerName;

        [DataMember]
        public int styleId;

        [DataMember]
        public string styleName;
    }
}
