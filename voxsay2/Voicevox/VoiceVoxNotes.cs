using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class VoiceVoxNotes
    {
        [DataMember(Name = "notes")]
        public List<VoiceVoxNote> Notes;
    }
}
