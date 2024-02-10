using System;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Core.Attributes
{
    public sealed class EnumValidateExistsAttribute : ValidationAttribute
    {
        public EnumValidateExistsAttribute(Type enumType)
            : base("Enumeración incorrecta, PDF: 2, EXCEL: 3, CSV: 4")
        {
            this.EnumType = enumType;
        }

        public override bool IsValid(object value)
        {
            if (this.EnumType == null)
            {
                throw new InvalidOperationException("El tipo no puede ser nulo");
            }
            if (!this.EnumType.IsEnum)
            {
                throw new InvalidOperationException("El tipo debe ser una enumeración");
            }
            if (!Enum.IsDefined(EnumType, value))
            {
                return false;
            }
            return true;
        }

        public Type EnumType
        {
            get;
            set;
        }
    }
}
