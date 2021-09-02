using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using ControlLibrary.Services.Interfaces;

namespace ControlLibrary.Services
{
    public sealed class FrequencyParser : IParser
    {
        public (double frequency, Metrics metric) Parse(string value)
        {
            double frequency = 0;
            if(string.IsNullOrEmpty(value)) return (frequency, Metrics.Hz);

            if(double.TryParse(value, out frequency))
                return (frequency, Metrics.Hz);

            var cValue = value.Trim().ToLower().ToCharArray();
            if(!char.IsDigit(cValue[0]))
                return (frequency, Metrics.Hz);

            var stringFrequency = string.Empty;

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

            var stringMetric = string.Join("", cValue[index..]);

            //меняем разделитель ',' дробной части числа на '.'
            var ruCultureSeparatedFloatValues = stringFrequency.Split(',', StringSplitOptions.RemoveEmptyEntries);
            stringFrequency = string.Join(".", ruCultureSeparatedFloatValues);

            double.TryParse(stringFrequency, NumberStyles.Any, CultureInfo.InvariantCulture, out frequency);


            return stringMetric switch
            {
                "кгц" => (frequency, Metrics.kHz),
                "к" => (frequency, Metrics.kHz),
                "мгц" => (frequency, Metrics.MHz),
                "м" => (frequency, Metrics.MHz),
                _ => (frequency, Metrics.Hz)
            };
        }
    }
}
