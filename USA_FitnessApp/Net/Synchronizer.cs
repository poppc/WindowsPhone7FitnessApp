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
using System.Collections.Generic;
using USA_FitnessApp.Model;
using USA_FitnessApp.Persistence;
using System.Collections.ObjectModel;
using USA_FitnessApp.Net.SyncObjects;
using USA_FitnessApp.Logic;

namespace USA_FitnessApp.Net
{
    public class Synchronizer
    {
        private List<FoodItemSync> fiSync;

        private List<ExerciseSync> exerciseSync;

        private List<Weight> weights;

        private MainLogic mainLogic;

        private Communication com;

        private AppStatus status;


        public Synchronizer(MainLogic mainLogic, Communication com)
        {
            this.mainLogic = mainLogic;
            this.com = com;
            status = App.MealViewModel.Status;
            loadCachedData();
        }

        private void loadCachedData()
        {
            //FoodItems
            fiSync = mainLogic.Load<List<FoodItemSync>>(typeof(List<FoodItemSync>));
            if (fiSync == null)
            {
                fiSync = new List<FoodItemSync>();
            }
            else
            {
                if(fiSync.Count > 0)
                    status.NeedsToSync();
            }

            //Exercises
            exerciseSync = mainLogic.Load<List<ExerciseSync>>(typeof(List<ExerciseSync>));
            if (exerciseSync == null)
            {
                exerciseSync = new List<ExerciseSync>();
            }
            else
            {
                if (exerciseSync.Count > 0)
                    status.NeedsToSync();
            }

            //Weights
            weights = mainLogic.Load<List<Weight>>(typeof(List<Weight>));
            if (weights == null)
            {
                weights = new List<Weight>();
            }
            else
            {
                if (weights.Count > 0)
                    status.NeedsToSync();
            }
        }

        public void storeCacheData()
        {
            //FoodItems
            mainLogic.Save<List<FoodItemSync>>(fiSync);

            //Exercises
            mainLogic.Save<List<ExerciseSync>>(exerciseSync);

            //Weights
            mainLogic.Save<List<Weight>>(weights);
        }

        public void AddMealsToSync(ObservableCollection<Meal> c)
        {
            if(c == null)
            {
                //TODO Handle Exception
                return;
            }

            if (c.Count > 0)
                status.NeedsToSync();

            foreach (Meal m in c)
            {
                String date = m.Time.ToString("yyyy-MM-dd");
                String meal = m.MTime.ToString().ToLower();
                foreach(FoodItem fi in m.FoodItems)
                {
                    fiSync.Add(new FoodItemSync { Date = date, Id = fi.Id, Amount = fi.Quantity, Meal = meal });
                }
            }
        }

        public void AddExercisesToSync(ObservableCollection<Exercise> exercises)
        {
            if (exercises == null)
            {
                //TODO Handle Exception
                return;
            }

            if (exercises.Count > 0)
                status.NeedsToSync();

            foreach (Exercise e in exercises)
            {
                String date = e.Date.ToString("yyyy-MM-dd");
                exerciseSync.Add(new ExerciseSync { Date = date, Id = e.Id, TimeFrame = e.TimeFrame, Amount = e.Reps, Intensity = e.Intensity.ToLower() });
            }
        }

        public void AddWeightToSync(Weight w)
        {
            weights.Add(w);
            status.NeedsToSync();
        }

        public void Syncronize()
        {
            if (fiSync.Count > 0)
            {
                com.UpdateDiet(fiSync);
                fiSync = new List<FoodItemSync>();
            }

            if (exerciseSync.Count > 0)
            {
                com.UpdateFitness(exerciseSync);
                exerciseSync = new List<ExerciseSync>();
            }
            if (weights.Count > 0)
            {
                com.UpdateWeight(weights);
                weights = new List<Weight>();
            }
        }
    }
}
