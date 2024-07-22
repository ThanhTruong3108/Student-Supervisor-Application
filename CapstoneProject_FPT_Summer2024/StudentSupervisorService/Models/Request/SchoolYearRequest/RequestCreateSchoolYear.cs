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
        [YearMustMatch("Year", ErrorMessage = "The year of StartDate must match the Year field.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The EndDate field is required.")]
        [YearMustMatch("Year", ErrorMessage = "The year of EndDate must match the Year field.")]
        public DateTime EndDate { get; set; }
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
