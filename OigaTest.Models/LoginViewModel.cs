
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OigaTest.Models
{
    public class LoginViewModel
    {
        #region Properties  

        /// <summary>  
        /// Gets or sets to username address.  
        /// </summary>  
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>  
        /// Gets or sets to password address.  
        /// </summary>  
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        #endregion
    }
}
