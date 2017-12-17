using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task_1;

namespace Task_2.Models
{
    public class AddTodoViewModel
    {
        [Required]
        public String text { get; set; }


        public DateTime? dateDue { get; set; }

        public IEnumerable<SelectListItem> labels { get; set; }
    }
}