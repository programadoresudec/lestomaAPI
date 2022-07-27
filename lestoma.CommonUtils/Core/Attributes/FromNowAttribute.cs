using System;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Core.Attributes
{
    public sealed class FromNowAttribute : ValidationAttribute
    {
        public FromNowAttribute() { }

        public string GetErrorMessage() => "La fecha debe ser pasada o igual a la fecha actual.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;
            int response = DateTime.Compare(date, DateTime.Now);
            if (response >= 0)
            {
                return new ValidationResult(GetErrorMessage());
            }
            else { return ValidationResult.Success; }

        }
    }
}
