using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums.UserRegistration
{
    public enum EventType
    {
        Login,
        Logout,
        PasswordChange,
        EmailChange,
        AccountCreated,
        AccountDeleted
        // Add more events as needed
    }
}
