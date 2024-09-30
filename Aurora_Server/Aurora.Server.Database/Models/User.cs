using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Database.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
