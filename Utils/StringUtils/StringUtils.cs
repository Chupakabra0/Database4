using System.Globalization;
using System.Linq;
using ConsoleDBTest.Models;

namespace ConsoleDBTest.Utils.StringUtils {
    public static class StringUtils {
        public static string GetPersonName(string name, string surname, string patronymic) =>
            $"{(string.IsNullOrEmpty(name) ? string.Empty : $"{name.First()}. ")}{(string.IsNullOrEmpty(patronymic) ? string.Empty : $"{patronymic.First()}. ")}{surname}";

        public static string ToFirstLetterUpperCase(this string str) =>
            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());

        public static string ClientTypeToString(this ClientType clientType) =>
            clientType switch {
                ClientType.None    => "None",
                ClientType.Teacher => "Преподаватель",
                ClientType.Student => "Студент",
                _                  => string.Empty
            };
    }
}
