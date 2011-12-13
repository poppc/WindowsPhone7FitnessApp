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
using System.IO;
using USA_FitnessApp.Persistence;
using USA_FitnessApp.Model;
using System.Windows.Navigation;
using System.Collections.Generic;
using USA_FitnessApp.Net;
using System.Collections.ObjectModel;
using USA_FitnessApp.Net.JSONParserObject;

namespace USA_FitnessApp.Logic
{
    public class MainLogic
    {
        private XMLSerializer ser = new XMLSerializer();

        private Communication com;

        private Synchronizer sync;
        public Synchronizer Sync
        {
            get
            {
                return sync;
            }

            private set
            {
                sync = value;
            }
        }

        private List<FoodItem> fis;
        public List<FoodItem> Fis
        {
            get
            {
                return fis;
            }

            set
            {
                fis = value;
                App.MealViewModel.LoadFoodItemsByCategory(fis);
            }
        }

        private List<Exercise> exercises;
        public List<Exercise> Exercises
        {
            get
            {
                return exercises;
            }

            set
            {
                exercises = value;
                App.MealViewModel.Exercises = exercises;
            }
        }

        public MainLogic()
        {
            App.MealViewModel.MainLogic = this;
            com = new Communication(this);
            sync = new Synchronizer(this, com);
        }

        public void LoadData()
        {
            DateTime today = DateTime.Now;
            today = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);

            //Preferences
            Preference p = ser.Load<Preference>(typeof(Preference));
            if (p == null)
            {
                p = new Preference();
                p.MeasurementSystem = MeasurementSystem.US;
                //No LoginCredentials available. Display Error
                App.MealViewModel.Status.LogInRequired();
            }
            else
            {
                if (p.Username != null && p.Username.Length > 0 && p.Password != null && p.Password.Length > 0)
                {
                    Login(p.Username, p.Password);
                }
                else
                {
                    //No LoginCredentials available. Display Error
                    App.MealViewModel.Status.LogInRequired();
                }
            }
            App.MealViewModel.Preference = p;

            //User
            User u = ser.Load<User>(typeof(User));
            if (u == null)
            {
                u = new User();
                com.ReceiveUserData();
            }
            else
            {
                if (u.NeedsToSync)
                    App.MealViewModel.Status.NeedsToSync();

                if (u.Weight != null && today > u.Weight.Date && u.Weight.WeightValue > 0)
                {
                    //sync.AddWeightToSync(u.Weight);
                    u.Weight = new Weight();
                    u.WeightSyncedToday = false;
                }
            }
            App.MealViewModel.User = u;
            App.MealViewModel.CalcDaysLeft();

            //TODO Load Goal

            //FoodItems
            Fis = ser.Load<List<FoodItem>>(typeof(List<FoodItem>));
            if (fis == null)
            {
                com.ReceiveFoodItems();
            }

            //Exercises
            Exercises = ser.Load<List<Exercise>>(typeof(List<Exercise>));
            if (Exercises == null)
            {
                com.ReceiveExercises();
            }

            //ExercisesDone
            ObservableCollection<Exercise> exercisesDone = ser.Load<ObservableCollection<Exercise>>(typeof(ObservableCollection<Exercise>));
            if (exercisesDone == null || exercisesDone.Count <= 0 || exercisesDone[0] == null)
            {
                App.MealViewModel.ExercisesDone = new ObservableCollection<Exercise>();
            }
            else
            {
                if (exercisesDone[0].Date < today)
                {
                    //sync.AddExercisesToSync(exercisesDone);
                    App.MealViewModel.ExercisesDone = new ObservableCollection<Exercise>();
                }
                else
                {
                    App.MealViewModel.ExercisesDone = exercisesDone;
                }
            }

            //TODO Load Meals from server?
            //Meals
            ObservableCollection<Meal> meals = ser.Load<ObservableCollection<Meal>>(typeof(ObservableCollection<Meal>));
            if (meals == null || meals.Count <= 0 || meals[0] == null)
            {
                App.MealViewModel.Meals = new ObservableCollection<Meal>();
            }
            else
            {
                if (meals[0].Time < today)
                {
                    //sync.AddMealsToSync(meals);
                    App.MealViewModel.Meals = new ObservableCollection<Meal>();
                }
                else
                {
                    App.MealViewModel.Meals = meals;
                }
            }
        }

        public void saveData()
        {
            //User
            App.MealViewModel.User.NeedsToSync = App.MealViewModel.Status.IsReadyToSync();
            ser.Save<User>(App.MealViewModel.User);

            //Preferences
            ser.Save<Preference>(App.MealViewModel.Preference);

            //FoodItems
            ser.Save<List<FoodItem>>(fis);

            //Exercises
            ser.Save<List<Exercise>>(exercises);

            //Meals
            ser.Save<ObservableCollection<Meal>>(App.MealViewModel.Meals);

            //ExercisesDone
            ser.Save<ObservableCollection<Exercise>>(App.MealViewModel.ExercisesDone);

            //Cache
            sync.storeCacheData();
        }

        public void Syncronize()
        {
            sync.Syncronize();
        }

        public T Load<T>(Type type)
        {
            return ser.Load<T>(type);
        }

        public void Save<T>(T t)
        {
            ser.Save<T>(t);
        }

        public void RequestMealHistory(DateTime start, DateTime end)
        {
            com.TrackDiet(start, end);
        }

        public void RequestFitnessHistory(DateTime start, DateTime end)
        {
            com.TrackFitness(start, end);
        }

        public void RequestWeightHistory(DateTime start, DateTime end)
        {
            com.TrackWeight(start, end);
        }

        public void Login(String user, String password)
        {
            com.Login(user, password);
        }

        public FoodItem GetFoodItemById(int id)
        {
            if (fis == null)
                return null;

            foreach (FoodItem fi in fis)
            {
                if (fi.Id == id)
                {
                    return fi;
                }
            }
            return null;
        }

        public Exercise GetExerciseById(int id)
        {
            if (exercises == null)
                return null;

            foreach (Exercise e in exercises)
            {
                if (e.Id == id)
                {
                    return e;
                }
            }
            return null;
        }

        public void Register(Registration reg)
        {
            com.Register(reg);
        }
    }
}
