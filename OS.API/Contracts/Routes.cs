using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Contracts
{
    public static class Routes
    {
        private const string Root = "os";


        public static class Users
        {
            public const string AllUsers = Root + "/users"; // non auth, for dev
            public const string OneUser = Root + "/user";
        }

        public static class Auth
        {
            public const string Login = Root + "/login";
            public const string Register = Root + "/register";
        }
    }
}
