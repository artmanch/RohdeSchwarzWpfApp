using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RohdeSchwarzWpfApp.ViewModels
{
    /// <summary>
    /// ViewModel для тестирования привязки свойства Frequency контрола FrequencyControl
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propetyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propetyName));
        }

        private double _Frequency;
        public double Frequency
        {
            get => _Frequency;
            set
            {
                if(Equals(_Frequency, value)) return;

                _Frequency = value;
                Debug.WriteLine($"Frequency ViewModel Value = {_Frequency}");
                OnPropertyChanged();
            }
        }
    }
}
