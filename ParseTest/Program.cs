using System;
using System.Globalization;
using ControlLibrary.Services;
using ControlLibrary.Services.Interfaces;

namespace ParseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IParser parser = new FrequencyParser();

            var value = parser.Parse("123421,   214   М   ");

        }
    }
}
