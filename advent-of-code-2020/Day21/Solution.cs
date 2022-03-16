using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day21
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var recipes = new List<Recipe>();
            foreach (var line in inputData)
            {
                recipes.Add(new Recipe(line));
            }

            var allergens = recipes.SelectMany(x => x.Allergens).Distinct();

            var ingredientsThatCannotBeAllergens = getAllIngredients(recipes);

            foreach (var allergen in allergens)
            {
                var ingredients = getAllIngredients(recipes);

                var ingredientSets = recipes
                    .Where(x => x.Allergens.Contains(allergen))
                    .Select(x => x.Ingredients);

                foreach (var ingredientSet in ingredientSets)
                {
                    ingredients = ingredients.Intersect(ingredientSet);
                }

                ingredientsThatCannotBeAllergens = ingredientsThatCannotBeAllergens.Except(ingredients);
            }

            var listedIngredients = ingredientsThatCannotBeAllergens.ToList();
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> getAllIngredients(IEnumerable<Recipe> recipes)
        {
            return recipes
                .SelectMany(x => x.Ingredients)
                .Distinct();
        }
    }
}
