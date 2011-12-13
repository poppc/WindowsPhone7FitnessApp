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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using USA_FitnessApp.Model;
using System.ComponentModel;
using USA_FitnessApp.Logic;
using System.Windows.Media.Imaging;
using USA_FitnessApp.Net.SyncObjects;
using USA_FitnessApp.Net.JSONParserObject;
using Microsoft.Phone.Controls;

namespace USA_FitnessApp.ViewModel
{
    public class MealViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Meal> meals;
        public ObservableCollection<Meal> Meals
        {
            get
            {
                if (meals == null)
                    meals = new ObservableCollection<Meal>();
                return meals;
            }

            set
            {
                meals = value;
            }
        }

        private ObservableCollection<Exercise> exercisesDone;
        public ObservableCollection<Exercise> ExercisesDone
        {
            get
            {
                if (exercisesDone == null)
                    exercisesDone = new ObservableCollection<Exercise>();
                return exercisesDone;
            }

            set
            {
                exercisesDone = value;
            }
        }

        public Preference Preference { get; set; }

        public List<MeasurementSystem> ALL_MEASUREMENT_SYSTEMS { get; private set; }

        public List<Goal.Type> ALL_GOAL_TYPES { get; private set; }

        public List<FoodItemCategory> ALL_FOOD_ITEM_CATEGORIES { get; private set; }

        public List<Meal.MealTime> ALL_MEAL_TIMES { get; private set; }

        private List<MealHistoryEntry> mealHistory;
        public List<MealHistoryEntry> MealHistory
        {
            get
            {
                return mealHistory;
            }

            set
            {
                mealHistory = value;
                RaisePropertyChanged("MealHistory");
            }
        }

        private String mealHistoryErrorTxt;
        public String MealHistoryErrorTxt
        {
            get
            {
                return mealHistoryErrorTxt;
            }

            set
            {
                mealHistoryErrorTxt = value;
                RaisePropertyChanged("MealHistoryErrorTxt");
            }
        }

        private List<USA_FitnessApp.Net.JSONParserObject.Track_Weight.JSONWeight> weightHistory;
        public List<USA_FitnessApp.Net.JSONParserObject.Track_Weight.JSONWeight> WeightHistory
        {
            get
            {
                return weightHistory;
            }

            set
            {
                weightHistory = value;
                RaisePropertyChanged("WeightHistory");
            }
        }

        private String weightHistoryErrorTxt;
        public String WeightHistoryErrorTxt
        {
            get
            {
                return weightHistoryErrorTxt;
            }

            set
            {
                weightHistoryErrorTxt = value;
                RaisePropertyChanged("WeightHistoryErrorTxt");
            }
        }

        private List<FitnessHistoryEntry> fitnessHistory;
        public List<FitnessHistoryEntry> FitnessHistory
        {
            get
            {
                return fitnessHistory;
            }

            set
            {
                fitnessHistory = value;
                RaisePropertyChanged("FitnessHistory");
            }
        }

        private String fitnessHistoryErrorTxt;
        public String FitnessHistoryErrorTxt
        {
            get
            {
                return fitnessHistoryErrorTxt;
            }

            set
            {
                fitnessHistoryErrorTxt = value;
                RaisePropertyChanged("FitnessHistoryErrorTxt");
            }
        }

        private Meal mealToAdd;
        public Meal MealToAdd
        {
            get
            {
                return mealToAdd;
            }

            set
            {
                mealToAdd = value;
                RaisePropertyChanged("MealToAdd");
            }
        }

        public Dictionary<FoodItemCategory, List<FoodItem>> FoodItemsByCategory { get; set; }

