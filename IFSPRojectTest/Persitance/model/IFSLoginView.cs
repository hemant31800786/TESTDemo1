using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IFSPRojectTest.Persitance.model
{
    public class IFSLoginView
    {
        [Required(ErrorMessage = "Please enter Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }

    }
}