using System.ComponentModel;

namespace Core.Model.Enums
{
    public enum MediaCategoryType
    {
        [Description("Internet radio")]
        InternetStreamRadio,
        [Description("Database - alarm sound")]
        DatabaseB64SoundAlarm
    }
}