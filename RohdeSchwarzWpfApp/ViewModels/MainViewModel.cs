using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RohdeSchwarzWpfApp.ViewModels
{
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

        private string _Title = "Test";
        public string Title
        {
            get => _Title;
            set
            {
                if(Equals(_Title, value)) return;

                _Title = value;
                Debug.WriteLine("Title ViewModel");
                OnPropertyChanged();
            }
        }
    }
}
