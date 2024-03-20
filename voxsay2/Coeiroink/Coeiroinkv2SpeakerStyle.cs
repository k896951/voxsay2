using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class Coeiroinkv2SpeakerStyle
    {
        [DataMember(Name = "styleName")]
        public string Name { get; set; }

        [DataMember(Name = "styleId")]
        public int Id { get; set; }
    }

}
