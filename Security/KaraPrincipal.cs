using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Framework.Common.Mvc.Security
{
    public class KaraPrincipal : GenericPrincipal
    {
        public KaraPrincipal(IIdentity identity, string[] roles) : base(identity, roles) { this.Roles = roles; }
        public int ApplicationUserId { get; set; }
        public int ApplicationUserPersonID { get; set; }
        public string[] Roles { get; set; }
    }
}
