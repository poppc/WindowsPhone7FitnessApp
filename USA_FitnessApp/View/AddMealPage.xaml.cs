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

namespace USA_FitnessApp
{
    public partial class AddMealPage : PhoneApplicationPage
    {
        public AddMealPage()
        {
            InitializeComponent();

            DataContext = App.MealViewModel;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Meal m = new Meal(App.MealViewModel.MealToAdd);
            App.MealViewModel.Meals.Add(m);
            ObservableCollection<Meal> meals = new ObservableCollection<Meal>();
            meals.Add(m);
            App.MealViewModel.MainLogic.Sync.AddMealsToSync(meals);
            App.MealViewModel.MealToAdd = null;
            NavigationService.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            App.MealViewModel.MealToAdd = null;
            NavigationService.GoBack();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Meal mealToAdd = App.MealViewModel.MealToAdd;

            if (mealToAdd == null)
            {
                mealToAdd = new Meal();
                mealToAdd.MTime = (Meal.MealTime)mealTimePicker.SelectedItem;
                mealToAdd.Time = (DateTime)timePicker.Value;
                mealToAdd.FoodItems = new ObservableCollection<FoodItem>();
            }

            ObservableCollection<FoodItem> fis = mealToAdd.FoodItems;
            FoodItem fi = new FoodItem((FoodItem)foodItemPicker.SelectedItem);
            fi.Quantity = Double.Parse(quantityTxt.Text);
            fis.Add(fi);

            App.MealViewModel.MealToAdd = mealToAdd;
        }

        private void categoryPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foodItemPicker.ItemsSource = App.MealViewModel.FoodItemsByCategory[(FoodItemCategory)categoryPicker.SelectedItem];
        }

        private void foodItemPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            unitTxtBlock.Text = ((FoodItem)foodItemPicker.SelectedItem).Unit.ToString();
        }
    }
}