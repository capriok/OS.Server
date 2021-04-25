using OS.API.Infrastructure.Interfaces;
using System;

namespace OS.API.Infrastructure
{
    public class DateService : IDateService
    {
        public string Now()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("T", " ");
        }
        public string LastLogin()
        {
            return DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm");
        }
    }
}
