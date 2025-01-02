using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2.common
{
    [DataContract]
    public class AudioQuery
    {
        [DataMember]
        public double? speedScale { get; set; }

        [DataMember]
        public double? pitchScale { get; set; }

        [DataMember]
        public double? intonationScale { get; set; }

        [DataMember]
        public double? volumeScale { get; set; }

        [DataMember]
        public double? prePhonemeLength { get; set; }

        [DataMember]
        public double? postPhonemeLength { get; set; }

        [DataMember]
        public int? outputSamplingRate { get; set; }

    }
}
