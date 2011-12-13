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
    public class MeasurementSystem : StringEnum
    {
        public class MeasuringUnit : StringEnum
        {
            public static readonly MeasuringUnit CUPS = new MeasuringUnit { Desc = "cups" };

            public static readonly MeasuringUnit OZ = new MeasuringUnit { Desc = "oz" };

            public static readonly MeasuringUnit FL_OZ = new MeasuringUnit { Desc = "fl oz" };

            public static readonly MeasuringUnit LBS = new MeasuringUnit { Desc = "lbs" };

            public static readonly MeasuringUnit G = new MeasuringUnit { Desc = "g" };

            public static readonly MeasuringUnit KG = new MeasuringUnit { Desc = "kg" };

            public static readonly MeasuringUnit ML = new MeasuringUnit { Desc = "ml" };

            public static readonly List<MeasuringUnit> ALL_MEASURING_UNITS= new List<MeasuringUnit> { CUPS, OZ, FL_OZ, LBS, G, KG, ML};
        }


        public static readonly MeasurementSystem US = new MeasurementSystem { Desc = "US",
            Units = new List<MeasuringUnit> { MeasuringUnit.CUPS, MeasuringUnit.OZ, MeasuringUnit.FL_OZ, MeasuringUnit.LBS }, BodyWeightUnit = MeasuringUnit.LBS };

        public static readonly MeasurementSystem METRICAL = new MeasurementSystem { Desc = "Metrical",
            Units = new List<MeasuringUnit> { MeasuringUnit.G, MeasuringUnit.KG, MeasuringUnit.ML }, BodyWeightUnit = MeasuringUnit.KG };

        public static readonly List<MeasurementSystem> ALL_MEASUREMENT_SYSTEM = new List<MeasurementSystem> { US, METRICAL};


        private List<MeasuringUnit> units;
        public List<MeasuringUnit> Units
        {
            get
            {
                return units;
            }

            set
            {
                units = value;
            }
        }

        private MeasuringUnit bodyWeightUnit;
        public MeasuringUnit BodyWeightUnit
        {
            get
            {
                return bodyWeightUnit;
            }

            set
            {
                bodyWeightUnit = value;
            }
        }

        public MeasuringUnit GetUnit(String desc)
        {
            foreach(MeasuringUnit unit in units)
            {
                if (unit.Desc.ToLower().Equals(desc.ToLower()))
                {
                    return unit;
                }
            }
            return null;
        }

        public static MeasuringUnit GetUnitByString(String desc)
        {
            foreach (MeasuringUnit unit in MeasuringUnit.ALL_MEASURING_UNITS)
            {
                if (unit.Desc.ToLower().Equals(desc.ToLower()))
                {
                    return unit;
                }
            }
            return null;
        }

        public static MeasurementSystem GetMeasurementSystem(String desc)
        {
            foreach (MeasurementSystem ms in ALL_MEASUREMENT_SYSTEM)
            {
                if (ms.Desc.ToLower().Equals(desc.ToLower()))
                {
                    return ms;
                }
            }
            return null;
        }
    }
}
