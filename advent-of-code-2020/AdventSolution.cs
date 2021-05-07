using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2020
{
    abstract class AdventSolution
    {
        protected AdventSolution()
        {
            displayDay();
            SolveExample1();
            SolveProblem1();
            SolveExample2();
            SolveProblem2();
        }

        private void displayDay()
        {
            Console.WriteLine($"Solving for {getNamespaceFolder()}...");
        }

        private string getNamespaceFolder()
        {
            var fullNamespace  = GetType().Namespace;
            var namespaceFolder = fullNamespace.Substring(fullNamespace.LastIndexOf(".") + 1);
            return namespaceFolder;
        }

        public void SolveExample1()
        {
            solve(
                "first example",
                performWorkForProblem1,
                getExampleDataset);
        }

        public void SolveExample2()
        {
            solve(
                "second example",
                performWorkForProblem2,
                getExampleDataset);
        }

        public void SolveProblem1()
        {
            solve(
                "first",
                performWorkForProblem1,
                getLiveDataset);
        }

        public void SolveProblem2()
        {
            solve(
                "second",
                performWorkForProblem2,
                getLiveDataset);
        }

        protected abstract void performWorkForProblem1(IList<string> inputData);

        protected abstract void performWorkForProblem2(IList<string> inputData);

        private void solve(
            string problem,
            Action<IList<string>> workMethod,
            Func<IList<string>> inputDataLoadMethod)
        {
            Console.WriteLine(getSolvingAnnouncement(problem));
            workMethod.Invoke(inputDataLoadMethod.Invoke());
        }

        private string getSolvingAnnouncement(string problem)
        {
            return $"Solving the {problem} problem...";
        }

        private IList<string> getExampleDataset()
        {
            return getDataset("example.txt");
        }

        private IList<string> getLiveDataset()
        {
            return getDataset("input.txt");
        }

        private IList<string> getDataset(
            string filename)
        {
            var folder = getNamespaceFolder();
            var filepath = Path.Combine(folder, filename);
            return File.ReadAllLines(filepath);
        }
    }
}
