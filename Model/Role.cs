using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Role : IdentityRole
    {


        public ICollection<Permission> Permissions { get; set; }
    }
}
