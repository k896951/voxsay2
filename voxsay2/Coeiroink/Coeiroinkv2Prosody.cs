using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class Coeiroinkv2Prosody
    {
        [DataMember]
        public string[] plain;

        [DataMember]
        public List<List<Coeiroinkv2ProsodyDetail>> detail;

    }
}
