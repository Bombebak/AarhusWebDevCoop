using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.Models.ViewModels
{
    public class MessageBoardViewModel
    {
        [Required(ErrorMessage = "A name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A message is required")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public DateTime CreateDate { get; set; }
    }
}