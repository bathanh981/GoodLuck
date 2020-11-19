using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model.Beam
{
   public class ChooseSteel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int quantity;
        private int phi;

        public int Phi { get => phi; set { phi = value; OnPropertyChanged(); } }
        public int Quantity { get => quantity; set { quantity = value; OnPropertyChanged(); } }

        public ChooseSteel()
        {
        }

        public ChooseSteel(int phi)
        {
            Phi = phi;
            Quantity = 2;
        }

        public ChooseSteel(int phi, int quantity) : this(phi)
        {
            Quantity = quantity;
        }

        public double As { get => Quantity * Math.PI * Math.Pow(Phi / 2, 2); }
    }
}
