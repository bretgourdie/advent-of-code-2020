using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;

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

            var rules = getAllergenRules(recipesWithOnlyAllergens);

            var allergens = recipesWithOnlyAllergens.SelectMany(x => x.Allergens).Distinct().ToList();

            var ingredients = recipesWithOnlyAllergens.SelectMany(x => x.Ingredients).Distinct().ToList();

            var allergenAssignments = getAllergenAssignments(
                rules,
                allergens,
                ingredients);

            return allergenAssignments;
        }

        private IDictionary<string, string> getAllergenAssignments(
            IDictionary<IEnumerable<string>, IEnumerable<string>> rules,
            IList<string> allergens,
            IList<string> allergenicIngredients)
        {
            foreach (var allergen in allergens)
            {
                foreach (var ingredient in allergenicIngredients)
                {
                    if (assignmentWorks(allergen, ingredient, rules))
                    {
                        var allergensMinusCurrent = allergens.Where(x => x != allergen).ToList();
                        var ingredientsMinusCurrent = allergenicIngredients.Where(x => x != ingredient).ToList();

                        if (allergensMinusCurrent.Count == 0 && ingredientsMinusCurrent.Count == 0)
                        {
                            return new Dictionary<string, string>() { {allergen, ingredient} };
                        }

                        else
                        {
                            var otherAssignments = getAllergenAssignments(rules, allergensMinusCurrent, ingredientsMinusCurrent);

                            if (otherAssignments != null)
                            {
                                otherAssignments[allergen] = ingredient;
                                return otherAssignments;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private bool assignmentWorks(
            string allergen,
            string ingredient,
            IDictionary<IEnumerable<string>, IEnumerable<string>> rules)
        {
            foreach (var rule in rules)
            {
                if (rule.Key.Contains(allergen))
                {
                    if (!rule.Value.Contains(ingredient))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static IDictionary<IEnumerable<string>, IEnumerable<string>> getAllergenRules(
            IList<Recipe> recipesWithOnlyAllergens)
        {
            var allergensToIngredientsRules = new Dictionary<HashSet<string>, HashSet<string>>();

            foreach (var recipe in recipesWithOnlyAllergens)
            {
                var allergens = new HashSet<string>(recipe.Allergens);
                var ingredients = new HashSet<string>(recipe.Ingredients);

                if (allergensToIngredientsRules.ContainsKey(allergens))
                {
                    allergensToIngredientsRules[allergens] =
                        new HashSet<string>(
                            allergensToIngredientsRules[allergens].Intersect(ingredients)
                        );
                }

                else
                {
                    allergensToIngredientsRules[allergens] = ingredients;
                }
            }

            var rulesToIEnumerables = new Dictionary<IEnumerable<string>, IEnumerable<string>>();
            foreach (var rule in allergensToIngredientsRules)
            {
                rulesToIEnumerables.Add(rule.Key, rule.Value);
            }

            return rulesToIEnumerables;
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
            return
                String.Join(",",
                    ingredientToAllergen
                        .OrderBy(x => x.Key)
                        .Select(x => x.Value)
                );
        }

        private IEnumerable<string> getAllIngredients(IEnumerable<Recipe> recipes)
        {
            return recipes
                .SelectMany(x => x.Ingredients)
                .Distinct();
        }
    }
}
