using ForumSystem.Data.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Data
{
    public class MsSqlDbContex : IdentityDbContext<User>
    {
        public MsSqlDbContex()
            : base("LocalConnection", throwIfV1Schema: false)
        {
        }

        public static MsSqlDbContex Create()
        {
            return new MsSqlDbContex();
        }
    }
}
