using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sql_practice
{
    internal class user
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserDescription { get; set; }
        public DateOnly datejoin { get; set; }
    }

}
