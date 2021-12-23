using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Framework.Common.Mvc.Security
{
    interface IPasswordHash
    {
        string HashPassword(string password);
    }
}
