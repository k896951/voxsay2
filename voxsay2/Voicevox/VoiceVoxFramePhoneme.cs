using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class VoiceVoxFramePhoneme
    {
        [DataMember(Name = "phoneme")]
        public string Phoneme { get; set; }

        [DataMember(Name = "frame_length")]
        public int Frame_Length { get; set; }
    }
}
