using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day21
{
    class Recipe
    {
        public readonly string[] Ingredients;
        public readonly string[] Allergens;
        public Recipe(string line)
        {
            var theSplit = line.Split(new [] {" ("}, StringSplitOptions.RemoveEmptyEntries);

            Ingredients = theSplit[0].Split(' ');
            if (theSplit.Length > 1)
            {
                Allergens = handleAllergens(theSplit[1]);
            }

            else
            {
                Allergens = new string[0];
            }
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
