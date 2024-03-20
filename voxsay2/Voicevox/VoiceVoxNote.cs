using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class VoiceVoxNote
    {
        [DataMember(Name = "key")]
        public int? Key { get; set; }

        [DataMember(Name = "frame_length")]
        public int Frame_Length { get; set; }

        [DataMember(Name = "lyric")]
        public string Lyric { get; set; }

        [DataMember(Name = "notelen")]
        public string NoteLen { get; set; }
    }
}
