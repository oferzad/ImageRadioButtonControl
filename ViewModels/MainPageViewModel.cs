using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tests.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private bool _option1IsChecked;
        public bool Option1IsChecked
        {
            get => _option1IsChecked;
            set
            {
                _option1IsChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _option2IsChecked;
        public bool Option2IsChecked
        {
            get => _option2IsChecked;
            set
            {
                _option2IsChecked = value;
                OnPropertyChanged();
            }
        }

        private object _selectedOption;
        public object SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
