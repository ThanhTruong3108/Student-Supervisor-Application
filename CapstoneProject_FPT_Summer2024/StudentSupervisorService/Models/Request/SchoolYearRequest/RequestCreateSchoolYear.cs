using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.SchoolYearRequest
{
    public class RequestCreateSchoolYear
    {
        [Required(ErrorMessage = "The SchoolId field is required.")]
        public int SchoolId { get; set; }

        [Required(ErrorMessage = "The Year field is required.")]
        public short Year { get; set; }

        [Required(ErrorMessage = "The StartDate field is required.")]
        [YearMustMatch("Year", ErrorMessage = "Năm của StartDate phải khớp với Year")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The EndDate field is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "The Semester1StartDate field is required.")]
        public DateTime Semester1StartDate { get; set; }

        [Required(ErrorMessage = "The Semester1EndDate field is required.")]
        public DateTime Semester1EndDate { get; set; }

        [Required(ErrorMessage = "The Semester2StartDate field is required.")]
        public DateTime Semester2StartDate { get; set; }

        [Required(ErrorMessage = "The Semester2EndDate field is required.")]
        public DateTime Semester2EndDate { get; set; }
    }

    public class YearMustMatchAttribute : ValidationAttribute
    {
        private readonly string _yearPropertyName;

        public YearMustMatchAttribute(string yearPropertyName)
        {
            _yearPropertyName = yearPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var yearProperty = validationContext.ObjectType.GetProperty(_yearPropertyName);
            if (yearProperty != null)
            {
                var yearValue = (short)yearProperty.GetValue(validationContext.ObjectInstance);
                var dateValue = (DateTime)value;

                if (dateValue.Year != yearValue)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
