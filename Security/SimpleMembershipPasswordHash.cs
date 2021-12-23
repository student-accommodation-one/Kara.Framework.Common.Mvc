using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Framework.Common.Mvc.Security
{
    public class SimpleMembershipPasswordHash : IPasswordHash
    {
        public string HashPassword(string password)
        {
            //var provider = new SimpleMembershipProvider();
            //return provider.GetPassword("testUser1");
            return string.Empty;
        }
    }
}
