using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace USA_FitnessApp.Model
{
    public class User : INotifyPropertyChanged
    {
        //Empty constructor for XMLSerializer
        public User()
        {
            Weight = new Weight();
        }

        private DateTime bday;
        public DateTime Bday
        {
            get
            {
                return bday;
            }

            set
            {
                bday = value;
                RaisePropertyChanged("Bday");
            }
        }

        private Weight weight;
        public Weight Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
                RaisePropertyChanged("Weight");
            }
        }

        public Boolean WeightSyncedToday { get; set; }

        private Boolean needsToSync;
        public Boolean NeedsToSync
        {
            get
            {
                return needsToSync;
            }

            set
            {
                needsToSync = value;
            }
        }

        private Double height;
        public Double Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
                RaisePropertyChanged("Height");
            }
        }

        //TODO Better dataType
        private Boolean gender;
        public Boolean Gender
        {
            get
            {
                return gender;
            }

            set
            {
                gender = value;
                RaisePropertyChanged("Gender");
            }
        }

        private Double bmi;
        public Double Bmi
        {
            get
            {
                return bmi;
            }

            set
            {
                bmi = value;
                RaisePropertyChanged("Bmi");
            }
        }

        private Goal goal;
        public Goal Goal
        {
            get
            {
                return goal;
            }

            set
            {
                goal = value;
                RaisePropertyChanged("Goal");
            }
        }

        public void CalcBmi()
        {
            if (Height > 0 && Weight != null && Weight.WeightValue > 0)
            {
                Bmi = Math.Round(Weight.GetWeightInKg() / ((Height * Height)/10000), 2);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
