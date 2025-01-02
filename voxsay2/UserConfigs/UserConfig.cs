using System.Runtime.Serialization;

namespace voxsay2.UserConfigs
{
    [DataContract]
    public class UserConfig
    {
        [DataMember(Name = "defaultSetting")]
        public DefaultSettings DefaultSetting { get; set; }

        [DataMember(Name = "soundSetting")]
        public SoundSettings SoundSetting { get; set; }

        public UserConfig()
        {
            DefaultSetting = new DefaultSettings();
            SoundSetting = new SoundSettings();
        }
    }
}
