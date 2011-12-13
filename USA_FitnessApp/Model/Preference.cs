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
    public class Preference : INotifyPropertyChanged
    {
        private String username;
        public String Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
                RaisePropertyChanged("Username");
            }
        }

        private String password;
        public String Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }
        
        private MeasurementSystem measurementSystem;
        public MeasurementSystem MeasurementSystem
        {
            get
            {
                return measurementSystem;
            }

            set
            {
                //Get/Set Weights to trigger a convertion of all weights
                if (measurementSystem != null && !measurementSystem.Equals(value))
                {
                    measurementSystem = value;
                    if (App.MealViewModel.User != null)
                    {
                        User u = App.MealViewModel.User;
                        if (u.Weight != null)
                        {
                            App.MealViewModel.WeightBox.Text = u.Weight.WeightValue.ToString();
                            //Weight w = u.Weight;
                            //w.WeightValue = w.WeightValue;
                        }
                        if (u.Goal != null)
                        {
                            Goal g = u.Goal;
                            if (g.StartWeight != null)
                            {
                                Weight w = g.StartWeight;
                                w.WeightValue = w.WeightValue;
                            }
                            if (g.GoalWeight != null)
                            {
                                Weight w = g.GoalWeight;
                                w.WeightValue = w.WeightValue;
                            }
                        }
                    }
                }
                measurementSystem = value;
                RaisePropertyChanged("MeasurementSystem");
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
