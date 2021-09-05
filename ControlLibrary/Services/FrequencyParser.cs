using System;
using System.Globalization;
using ControlLibrary.Services.Interfaces;

namespace ControlLibrary.Services
{
    public sealed class FrequencyParser : IParser
    {
        /// <summary>
        /// Парсер строки в кортеж 
        /// </summary>
        /// <param name="value">пользовательская строка</param>
        /// <returns>кортеж данных</returns>
        public (double frequency, Metrics metric) Parse(string value)
        {
            ///Если строка пустая возвращаем кортеж (0, Hz)
            double frequency = 0;
            if(string.IsNullOrEmpty(value)) return (frequency, Metrics.Hz);

            ///Если парсится без проблем, то возвращаем сразу в Гц
            if(double.TryParse(value, out frequency))
                return (frequency, Metrics.Hz);

            ///Валидация, проверяющая, что в начале у нас идет цифра
            var cValue = value.Trim().ToLower().ToCharArray();
            if(!char.IsDigit(cValue[0]))
                return (frequency, Metrics.Hz);

            var stringFrequency = string.Empty;

            ///Запоминаем индекс элемента в массиве символов, с которого начинают идти все кроме цифр в строке
            int index;
            for(index = 0; index < cValue.Length; index++)
            {
                if(char.IsDigit(cValue[index]) || cValue[index] == '.' || cValue[index] == ',')
                    stringFrequency += cValue[index];
                else if(char.IsWhiteSpace(cValue[index]))
                    continue;
                else if(char.IsLetter(cValue[index]))
                    break;
            }

            ///Создаем строку, которая предположительно является единицами измерения
            var stringMetric = string.Join("", cValue[index..]);

            //разбиваем строку на целую и дробную часть и слепливаем с разделителем "точка"
            var ruCultureSeparatedFloatValues = stringFrequency.Split(new char[] { ',', '.'}, StringSplitOptions.RemoveEmptyEntries);
            stringFrequency = string.Join(".", ruCultureSeparatedFloatValues);

            ///Т.к. разделитель точка, то используем инвариантную культуру, чтоб не было проблем с преобразованием в double
            double.TryParse(stringFrequency, NumberStyles.Any, CultureInfo.InvariantCulture, out frequency);

            ///Сравниваем предположительную строку с единицами измерений с паттернами строк
            return stringMetric switch
            {
                "кгц" => (frequency, Metrics.kHz),
                "кц" => (frequency, Metrics.kHz),
                "к" => (frequency, Metrics.kHz),
                "мгц" => (frequency, Metrics.MHz),
                "мц" => (frequency, Metrics.MHz),
                "м" => (frequency, Metrics.MHz),
                _ => (frequency, Metrics.Hz)
            };
        }
    }
}
