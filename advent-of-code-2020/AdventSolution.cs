using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2020
{
    abstract class AdventSolution
    {
        protected virtual string getExample1DatasetFilename()
        {
            return getExampleDatasetFilename();
        }

        protected virtual string getExample2DatasetFilename()
        {
            return getExampleDatasetFilename();
        }

        private string getExampleDatasetFilename()
        {
            return "example.txt";
        }

        private string getLiveDatasetFilename()
        {
            return "input.txt";
        }

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
                performWorkForExample1,
                getExample1DatasetFilename());
        }

        public void SolveExample2()
        {
            solve(
                "second example",
                performWorkForExample2,
                getExample2DatasetFilename());
        }

        public void SolveProblem1()
        {
            solve(
                "first",
                performWorkForProblem1,
                getLiveDatasetFilename());
        }

        public void SolveProblem2()
        {
            solve(
                "second",
                performWorkForProblem2,
                getLiveDatasetFilename());
        }

        protected virtual void performWorkForExample1(IList<string> inputData)
        {
            performWorkForProblem1(inputData);
        }

        protected abstract void performWorkForProblem1(IList<string> inputData);

        protected virtual void performWorkForExample2(IList<string> inputData)
        {
            performWorkForProblem2(inputData);
        }

        protected abstract void performWorkForProblem2(IList<string> inputData);

        private void solve(
            string problem,
            Action<IList<string>> workMethod,
            string inputDataFilename)
        {
            Console.WriteLine(getSolvingAnnouncement(problem));
            workMethod.Invoke(getDataset(inputDataFilename));
        }

        private string getSolvingAnnouncement(string problem)
        {
            return $"Solving the {problem} problem...";
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
