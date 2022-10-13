using System;
using System.ComponentModel;
using System.Reflection;

namespace lestoma.CommonUtils.Enums
{
    /// <summary>
    /// Clase EnumDescription
    /// </summary>
    public static class EnumConfig
    {
        /// <summary>
        /// Obtiene el valor string del atributo Description de un enumerador
        /// </summary>
        /// <param name="value">Enum</param>
        /// <returns>description</returns>
        public static string GetDescription(Enum value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            DescriptionAttribute[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Description;
            }
            return output;
        }

        /// <summary>
        /// Obtiene el nombre de un enumerador
        /// </summary>
        /// <param name="value">Enum</param>
        /// <returns>Name</returns>
        public static string GetName(Enum value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            String name = Enum.GetName(value.GetType(), value);
            return name;
        }

    }
}
