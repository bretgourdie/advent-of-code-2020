using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day04
{
    class Validation
    {
        public string Attribute { get; private set; }

        private Func<string, bool> validationFunction;

        public Validation(
            string attribute,
            Func<string, bool> validationFunction)
        {
            Attribute = attribute;
            this.validationFunction = validationFunction;
        }

        public bool IsValid(string value) => validationFunction(value);
    }
}
