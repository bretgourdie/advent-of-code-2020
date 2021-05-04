using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day04
{
    class Passport
    {
        private Dictionary<string, string> attributes;

        private const int totalNumberOfAttributes = 8;

        public Passport()
        {
            attributes = new Dictionary<string, string>();
        }

        public void AddAttributes(string line)
        {
            foreach (var attributeDataPair in line.Split(' '))
            {
                var splitPair = attributeDataPair.Split(':');
                var attribute = splitPair[0];
                var value = splitPair[1];

                attributes[attribute] = value;
            }
        }

        public bool HasAttribute(string attribute)
        {
            return attributes.ContainsKey(attribute);
        }

        public string GetAttribute(string attribute)
        {
            return attributes[attribute];
        }

        public bool IsValid()
        {
            return attributes.Count == totalNumberOfAttributes
                ||
                (
                    attributes.Count == totalNumberOfAttributes - 1
                    && !HasAttribute("cid")
                );
        }
    }
}
