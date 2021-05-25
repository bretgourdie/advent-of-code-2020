using System.Collections.Generic;

namespace advent_of_code_2020.Day19
{
    class RuleSet
    {
        private readonly IDictionary<int, Rule> rulesById;

        public RuleSet(
            IList<string> rulesSection,
            IList<string> changes)
        {
            rulesById = initializeRuleDictionary(rulesSection);
            applyChanges(rulesById, changes);
        }

        private void applyChanges(
            IDictionary<int, Rule> rules,
            IList<string> changes)
        {
            foreach (var change in changes)
            {
                var rule = new Rule(change);
                rules[rule.Id] = rule;
            }
        }

        private IDictionary<int, Rule> initializeRuleDictionary(
            IList<string> ruleSection)
        {
            var dict = new Dictionary<int, Rule>();

            foreach (var ruleLine in ruleSection)
            {
                var rule = new Rule(ruleLine);
                dict[rule.Id] = rule;
            }

            return dict;
        }

        public string GetTest(int ruleId = 0)
        {
            return rulesById[ruleId].GetTest(rulesById);
        }
    }
}
