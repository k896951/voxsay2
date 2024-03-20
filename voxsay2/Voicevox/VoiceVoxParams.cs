using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    public class VoiceVoxParams : SpeakerParams
    {
        public VoiceVoxParams()
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
