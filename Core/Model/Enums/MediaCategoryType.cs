using System.ComponentModel;

namespace Core.Model.Enums
{
    public enum MediaCategoryType
    {
        [Description("Internet radio - music")]
        InternetStreamRadioMusic,
        [Description("Internet radio - classical music")]
        InternetStreamRadioMusicClassical,
        [Description("Internet radio - dinner music")]
        InternetStreamRadioMusicDinner,
        [Description("Internet radio - talk")]
        InternetStreamRadioTalk,
        [Description("Internet radio - news talk")]
        InternetStreamRadioTalkNews,
        [Description("Database - alarm sound")]
        DatabaseB64SoundAlarm
    }
}