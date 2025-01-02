using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voxsay2.common;

namespace voxsay2.Coeiroink
{
    public class Coeiroinkv2Params : SpeakerParams
    {
        public Coeiroinkv2Params()
        {
            speedScale = 1.0f;
            pitchScale = 0.0f;
            intonationScale = 1.0f;
            volumeScale = 1.0f;
            prePhonemeLength = 0.1f;
            postPhonemeLength = 0.1f;
            outputSamplingRate = 44100;
        }
    }

}
