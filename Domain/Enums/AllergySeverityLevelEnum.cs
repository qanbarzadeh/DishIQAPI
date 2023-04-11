using System.ComponentModel;

namespace Domain.Enums
{
    public enum AllergySeverityLevelEnum
    {
        [Description("Mild")]
        Mild = 1,

        [Description("Moderate")]
        Moderate = 2,

        [Description("Severe")]
        Severe = 3,

        [Description("Anaphylactic")]
        Anaphylactic = 4
    }
}
