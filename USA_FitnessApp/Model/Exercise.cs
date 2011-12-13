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

namespace USA_FitnessApp.Model
{
    public class Exercise
    {
        public class ExerciseType : StringEnum
        {
            public static readonly ExerciseType CARDIO = new ExerciseType { Desc = "Cardio" };

            public static readonly ExerciseType OTHER = new ExerciseType { Desc = "Other" };

            public static readonly List<ExerciseType> ALL_EXERCISE_TYPES = new List<ExerciseType> { CARDIO, OTHER };


            public static ExerciseType GetExerciseType(String desc)
            {
                foreach (ExerciseType et in ALL_EXERCISE_TYPES)
                {
                    if (et.Desc.ToLower().Equals(desc.ToLower()))
                    {
                        return et;
                    }
                }
                return null;
            }
        }

        public int Id { get; set; }

        public String Name { get; set; }

        public int Light_Cal { get; set; }

        public int Moderate_Cal { get; set; }

        public int Intense_Cal { get; set; }

        private List<String> e_Type;
        public List<String> E_Type
        {
            get
            {
                return e_Type;
            }

            set
            {
                e_Type = value;
                if (ExerciseTypes == null)
                    ExerciseTypes = new List<ExerciseType>();
                foreach (String s in value)
                {
                    ExerciseTypes.Add(ExerciseType.GetExerciseType(s));
                }
            }
        }

        public List<ExerciseType> ExerciseTypes { get; set; }

        public DateTime Date { get; set; }

        public int Reps { get; set; }

        public int TimeFrame { get; set; }

        public String Intensity { get; set; }

        public Exercise()
        {

        }

        //Copy of a basic exercise to be used for a done exercise
        public Exercise(Exercise e)
        {
            Id = e.Id;
            Name = e.Name;
            Light_Cal = e.Light_Cal;
            Moderate_Cal = e.Moderate_Cal;
            Intense_Cal = e.Intense_Cal;
            E_Type = new List<String>(); //Sets ExerciseTypes implicit
            foreach (String s in e.E_Type)
            {
                E_Type.Add(s);
            }
        }

        public override String ToString()
        {
            return Name;
        }
    }
}
