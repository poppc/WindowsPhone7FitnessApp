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
    public class Weight : INotifyPropertyChanged
    {
        private static readonly Double KG_TO_LBS = 2.20462262;

        // Stored in kg
        private Double weightValue;
        public Double WeightValue
        {
            get
            {
                String ms = App.MealViewModel.Preference.MeasurementSystem.Desc;
                if (ms.Equals(MeasurementSystem.US.Desc))
                {
                    return Math.Round(weightValue * KG_TO_LBS, 0);
                }

                return Math.Round(weightValue, 1);
            }

            set
            {
                if (App.MealViewModel.Preference != null)
                {
                    String ms = App.MealViewModel.Preference.MeasurementSystem.Desc;
                    if (ms.Equals(MeasurementSystem.US.Desc))
                    {
                        weightValue = value / KG_TO_LBS;
                    }
                    else
                    {
                        weightValue = value;
                    }
                    RaisePropertyChanged("WeightValue");
                }
            }
        }

        public Double GetWeightInKg()
        {
            return weightValue;
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
                RaisePropertyChanged("Date");
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