        private User user;
        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
                RaisePropertyChanged("User");
            }
        }

        private ImageSource statusImage;
        public ImageSource StatusImage
        {
            get
            {
                return statusImage;
            }

            set
            {
                statusImage = value;
                RaisePropertyChanged("StatusImage");
            }
        }

        public MainLogic MainLogic { get; set; }

        private AppStatus status;
        public AppStatus Status
        {
            get
            {
                if (status == null)
                    status = new AppStatus();

                return status;
            }
        }

        private int daysLeft;
        public int DaysLeft
        {
            get
            {
                return daysLeft;
            }

            set
            {
                daysLeft = value;
                RaisePropertyChanged("DaysLeft");
            }
        }

        public TextBox WeightBox { get; set; }

        public ListPicker MsPicker { get; set; }

        public List<Exercise> Exercises { get; set; }

        public List<String> Intensity { get; set; }

        public List<String> Genders { get; set; }

        private String registrationFailedTxt;
        public String RegistrationFailedTxt
        {
            get
            {
                return registrationFailedTxt;
            }

            set
            {
                registrationFailedTxt = value;
                RaisePropertyChanged("RegistrationFailedTxt");
            }
        }

        private String unitTxt;
        public String UnitTxt
        {
            get
            {
                return unitTxt;
            }

            set
            {
                unitTxt = value;
                RaisePropertyChanged("UnitTxt");
            }
        }


        public MealViewModel()
        {
            ALL_MEASUREMENT_SYSTEMS = MeasurementSystem.ALL_MEASUREMENT_SYSTEM;
            ALL_GOAL_TYPES = Goal.Type.ALL_GOAL_TYPES;
            ALL_FOOD_ITEM_CATEGORIES = FoodItemCategory.ALL_FOOD_ITEM_CATEGORIES;
            ALL_MEAL_TIMES = Meal.MealTime.ALL_MEAL_TIMES;
            Intensity = new List<String> { "Light", "Moderate", "Intense" };
            Genders = new List<String> { "Male", "Female" };
        }

        public void LoadUserData(User_Data ud)
        {
            User.Height = ud.Height;
            String unit = ud.Unit;
            if (unit.Equals("me"))
                unit = "metrical";
            Preference.MeasurementSystem = MeasurementSystem.GetMeasurementSystem(unit);
        }

        public void CalcDaysLeft()
        {
            DateTime today = DateTime.Now;
            today = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            User u = User;
            if (u != null && u.Goal != null && u.Goal.GoalWeight != null && u.Goal.GoalWeight.Date != null)
            {
                TimeSpan ts = u.Goal.GoalWeight.Date - today;
                DaysLeft = (int)ts.TotalDays;
            }
        }

        //Creates a dictionary by coping all fooditems from fis to FoodItemsByCategory where the category matches
        public void LoadFoodItemsByCategory(List<FoodItem> fis)
        {
            if (fis == null)
                return;

            if (FoodItemsByCategory == null)
                FoodItemsByCategory = new Dictionary<FoodItemCategory, List<FoodItem>>();

            foreach (FoodItemCategory fic in ALL_FOOD_ITEM_CATEGORIES)
            {
                FoodItemsByCategory.Add(fic, new List<FoodItem>());
            }

            foreach (FoodItem fi in fis)
            {
                foreach (FoodItemCategory cat in fi.Category)
                {
                    FoodItemsByCategory[cat].Add(new FoodItem(fi));
                }
            }
        }

        public void CreateMealHistory(List<FoodItemSync> track_diet)
        {
            if (track_diet == null || track_diet.Count < 1)
            {
                MealHistoryErrorTxt = "No hits found!";
                return;
            }

            List<MealHistoryEntry> mh = new List<MealHistoryEntry>();
            String lastTime = "";
            Meal.MealTime lastMt = null;
            List<MealHistoryEntry.MealTimeEntry> mhe = null;
            List<FoodItem> lfi = null;
            foreach(FoodItemSync fis in track_diet)
            {
                String time = fis.Date;
                if (!lastTime.Equals(time))
                {
                    mhe = new List<MealHistoryEntry.MealTimeEntry>();
                    mh.Add(new MealHistoryEntry { Date = time, MealTimeEntries = mhe });
                    lastTime = time;
                }
                
                Meal.MealTime mt = Meal.MealTime.GetMealTime(fis.Meal);
                if (lastMt == null || !lastMt.Equals(mt))
                {
                    lfi = new List<FoodItem>();
                    mhe.Add(new MealHistoryEntry.MealTimeEntry { MTime = mt, FoodItems = lfi });
                    lastMt = mt;
                }

                FoodItem fi = MainLogic.GetFoodItemById((fis.Id));
                if (fi != null)
                {
                    fi.Quantity = fis.Amount;
                    lfi.Add(fi);
                }
            }
            MealHistory = mh;
        }

        public void CreateFitnessHistory(List<ExerciseSync> track_fitness)
        {
            if (track_fitness == null || track_fitness.Count < 1)
            {
                FitnessHistoryErrorTxt = "No hits found!";
                return;
            }

            List<FitnessHistoryEntry> fh = new List<FitnessHistoryEntry>();
            List<Exercise> le = null;
            String lastTime = "";
            foreach (ExerciseSync es in track_fitness)
            {
                String time = es.Date;
                if (!lastTime.Equals(time))
                {
                    le = new List<Exercise>();
                    fh.Add(new FitnessHistoryEntry { Date = time, Exercises = le });
                    lastTime = time;
                }

                Exercise ex = MainLogic.GetExerciseById((es.Id));
                if (ex != null)
                {
                    ex.TimeFrame = es.TimeFrame;
                    ex.Reps = es.Amount;
                    ex.Intensity = es.Intensity;
                    le.Add(ex);
                }
            }
            FitnessHistory = fh;
        }

        public void CreateWeightHistory(List<USA_FitnessApp.Net.JSONParserObject.Track_Weight.JSONWeight> track_weight)
        {
            if (track_weight == null || track_weight.Count < 1)
            {
                WeightHistoryErrorTxt = "No hits found!";
                return;
            }

            foreach (USA_FitnessApp.Net.JSONParserObject.Track_Weight.JSONWeight w in track_weight)
            {
                w.WeightUnit = Preference.MeasurementSystem.BodyWeightUnit.ToString();
            }
            WeightHistory = track_weight;
        }

        //public bool IsDataLoaded
        //{
        //    get;
        //    private set;
        //}

        public void LoadData()
        {
            //Meals = new ObservableCollection<Meal>();
            //List<FoodItem> fi = new List<FoodItem>();

            ////Meal 1
            ////12 oz. chicken
            ////1 cup of egg whites
            ////1 cup of cream of rice
            //fi.Add(new FoodItem() { Name = "chicken", Quantity = 12, Unit = MeasurementSystem.MeasuringUnit.OZ, Calories = 120 });
            //fi.Add(new FoodItem() { Name = "egg whites", Quantity = 1, Unit = MeasurementSystem.MeasuringUnit.CUPS });
            //fi.Add(new FoodItem() { Name = "cream of rice", Quantity = 1, Unit = MeasurementSystem.MeasuringUnit.CUPS });
            //Meals.Add(new Meal() { Name = "Meal 1", FoodItems = fi });

            ////Meal 2
            ////12 oz. ground beef
            ////8 oz. whole wheat pasta
            //fi = new List<FoodItem>();
            //fi.Add(new FoodItem() { Name = "ground", Quantity = 12, Unit = MeasurementSystem.MeasuringUnit.OZ, Calories = 120 });
            //fi.Add(new FoodItem() { Name = "whole wheat pasta", Quantity = 8, Unit = MeasurementSystem.MeasuringUnit.OZ });
            //Meals.Add(new Meal() { Name = "Meal 2", FoodItems = fi });

            ////Meal 3
            ////8 oz. beef tenderloin
            ////10 oz. white potato
            ////1 cup of spinach
            //fi = new List<FoodItem>();
            //fi.Add(new FoodItem() { Name = "beef tenderloin", Quantity = 8, Unit = MeasurementSystem.MeasuringUnit.OZ, Calories = 120 });
            //fi.Add(new FoodItem() { Name = "white potato", Quantity = 10, Unit = MeasurementSystem.MeasuringUnit.OZ });
            //fi.Add(new FoodItem() { Name = "spinach", Quantity = 1, Unit = MeasurementSystem.MeasuringUnit.CUPS });
            //Meals.Add(new Meal() { Name = "Meal 3", FoodItems = fi });

            //this.IsDataLoaded = true;
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
