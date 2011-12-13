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

namespace USA_FitnessApp.Model
{
    public abstract class StringEnum
    {
        private String desc;
        public String Desc
        {
            get
            {
                return desc;
            }

            set
            {
                desc = value;
            }
        }

        public override String ToString()
        {
            return desc;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }
            StringEnum t = (StringEnum)obj;

            return this.desc.Equals(t.desc);
        }

        public override int GetHashCode()
        {
            return desc.GetHashCode();
        }
    }
}
