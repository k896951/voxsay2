using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using voxsay2.common;

namespace voxsay2.Coeiroink
{
    [DataContract]
    public class Coeiroinkv2AudioQuery : AudioQuery
    {
        [DataMember]
        public string speakerUuid { get; set; }

        [DataMember]
        public int styleId { get; set; }

        [DataMember]
        public string text { get; set; }

        [DataMember]
        public List<List<Coeiroinkv2ProsodyDetail>> prosodyDetail { get; set; }

        [DataMember]
        public bool outputStereo { get; set; }
    }
}
