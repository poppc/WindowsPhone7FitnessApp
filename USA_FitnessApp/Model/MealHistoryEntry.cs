﻿using System;
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
    public class MealHistoryEntry
    {
        public class MealTimeEntry
        {
            public USA_FitnessApp.Model.Meal.MealTime MTime { get; set; }

            public List<FoodItem> FoodItems { get; set; }
        }

        public String Date { get; set; }

        public List<MealTimeEntry> MealTimeEntries { get; set; }
    }
}