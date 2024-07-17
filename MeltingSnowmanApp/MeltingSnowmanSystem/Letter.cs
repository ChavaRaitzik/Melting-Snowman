using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeltingSnowmanSystem
{
    public class Letter : INotifyPropertyChanged
    {
        private string _lettervalue = "";
        System.Drawing.Color _backcolor;


        public event PropertyChangedEventHandler? PropertyChanged;

        public string LetterValue 
        { 
            get => _lettervalue; set 
            { 
                _lettervalue = value;
                this.InvokePropertyChanged();
            } 
        }
        public System.Drawing.Color BackColor
        {
            get => _backcolor; set
            {
                _backcolor = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged("BackColorMaui");
            }
        }

        public Microsoft.Maui.Graphics.Color BackColorMaui
        {
            get => this.ConvertToMauiColor(this.BackColor);
        }

        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public Microsoft.Maui.Graphics.Color ConvertToMauiColor(System.Drawing.Color systemColor)
        {
            float red = systemColor.R / 255f;
            float green = systemColor.G / 255f;
            float blue = systemColor.B / 255f;
            float alpha = systemColor.A / 255f;

            return new Microsoft.Maui.Graphics.Color(red, green, blue, alpha);
        }
    }   
}
