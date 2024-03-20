using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class Coeiroinkv2ProsodyDetail
    {
        [DataMember]
        public string phoneme;

        [DataMember]
        public string hira;

        [DataMember]
        public int? accent;
    }
}
