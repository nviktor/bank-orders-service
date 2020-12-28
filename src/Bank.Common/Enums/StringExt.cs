using System;
using System.Text;

namespace Bank.Common.Enums
{
    public static class StringExt
    {
        /// <summary>
        /// Преобразует строку в Enum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="val"></param>
        /// <returns>возвращает значение перечисления(по умолчанию 0)</returns>
        public static TEnum ToEnum<TEnum>(this string val, bool throwOnError = false)
        {
            try
            {
                var enumVal = (TEnum)Enum.Parse(typeof(TEnum), val);
                return enumVal;
            }
            catch (ArgumentNullException)
            {
                if (throwOnError)
                {
                    throw new ArgumentException($"Значение переменной не может быть null. Возможные значения {EnumToString<TEnum>()}");
                }

                return default(TEnum);
            }
            catch (ArgumentException)
            {
                if (throwOnError)
                {
                    throw new ArgumentException($"Значение \"{val}\" не найдено. Возможные значения {EnumToString<TEnum>()}");
                }

                return default(TEnum);
            }
        }

        private static string EnumToString<TEnum>()
        {
            var enumType = typeof(TEnum);

            var str = new StringBuilder();

            str.AppendFormat("\r\n{0}:\r\n", enumType.Name);

            foreach (var item in Enum.GetNames(enumType))
            {
                str.AppendFormat("{0}\r\n", item);
            }

            return str.ToString();
        }
    }
}
