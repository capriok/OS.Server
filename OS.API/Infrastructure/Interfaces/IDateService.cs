using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Infrastructure.Interfaces
{
    public interface IDateService
    {
        public string Now();
        public string LastLogin();
    }
}
