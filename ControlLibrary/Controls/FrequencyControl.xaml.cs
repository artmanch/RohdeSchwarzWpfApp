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
        /// <summary>
        ///Поле для хранения частоты в Гц (Hz)
        /// </summary>
        public double Frequency { get; set; } = 0;

        private IParser Parser { get; set; } = new FrequencyParser();

        public FrequencyControl() => InitializeComponent();

        public string Text 
        {
            get => FrequencyTextBox.Text;
            set => FrequencyTextBox.Text = value;
        }

        //todo:добавить логику получения значения поля Frequency в Гц и формирования Text 
        private void FrequencyConfirmed(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                if(sender is null) return;
                
                var value = Parser.Parse(Text);
            }
        }
    }
}
