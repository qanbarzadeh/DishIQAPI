using System.ComponentModel;

namespace Domain.Enums
{
    [Flags]
    public enum AccountStatusEnum
    {
        [Description("Active")]
        Active = 1,

        [Description("Inactive")]
        Inactive = 2,

        [Description("Suspended")]
        Suspended = 3,

        [Description("Deleted")]
        Deleted = 4
    }
}
