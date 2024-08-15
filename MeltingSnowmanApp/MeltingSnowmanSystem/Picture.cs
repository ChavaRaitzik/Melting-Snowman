using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeltingSnowmanSystem
{
    public class Picture : INotifyPropertyChanged
    {
       
            string _picturevalue = "";

            public event PropertyChangedEventHandler? PropertyChanged;

            public string PictureValue
            {
                get => _picturevalue; set
                {
                    _picturevalue = value;
                    this.InvokePropertyChanged();
                }
            }
            private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
            }
    }
}
