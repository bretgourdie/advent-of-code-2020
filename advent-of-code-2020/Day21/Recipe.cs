using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day21
{
    class Recipe
    {
        public readonly HashSet<string> Ingredients;
        public readonly List<string> Allergens;
        public Recipe(string line)
        {
            var theSplit = line.Split(new [] {" ("}, StringSplitOptions.RemoveEmptyEntries);

            Ingredients = new HashSet<string>(theSplit[0].Split(' '));
            if (theSplit.Length > 1)
            {
                Allergens = new List<string>(handleAllergens(theSplit[1]));
            }

            else
            {
                Allergens = new List<string>();
            }
        }

        public Recipe(IEnumerable<string> ingredients, IEnumerable<string> allergens)
        {
            Ingredients = new HashSet<string>(ingredients);
            Allergens = new List<string>(allergens);
        }

        private string[] handleAllergens(string allergenSegment)
        {
            var list = new List<string>();
            var allergens = allergenSegment.Split(new [] {", "}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var allergen in allergens)
            {
                list.Add(
                    allergen
                        .Replace(")", "")
                        .Replace("contains ", ""));
            }

            return list.ToArray();
        }
    }
}
