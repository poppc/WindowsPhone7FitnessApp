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
using System.IO;
using System.Xml.Serialization;
using USA_FitnessApp.Model;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using USA_FitnessApp.Net.SyncObjects;

namespace USA_FitnessApp.Persistence
{
    public class XMLSerializer
    {
        private static Dictionary<Type, String> path = new Dictionary<Type,string>
        {
            { typeof(Preference), "Preference.xml" },
            { typeof(User), "User.xml" },
            { typeof(List<FoodItem>), "FoodItems.xml" },
            { typeof(List<Exercise>), "Exercises.xml" },
            { typeof(List<Weight>), "WeightCache.xml" },
            { typeof(List<FoodItemSync>), "MealCache.xml" },
            { typeof(List<ExerciseSync>), "ExerciseCache.xml" },
            { typeof(ObservableCollection<Exercise>), "ExercisesDone.xml" },
            { typeof(ObservableCollection<Meal>), "Meals.xml" }
        };
        

        public T Load<T>(Type type)
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            T t = default(T);
            if (store.FileExists(path[type]))
            {
                IsolatedStorageFileStream fs = store.OpenFile(path[type], FileMode.Open, FileAccess.Read);
                t = (T)Deserialize(fs, type);
                fs.Close();
                store.DeleteFile(path[type]);
            }
            return t;
        }

        public void Save<T>(T t)
        {
            if (t == null)
                return;
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();

            Serialize(store.OpenFile(path[t.GetType()], FileMode.OpenOrCreate, FileAccess.Write), t);
        }

        private void Serialize(Stream stream, Object obj)
        {
            if (stream == null || obj == null)
            {
                return; //TODO Exception
            }

            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stream, obj);
        }

        private Object Deserialize(Stream stream, Type type)
        {
            if (stream == null || type == null)
            {
                return null; //TODO Exception
            }

            XmlSerializer serializer = new XmlSerializer(type);
            return serializer.Deserialize(stream);
        }
    }
}
