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
            IEnumerable<Recipe> recipes,
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

        private IEnumerable<Recipe> getRecipes(IList<string> inputData)
        {
            return inputData.Select(
                line => new Recipe(line)
            );
        }

        private IEnumerable<string> getNonAllergenicIngredients(IEnumerable<Recipe> recipes)
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
            IEnumerable<Recipe> recipes,
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
            IEnumerable<string> allergens,
            IEnumerable<string> allergenicIngredients)
        {
            foreach (var allergen in allergens)
            {
                foreach (var ingredient in allergenicIngredients)
                {
                    if (assignmentWorks(allergen, ingredient, rules))
                    {
                        var allergensMinusCurrent = allergens.Where(x => x != allergen);
                        var ingredientsMinusCurrent = allergenicIngredients.Where(x => x != ingredient);

                        if (!allergensMinusCurrent.Any() && !ingredientsMinusCurrent.Any())
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
            return rules.All(
                x =>
                    !x.Key.Contains(allergen)
                    || (x.Key.Contains(allergen) && x.Value.Contains(ingredient))
            );
        }

        private static IDictionary<IEnumerable<string>, IEnumerable<string>> getAllergenRules(
            IEnumerable<Recipe> recipesWithOnlyAllergens)
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

        private IEnumerable<Recipe> getRecipesWithOnlyAllergens(
            IEnumerable<Recipe> recipes,
            IEnumerable<string> nonAllergenicIngredients)
        {
            return recipes
                .Select(recipe => new Recipe(
                    recipe.Ingredients.Except(nonAllergenicIngredients),
                    recipe.Allergens)
                );
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
