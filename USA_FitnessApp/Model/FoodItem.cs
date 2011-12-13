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
using System.ComponentModel;
using System.Collections.Generic;

namespace USA_FitnessApp.Model
{
    public class FoodItem : INotifyPropertyChanged
    {
        //Empty constructor for XMLSerializer
        public FoodItem()
        {

        }

        //Copy constructor
        public FoodItem(FoodItem fi)
        {
            Id = fi.Id;
            Name = fi.Name;
            Category = new List<FoodItemCategory>();
            foreach (FoodItemCategory cat in fi.Category)
            {
                Category.Add(cat);
            }
            US_Serving = fi.US_Serving;
            Quantity = fi.Quantity;
            Unit = fi.Unit;
            Calories = fi.Calories;
            Protein = fi.Protein;
            Carbohydrates = fi.Carbohydrates;
            Sugar = fi.Sugar;
            Fat_sat = fi.Fat_sat;
            Fat_trans = fi.Fat_trans;
            Fat_poly = fi.Fat_poly;
            Cholesterol = fi.Cholesterol;
            Sodium = fi.Sodium;
            Type = new List<String>();
            foreach (String s in fi.Type)
            {
                Type.Add(s);
            }
        }

        private int id;
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        
        private String name;
        public String Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        private List<FoodItemCategory> category;
        public List<FoodItemCategory> Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
                RaisePropertyChanged("Category");
            }
        }

        /// <summary>
        /// Property just for the server communication.
        /// Extracts the string and sets Quantity and Unit
        /// </summary>
        private String us_serving;
        public String US_Serving
        {
            get
            {
                return us_serving;
            }

            set
            {
                us_serving = value;
                if (value.Length > 0)
                {
                    int divider = value.IndexOf(" ");
                    Quantity = Double.Parse(value.Substring(0, divider));
                    Unit = MeasurementSystem.GetUnitByString(value.Substring(divider+1));
                }
            }
        }

        private Double quantity;
        public Double Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
                RaisePropertyChanged("Quantity");
            }
        }

        private MeasurementSystem.MeasuringUnit unit;
        public MeasurementSystem.MeasuringUnit Unit
        {
            get
            {
                return unit;
            }

            set
            {
                unit = value;
                RaisePropertyChanged("Unit");
            }
        }

        private Double calories;
        public Double Calories
        {
            get
            {
                return calories;
            }

            set
            {
                calories = value;
                RaisePropertyChanged("Calories");
            }
        }

        private Double protein;
        public Double Protein
        {
            get
            {
                return protein;
            }

            set
            {
                protein = value;
                RaisePropertyChanged("Protein");
            }
        }

        private Double carbohydrates;
        public Double Carbohydrates
        {
            get
            {
                return carbohydrates;
            }

            set
            {
                carbohydrates = value;
                RaisePropertyChanged("Carbs");
            }
        }

        private Double sugar;
        public Double Sugar
        {
            get
            {
                return sugar;
            }

            set
            {
                sugar = value;
                RaisePropertyChanged("Sugar");
            }
        }

        private Double fat_sat;
        public Double Fat_sat
        {
            get
            {
                return fat_sat;
            }

            set
            {
                fat_sat = value;
                RaisePropertyChanged("SatFat");
            }
        }

        private Double fat_trans;
        public Double Fat_trans
        {
            get
            {
                return fat_trans;
            }

            set
            {
                fat_trans = value;
                RaisePropertyChanged("TransFat");
            }
        }

        private Double fat_poly;
        public Double Fat_poly
        {
            get
            {
                return fat_poly;
            }

            set
            {
                fat_poly = value;
                RaisePropertyChanged("PolyFat");
            }
        }

        private Double cholesterol;
        public Double Cholesterol
        {
            get
            {
                return cholesterol;
            }

            set
            {
                cholesterol = value;
                RaisePropertyChanged("Choles");
            }
        }

        private Double sodium;
        public Double Sodium
        {
            get
            {
                return sodium;
            }

            set
            {
                sodium = value;
                RaisePropertyChanged("Sodium");
            }
        }

        /// <summary>
        /// Property just for the server communication.
        /// Converts the string into a FoodCategory
        /// </summary>
        private List<String> type;
        public List<String> Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
                if (Category == null)
                    Category = new List<FoodItemCategory>();
                foreach(String s in value)
                {
                    Category.Add(FoodItemCategory.GetCategoryByString(s));
                }
            }
        }


        public override String ToString()
        {
            return name;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
