using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
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