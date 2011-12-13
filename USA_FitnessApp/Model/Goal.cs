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
using System.Collections.Generic;

namespace USA_FitnessApp.Model
{
    public class Goal : INotifyPropertyChanged
    {
        public class Type : StringEnum
        {
            public static readonly Type LOSE_WEIGHT = new Type { Desc = "Lose Weight" };

            public static readonly Type MAINTAIN_WEIGHT = new Type { Desc = "Maintain Weight" };

            public static readonly Type BUILD_MUSCLE = new Type { Desc = "Build Muscle" };

            public static readonly List<Goal.Type> ALL_GOAL_TYPES = new List<Goal.Type> { LOSE_WEIGHT, MAINTAIN_WEIGHT, BUILD_MUSCLE };
        }


        private Type goalType;
        public Type GoalType
        {
            get
            {
                return goalType;
            }

            set
            {
                goalType = value;
                RaisePropertyChanged("GoalType");
            }
        }

        private Weight startWeight;
        public Weight StartWeight
        {
            get
            {
                return startWeight;
            }

            set
            {
                startWeight = value;
                RaisePropertyChanged("StartWeight");
            }
        }

        private Weight goalWeight;
        public Weight GoalWeight
        {
            get
            {
                return goalWeight;
            }

            set
            {
                goalWeight = value;
                RaisePropertyChanged("GoalWeight");
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
