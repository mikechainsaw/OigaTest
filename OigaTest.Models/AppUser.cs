using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OigaTest.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public EUserRole Role{ get; set; }

        public string Address { get; set; }

        public Tenant Tenant { get; set; }
    }
}
