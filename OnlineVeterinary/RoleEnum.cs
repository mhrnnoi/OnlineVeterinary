using System.ComponentModel;

namespace OnlineVeterinary
{
    public enum RoleEnum
    {
        [Description("Doctor")]
        Doctor = 1,
        [Description("CareGiver")]

        CareGiver,
        [Description("Admin")]
        Admin

    }
}