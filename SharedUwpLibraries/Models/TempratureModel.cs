using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUwpLibraries.Models
{
    public class TemperatureModel
    {
        public class Temperature
        {
            public Main main { get; set; }
        }

        public class Main
        {
            public float temp { get; set; }
            public int humidity { get; set; }

        }
    }
}