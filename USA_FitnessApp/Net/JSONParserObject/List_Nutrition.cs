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

namespace USA_FitnessApp.Net.JSONParserObject
{
    public class List_Nutrition : ListResponse
    {
        public DateTime LastUpdate { get; set; }

        public List<FoodItem> Items { get; set; }
    }
}
