using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2.UserConfigs
{
    [DataContract]
    public class SoundSettings
    {
        [DataMember(Name = "method")]
        public string Method { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "audiodriver")]
        public string AudioDriver { get; set; }

        [DataMember(Name = "frontopts")]
        public string[] FrontOpts { get; set; }

        [DataMember(Name = "rearopts")]
        public string[] RearOpts { get; set; }

        public SoundSettings()
        {
            FrontOpts = [ "-q" ];
            RearOpts = [ "-d" ];
            Command = "sox";
            Method = "method";
            AudioDriver = "waveaudio";
        }
    }
}
