using System;
using System.ComponentModel.DataAnnotations;

namespace Task_2.Models
{
    public class AddLabelModel
    {
        [Required]
        public String label { get; set; }

        public Guid id { get; set; }
    }
}