using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Controllers
{
    public static class Routes
    {
        private const string Root = "os";

        public static class User
        {
            public const string AllUsers = Root + "/users"; // non auth, for dev
            public const string OneUser = Root + "/user";
            public const string Login = Root + "/login";
            public const string Register = Root + "/register";
            public const string Refresh = Root + "/refreshAccessToken";
        }

        public static class Oversite
        {
            public const string AllOversites = Root + "/oversites";
            public const string OneOversite = Root + "/oversite";
        }
    }
}
