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

namespace USA_FitnessApp.Net.SyncObjects
{
    public class FoodItemSync
    {
        public String Date { get; set; }

        public int Id { get; set; }

        public double Amount { get; set; }

        public String Meal { get; set; }
    }
}
