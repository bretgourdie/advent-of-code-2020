using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day07
{
    class BagTree
    {
        IDictionary<string, IList<BagEdge>> colorToEdges;

        public BagTree(IList<string> inputData)
        {
            colorToEdges = new Dictionary<string, IList<BagEdge>>();

            foreach (var line in inputData)
            {
                var parentColor = getParentColor(line);
                var childrenStrings = getChildrenStrings(line);

                foreach (var childString in childrenStrings)
                {
                    addEdge(
                        new BagEdge(
                            getChildBagCount(childString),
                            parentColor,
                            getChildColor(childString)
                        )
                    );
                }
            }
        }

        public IEnumerable<BagEdge> GetEdges(
            string color)
        {
            return colorToEdges[color];
        }

        public IEnumerable<string> GetParents()
        {
            return colorToEdges.Keys;
        }

        private void addEdge(
            BagEdge bagEdge)
        {
            if (!colorToEdges.ContainsKey(bagEdge.FromColor))
            {
                colorToEdges[bagEdge.FromColor] = new List<BagEdge>();
            }

            colorToEdges[bagEdge.FromColor].Add(bagEdge);
        }

        private string getChildColor(string childString)
        {
            if (isLeafChild(childString)) return null;
            return childString.Substring(childString.IndexOf(" ") + 1);
        }

        private int getChildBagCount(string childString)
        {
            if (isLeafChild(childString)) return 0;
            return int.Parse(childString.Split(' ')[0]);
        }

        private bool isLeafChild(string childString)
        {
            return childString.Contains("no other");
        }

        private string getParentColor(string line)
        {
            return noBagWord(
                splitParentFromChildrenStrings(line)[0]
            );
        }

        private string noBagWord(string colorAndBagPhrase)
        {
            var noBagWord = colorAndBagPhrase
                .Substring(
                    0,
                    colorAndBagPhrase.IndexOf("bag"))
                .TrimEnd();
            return noBagWord;
        }

        private IEnumerable<string> getChildrenStrings(string line)
        {
            var childrenSection = splitParentFromChildrenStrings(line)[1];

            return childrenSection.Split(
                    new string[] {", "},
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(x =>
                    noBagWord(x));
                
        }

        private string[] splitParentFromChildrenStrings(string line)
        {
            return line.Split(
                new string[] {" contain "},
                StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
