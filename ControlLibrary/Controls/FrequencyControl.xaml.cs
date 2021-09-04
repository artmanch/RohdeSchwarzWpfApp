using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ControlLibrary.Services;
using ControlLibrary.Services.Interfaces;

namespace ControlLibrary.Controls
{
    /// <summary>
    /// Логика взаимодействия для FrequencyControl.xaml
    /// </summary>
    public partial class FrequencyControl : UserControl
    {
        private IParser Parser { get; set; }

        public FrequencyControl()
        {
            Parser = new FrequencyParser();
            InitializeComponent();
        }

        /// <summary>
        ///Свойство зависимости для хранения частоты в Гц (Hz)
        /// </summary>
        public static readonly DependencyProperty FrequencyProperty =
            DependencyProperty.Register(
                nameof(Frequency),
                typeof(double),
                typeof(FrequencyControl),
                new PropertyMetadata(default(double)));

        [Description("Частота в Гц")]
        public double Frequency
        {
            get => (double) GetValue(FrequencyProperty);
            set => SetValue(FrequencyProperty, value);
        }

        /// <summary>
        /// Свойство для работы с текстом TextBox-а
        /// </summary>
        public string Text
        {
            get => FrequencyTextBox.Text;
            set => FrequencyTextBox.Text = value;
        }

        private void FrequencyConfirmed(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                if(sender is null) return;

                var value = Parser.Parse(Text);
                Frequency = GetFrequency(value.frequency, value.metric);
                Text = GetText(value.frequency, value.metric);
            }
        }

        /// <summary>
        /// Получаем частоту в Гц 
        /// </summary>
        /// <param name="frequency">частота в текущих е.и.</param>
        /// <param name="metric">е.и.</param>
        /// <returns>частота в Гц</returns>
        private double GetFrequency(double frequency, Metrics metric) => metric switch
        {
            Metrics.kHz => frequency * 1000,
            Metrics.MHz => frequency * 1000000,
            _ => frequency
        };

        /// <summary>
        /// Получаем строковое представление частоты с учетом е.и.
        /// </summary>
        /// <param name="frequency">частота в текущих е.и.</param>
        /// <param name="metric">е.и.</param>
        /// <returns>строковое представление частоты с указанием е.и.)</returns>
        private string GetText(double frequency, Metrics metric)
        {
            switch(metric)
            {
                case Metrics.Hz:
                    if(frequency < 1000) return $"{frequency:f4} Гц";
                    else if(frequency > 1000 && frequency < 1000000) return $"{frequency / 1000:f4} кГц";
                    else return $"{frequency / 1000000:f4} МГц";

                case Metrics.kHz:
                    if(frequency < 1000 && frequency > 1) return $"{frequency:f4} кГц";
                    else if(frequency > 1000) return $"{frequency / 1000:f4} МГц";
                    else return $"{frequency * 1000:f4} Гц";

                case Metrics.MHz:
                    if(frequency > 1) return $"{frequency:f4} МГц";
                    else if(frequency < 1 && frequency > 0.001) return $"{frequency * 1000:f4} кГц";
                    else return $"{frequency * 1000000:f4} Гц";

                default: throw new InvalidOperationException("Не существующая операция");
            }
        }
    }
}
