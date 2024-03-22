using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class DefaultSettings
    {
        [DataMember(Name = "prod")]
        public string SpecifiedProduct { get;  set; }

        [DataMember(Name = "host")]
        public string? SpecifiedHost { get;  set; }

        [DataMember(Name = "port")]
        public int? SpecifiedPort { get;  set; }

        [DataMember(Name = "speed")]
        public double? SpeedScale { get;  set; }

        [DataMember(Name = "pitch")]
        public double? PitchScale { get;  set; }

        [DataMember(Name = "intonation")]
        public double? IntonationScale { get;  set; }

        [DataMember(Name = "volume")]
        public double? VolumeScale { get;  set; }

        [DataMember(Name = "prephonemelength")]
        public double? PrePhonemeLength { get;  set; }

        [DataMember(Name = "postphonemelength")]
        public double? PostPhonemeLength { get;  set; }

        [DataMember(Name = "samplingrate")]
        public int? OutputSamplingRate { get;  set; }

        [DataMember(Name = "index")]
        public int? Index { get;  set; }

        [DataMember(Name = "renderingmode")]
        public string RenderingMode { get;  set; }

        [DataMember(Name = "teacherindex")]
        public int? TeacherIndex { get; set; }

        [DataMember(Name = "mf")]
        public string? InputfilenameM { get;  set; }

        [DataMember(Name = "sf")]
        public string? InputfilenameS { get;  set; }

        public DefaultSettings()
        {
            SpecifiedProduct = "voicevox";
            RenderingMode = "talk";
        }
    }
}
