using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training10.ViewModel
{
    public class HorseVM:ViewModelBase
    {
        string name;
        int speed;

        public HorseVM(string name, int speed)
        {
            this.Name = name;
            this.Speed = speed;
        }

        public string Name { get => name; set => name = value; }
        public int Speed { get => speed; set => speed = value; }
    }
}
