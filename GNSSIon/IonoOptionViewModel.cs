using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoFun.GNSS;

namespace GNSSIon
{
    public class IonoOptionViewModel:INotifyPropertyChanged
    {
        internal IonoOption option = new IonoOption();

        public string RootFolder
        {
            get => option.RootFolder;
            set
            {
                if (option.RootFolder != value)
                {
                    option.RootFolder = value;
                    OnPropertyChanged(nameof(RootFolder));
                }
            }
        }

        public enumSolType SolType
        {
            get => option.SolType;
            set
            {
                if (option.SolType != value)
                {
                    option.SolType = value;
                    OnPropertyChanged(nameof(SolType));
                }
            }
        }

        public double CutOffAngle
        {
            get => option.CutOffAngle;
            set
            {
                if (option.CutOffAngle != value)
                {
                    option.CutOffAngle = value;
                    OnPropertyChanged(nameof(CutOffAngle));
                }
            }
        }

        public int MinArcLength
        {
            get => option.MinArcLength;
            set
            {
                if (option.MinArcLength != value)
                {
                    option.MinArcLength = value;
                    OnPropertyChanged(nameof(MinArcLength));
                }
            }
        }

        public enumIonoModel IonoModelType
        {
            get => option.IonoModelType;
            set
            {
                if (option.IonoModelType != value)
                {
                    option.IonoModelType = value;
                    OnPropertyChanged(nameof(IonoModelType));
                }
            }
        }

        public List<string> OutputItems
        {
            get => option.OutputItems;
            set
            {
                if (option.OutputItems != value)
                {
                    option.OutputItems = value;
                    OnPropertyChanged(nameof(OutputItems));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
