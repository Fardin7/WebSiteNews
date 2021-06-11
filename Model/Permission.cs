using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Permission
    {
        public int Id { get; set; }
        public int Acrion { get; set; }
        public int Entity { get; set; }
        public string ApplicationUserId { get; set; }
        public string RoleId { get; set; }

    }
}
