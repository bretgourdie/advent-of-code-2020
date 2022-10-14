using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day21
{
    class Solution : AdventSolution
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            var recipes = getRecipes(inputData);
            var nonAllergenicIngredients = getNonAllergenicIngredients(recipes);
            var nonAllergenicIngredientsUsedCount = countNonAllergenicIngredientsUsedInRecipes(
                recipes,
                nonAllergenicIngredients);

            Console.WriteLine(
                $"Non-allergenic ingredients appear {nonAllergenicIngredientsUsedCount} time(s)");
        }

        private int countNonAllergenicIngredientsUsedInRecipes(
            IList<Recipe> recipes,
            IEnumerable<string> nonAllergenicIngredients)
        {
            int count = 0;

            foreach (var recipe in recipes)
            {
                foreach (var ingredient in nonAllergenicIngredients)
                {
                    if (recipe.Ingredients.Contains(ingredient))
                    {
                        count += 1;
                    }
                }
            }

            return count;
        }

        private IList<Recipe> getRecipes(IList<string> inputData)
        {
            var recipes = new List<Recipe>();
            foreach (var line in inputData)
            {
                recipes.Add(new Recipe(line));
            }

            return recipes;
        }

        private IEnumerable<string> getNonAllergenicIngredients(IList<Recipe> recipes)
        {
            var allergens = recipes.SelectMany(x => x.Allergens).Distinct();

            var ingredientsThatCannotBeAllergens = getAllIngredients(recipes);

            foreach (var allergen in allergens)
            {
                var ingredientsThatAreNotAllergens = getAllIngredients(recipes);

                var ingredientsInAnAllergenRecipe = recipes
                    .Where(x => x.Allergens.Contains(allergen))
                    .Select(x => x.Ingredients);

                foreach (var ingredientsThatCouldBeAllergens in ingredientsInAnAllergenRecipe)
                {
                    ingredientsThatAreNotAllergens = ingredientsThatAreNotAllergens.Intersect(ingredientsThatCouldBeAllergens);
                }

                ingredientsThatCannotBeAllergens = ingredientsThatCannotBeAllergens.Except(ingredientsThatAreNotAllergens);
            }

            return ingredientsThatCannotBeAllergens;
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            var recipes = getRecipes(inputData);
            var nonAllergenicIngredients = getNonAllergenicIngredients(recipes);
            var ingredientToAllergen = getIngredientsByAllergen(recipes, nonAllergenicIngredients);

            var canonicalDangerousIngredientList = getCanonicalDangerousIngredientList(ingredientToAllergen);

            Console.WriteLine(
                $"Canonical dangerous ingredient list is \"{canonicalDangerousIngredientList}\"");
        }

        private IDictionary<string, string> getIngredientsByAllergen(
            IList<Recipe> recipes,
            IEnumerable<string> nonAllergenicIngredients)
        {
            var recipesWithOnlyAllergens = getRecipesWithOnlyAllergens(recipes, nonAllergenicIngredients);

            var allergens = recipes.SelectMany(x => x.Allergens).Distinct();

            var ingredientToAllergen = new Dictionary<string, string>();

            while (ingredientToAllergen.Count < allergens.Count())
            {
                foreach (var recipe in recipesWithOnlyAllergens)
                {
                    if (recipe.Allergens.Count == 1 && recipe.Ingredients.Count == 1)
                    {
                        ingredientToAllergen[recipe.Ingredients.Single()] = recipe.Allergens.Single();

                        // remove ingredients
                    }
                }
            }

            throw new NotImplementedException();
        }

        private IList<Recipe> getRecipesWithOnlyAllergens(
            IList<Recipe> recipes,
            IEnumerable<string> nonAllergenicIngredients)
        {
            var recipesWithOnlyAllergenics = new List<Recipe>();

            foreach (var recipe in recipes)
            {
                var onlyAllergenicIngredients = recipe.Ingredients.Except(nonAllergenicIngredients);

                var onlyAllergenicRecipe = new Recipe(
                    onlyAllergenicIngredients,
                    recipe.Allergens);

                recipesWithOnlyAllergenics.Add(onlyAllergenicRecipe);
            }

            return recipesWithOnlyAllergenics;
        }

        private string getCanonicalDangerousIngredientList(IDictionary<string, string> ingredientToAllergen)
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
