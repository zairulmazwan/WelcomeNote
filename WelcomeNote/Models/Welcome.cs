using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WelcomeNote.Models
{
    public class Welcome
    {

        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Message Para 1")]
        public string Message1 { get; set; }

        [Required]
        [Display(Name = "Message Para 2")]
        public string Message2 { get; set; }


        [Required]
        [Display(Name = "Message Para 3")]
        public string Message3 { get; set; }


        [Required]
        [Display(Name = "Date Update")]
        //[DataType(DataType.Date)]
        public string DateUpdate { get; set; }


        [Required]
        [Display(Name = "User")]
        public string User { get; set; }

    }
}
