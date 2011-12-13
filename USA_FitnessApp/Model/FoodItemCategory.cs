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
    public class FoodItemCategory : StringEnum
    {
        public static readonly FoodItemCategory BREAD = new FoodItemCategory { Desc="Bread" };

        public static readonly FoodItemCategory BEVERAGES = new FoodItemCategory { Desc = "Beverages" };

        public static readonly FoodItemCategory CONDIMENTS = new FoodItemCategory { Desc = "Condiments" };

        public static readonly FoodItemCategory DAIRY = new FoodItemCategory { Desc = "Dairy" };

        public static readonly FoodItemCategory FASTFOOD = new FoodItemCategory { Desc = "Fastfood" };

        public static readonly FoodItemCategory FRUIT = new FoodItemCategory { Desc = "Fruit" };

        public static readonly FoodItemCategory GRAIN = new FoodItemCategory { Desc = "Grain" };

        public static readonly FoodItemCategory MEAT = new FoodItemCategory { Desc = "Meat" };

        public static readonly FoodItemCategory OTHER_PROTEINS = new FoodItemCategory { Desc = "Other proteins" };

        public static readonly FoodItemCategory SEAFOOD = new FoodItemCategory { Desc = "Seafood" };

        public static readonly FoodItemCategory SWEETS = new FoodItemCategory { Desc = "Sweets" };

        public static readonly FoodItemCategory VEGETABLE = new FoodItemCategory { Desc = "Vegetable" };

        public static readonly List<FoodItemCategory> ALL_FOOD_ITEM_CATEGORIES = new List<FoodItemCategory> {
            BREAD, BEVERAGES, CONDIMENTS, DAIRY, FASTFOOD, FRUIT, GRAIN, MEAT, OTHER_PROTEINS, SEAFOOD, SWEETS, VEGETABLE };


        public static FoodItemCategory GetCategoryByString(String desc)
        {
            foreach (FoodItemCategory cat in FoodItemCategory.ALL_FOOD_ITEM_CATEGORIES)
            {
                if (cat.Desc.ToLower().Equals(desc.ToLower()))
                {
                    return cat;
                }
            }
            return null;
        }
    }
}
