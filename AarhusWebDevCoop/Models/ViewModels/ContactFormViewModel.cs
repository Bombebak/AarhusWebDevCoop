using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.Models.ViewModels
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "A name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "An email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "A subject is required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "A message is required")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}