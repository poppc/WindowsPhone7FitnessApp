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
using System.Collections.ObjectModel;

namespace USA_FitnessApp.Model
{
    public class Meal : INotifyPropertyChanged
    {
        public class MealTime : StringEnum
        {
            public static readonly MealTime BREAKFAST = new MealTime { Desc = "Breakfast" };

            public static readonly MealTime LUNCH = new MealTime { Desc = "Lunch" };

            public static readonly MealTime DINNER = new MealTime { Desc = "Dinner" };

            public static readonly MealTime SNACK = new MealTime { Desc = "Snack" };

            public static readonly List<MealTime> ALL_MEAL_TIMES = new List<MealTime> { BREAKFAST, LUNCH, DINNER, SNACK };

            public static MealTime GetMealTime(String desc)
            {
                foreach(MealTime mt in ALL_MEAL_TIMES)
                {
                    if (mt.Desc.ToLower().Equals(desc.ToLower()))
                    {
                        return mt;
                    }
                }
                return null;
            }
        }


        //Empty constructor for XMLSerializer
        public Meal()
        {

        }

        //Copy constructor
        public Meal(Meal m)
        {
            mTime = m.mTime;
            Time = new DateTime(m.Time.Year, m.Time.Month, m.Time.Day, m.Time.Hour, m.Time.Minute, m.Time.Second);
            FoodItems = new ObservableCollection<FoodItem>();
            foreach(FoodItem f in m.FoodItems)
            {
                FoodItems.Add(new FoodItem(f));
            }
        }

        private MealTime mTime;
        public MealTime MTime
        {
            get
            {
                return mTime;
            }

            set
            {
                mTime = value;
                RaisePropertyChanged("MealTime");
            }
        }

        private DateTime time;
        public DateTime Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
                RaisePropertyChanged("Time");
            }
        }

        private ObservableCollection<FoodItem> foodItems;
        public ObservableCollection<FoodItem> FoodItems
        {
            get
            {
                return foodItems;
            }

            set
            {
                foodItems = value;
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
