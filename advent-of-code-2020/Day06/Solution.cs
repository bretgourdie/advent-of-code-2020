using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day06
{
    class Solution : AdventSolution<string>
    {
        protected override void performWorkForProblem1(IList<string> inputData)
        {
            countAnsweredQuestions(
                inputData,
                new AnyAnswered());
        }

        private void countAnsweredQuestions(
            IList<string> inputData,
            IAnswered answerStrategy)
        {
            int totalQuestionsAnswered = 0;
            var answeredQuestions = answerStrategy.Initialize();

            foreach (var line in inputData)
            {
                if (line == String.Empty)
                {
                    totalQuestionsAnswered = addAnsweredQuestionsToTotal(
                        answeredQuestions,
                        totalQuestionsAnswered);
                    answeredQuestions = answerStrategy.Initialize();
                }

                else
                {
                    answerStrategy.Record(answeredQuestions, line);
                }
            }

            totalQuestionsAnswered = addAnsweredQuestionsToTotal(
                answeredQuestions,
                totalQuestionsAnswered);

            Console.WriteLine($"Total questions answered was {totalQuestionsAnswered}");
        }

        private int addAnsweredQuestionsToTotal<T>(
            ISet<T> answeredQuestions,
            int previousTotal)
        {
            return previousTotal + answeredQuestions.Count;
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            countAnsweredQuestions(
                inputData,
                new AllAnswered());
        }
    }
}
