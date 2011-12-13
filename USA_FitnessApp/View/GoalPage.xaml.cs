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

namespace USA_FitnessApp.View
{
    public partial class GoalPage : PhoneApplicationPage
    {
        public GoalPage()
        {
            InitializeComponent();

            DataContext = App.MealViewModel;

            goalPicker.ItemsSource = App.MealViewModel.ALL_GOAL_TYPES;
            if (App.MealViewModel.Preference != null && App.MealViewModel.User.Goal != null && App.MealViewModel.User.Goal.GoalType != null)
                goalPicker.SelectedItem = App.MealViewModel.User.Goal.GoalType;

            User u = App.MealViewModel.User;
            if (u != null)
            {
                if (u.Weight != null && u.Weight.WeightValue > 0)
                    startWeightTxt.Text = u.Weight.WeightValue.ToString();

                if (u.Goal != null)
                {
                    Goal g = u.Goal;
                    if (g.GoalWeight != null && g.GoalWeight.WeightValue > 0)
                        goalWeightTxt.Text = g.GoalWeight.WeightValue.ToString();

                    if (g.StartWeight != null && g.StartWeight.WeightValue > 0)
                        startWeightTxt.Text = g.StartWeight.WeightValue.ToString();    
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            User u = App.MealViewModel.User;
           
            if (goalWeightTxt.Text.Length > 0)
            {
                if (u.Goal == null)
                {
                    u.Goal = new Goal();
                }
                u.Goal.GoalType = (Goal.Type)goalPicker.SelectedItem;
                if (u.Goal.GoalWeight == null)
                    u.Goal.GoalWeight = new Weight();
                u.Goal.GoalWeight.Date = (DateTime)goalDatePicker.Value;
                u.Goal.GoalWeight.WeightValue = Double.Parse(goalWeightTxt.Text);
                if (u.Goal.StartWeight == null)
                    u.Goal.StartWeight = new Weight();
                u.Goal.StartWeight.Date = DateTime.Now;
                //TODO Heutiges Gewicht noch nicht gesetzt
                u.Goal.StartWeight.WeightValue = u.Weight.WeightValue;
                App.MealViewModel.CalcDaysLeft();
            }

            NavigationService.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}