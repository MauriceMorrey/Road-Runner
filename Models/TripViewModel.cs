using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace road_runner.Models
{
    public class TripViewModel : BaseEntity
    {
        [Required]
        public string start_point { get; set; }

        [Required]
        public string destination { get; set; }

        [Required]

        public string description { get; set; }

        [Required]
        [FutureDate]
        public DateTime start_date { get; set; }

        [Required]
        [FutureDate]
        public DateTime end_date { get; set; }


        public class FutureDate : ValidationAttribute
        {

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime date = (DateTime)value;
                return date < DateTime.Now ? new ValidationResult("Date must be in the future") : ValidationResult.Success;
            }
        }
    }
}