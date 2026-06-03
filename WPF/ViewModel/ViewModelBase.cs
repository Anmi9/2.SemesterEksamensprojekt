using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private string _statusMessage = "";
        public string StatusMessage
        {
            get  => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}