using voxsay2.common;

namespace voxsay2.AivisSpeech
{
    public class AivisSpeechParams : SpeakerParams
    {
        public double tempodynamicsScale;
        public double pauselengthScale;

        public AivisSpeechParams()
        {
            speedScale = 1.0f;
            pitchScale = 0.0f;
            intonationScale = 1.0f;
            volumeScale = 1.0f;
            tempodynamicsScale = 1.0;
            prePhonemeLength = 0.1f;
            postPhonemeLength = 0.1f;
            pauselengthScale = 1.0;
            outputSamplingRate = 44100;
        }

    }
}
