using System.Windows.Controls;

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

        public FrequencyControl() => InitializeComponent();
    }
}
