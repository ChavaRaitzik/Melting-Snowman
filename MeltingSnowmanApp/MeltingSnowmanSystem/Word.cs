using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeltingSnowmanSystem
{
    public class Word : INotifyPropertyChanged
    {
        System.Drawing.Color _color;

        public event PropertyChangedEventHandler? PropertyChanged;
        public string WordValue { get; set; }

        public System.Drawing.Color Color
        {
            get => _color; set
            {
                _color = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged("BackColorMaui");
            }
        }

        public Microsoft.Maui.Graphics.Color ColorMAUI
        {
            get => this.ConvertToMauiColor(this.Color);
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
