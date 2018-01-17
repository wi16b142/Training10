using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using Training10.Com;

namespace Training10.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        HorseVM fastestHorse;
        ObservableCollection<HorseVM> horses;
        Server server;
        string newHorse = "";
        string newSpeed = "";
        public RelayCommand AddBtnClkick { get; set; }

        public MainViewModel()
        {
            Horses = new ObservableCollection<HorseVM>();
            fastestHorse = new HorseVM("", 0);

            AddBtnClkick = new RelayCommand(()=>
            {
                GuiUpdate(NewHorse+":"+NewSpeed);
            }, () =>
              {
                  if (NewHorse.Length > 0 && NewSpeed.Length > 0) return true;
                  return false;
            });

            if (IsInDesignMode)
            {

                Horses.Add(new HorseVM("Racer1", 100));
                Horses.Add(new HorseVM("Racer2", 66));
                Horses.Add(new HorseVM("Racer3", 300));
                Horses.Add(new HorseVM("Racer4", 44));
                Horses.Add(new HorseVM("Racer5", 180));
            }
            else
            {
                server = new Server(GuiUpdate);
            }

        }

        private void GuiUpdate(string msg)
        {
            App.Current.Dispatcher.Invoke(() => 
            {
                string name = msg.Split(':')[0];
                int speed = int.Parse(msg.Split(':')[1]);

                Horses.Add(new HorseVM(name, speed));
                UpdateFastestHorse();
            });
        }

        public ObservableCollection<HorseVM> Horses
        {
            get => horses; set
            {
                horses = value;
                RaisePropertyChanged();
            }
        }

        private void UpdateFastestHorse()
        {
            if (Horses.Count > 1)
            {
                foreach (var horse in Horses)
                {
                    if (fastestHorse.Speed < horse.Speed) fastestHorse = horse;
                }
            }
            if (Horses.Count == 1) fastestHorse = Horses[0];

            RaisePropertyChanged("FastestHorse");
        }

        public HorseVM FastestHorse
        {
            get
            {
                return fastestHorse;
            }

            set
            {
                fastestHorse = value;
                RaisePropertyChanged();
            }
        }

        public string NewHorse
        {
            get => newHorse; set
            {
                newHorse = value;
                RaisePropertyChanged();
            }
        }
        public string NewSpeed
        {
            get => newSpeed; set
            {
                newSpeed = value;
                RaisePropertyChanged();
            }
        }
       
    }
}