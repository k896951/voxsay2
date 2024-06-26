﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    [DataContract]
    public class VoiceVoxFrameAudioQuery
    {
        [DataMember(Name = "f0")]
        public double[] F0 { get; set; }

        [DataMember(Name = "volume")]
        public double[] Volume { get; set; }

        [DataMember(Name = "phonemes")]
        public VoiceVoxFramePhoneme[] Phonemes { get; set; }

        [DataMember(Name = "volumeScale")]
        public double VolumeScale { get; set; }

        [DataMember(Name = "outputSamplingRate")]
        public int OutputSamplingRate { get; set; }

        [DataMember(Name = "outputStereo")]
        public Boolean OutputStereo { get; set; }
    }
}
