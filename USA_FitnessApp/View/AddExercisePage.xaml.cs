using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using USA_FitnessApp.Model;
using System.Collections.ObjectModel;

namespace USA_FitnessApp.View
{
    public partial class AddExercisePage : PhoneApplicationPage
    {
        public AddExercisePage()
        {
            InitializeComponent();

            DataContext = App.MealViewModel;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (repsTxt.Text.Length > 0 || timeFrameTxt.Text.Length > 0)
            {
                Exercise exercise = new Exercise((Exercise)exercisePicker.SelectedItem);
                exercise.Date = (DateTime)datePicker.Value;
                try
                {
                    exercise.Reps = (int)Double.Parse(repsTxt.Text);
                }
                catch (Exception)
                {
                    exercise.Reps = 0;
                }
                try
                {
                    exercise.TimeFrame = (int)Double.Parse(timeFrameTxt.Text);
                }
                catch (Exception)
                {
                    exercise.TimeFrame = 0;
                }
                exercise.Intensity = (String)intensityPicker.SelectedItem;
                App.MealViewModel.ExercisesDone.Add(exercise);
                ObservableCollection<Exercise> ex = new ObservableCollection<Exercise>();
                ex.Add(exercise);
                App.MealViewModel.MainLogic.Sync.AddExercisesToSync(ex);
            }
            
            NavigationService.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}