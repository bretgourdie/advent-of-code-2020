using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2020
{
    abstract class AdventSolution<Input>
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
                getExampleDataset<Input>);
        }

        public void SolveExample2()
        {
            solve(
                "second example",
                performWorkForProblem2,
                getExampleDataset<Input>);
        }

        public void SolveProblem1()
        {
            solve(
                "first",
                performWorkForProblem1,
                getLiveDataset<Input>);
        }

        public void SolveProblem2()
        {
            solve(
                "second",
                performWorkForProblem2,
                getLiveDataset<Input>);
        }

        protected abstract void performWorkForProblem1(IList<Input> inputData);

        protected abstract void performWorkForProblem2(IList<Input> inputData);

        private void solve(
            string problem,
            Action<IList<Input>> workMethod,
            Func<IList<Input>> inputDataLoadMethod)
        {
            Console.WriteLine(getSolvingAnnouncement(problem));
            workMethod.Invoke(inputDataLoadMethod.Invoke());
        }

        private string getSolvingAnnouncement(string problem)
        {
            return $"Solving the {problem} problem...";
        }

        private IList<T> getExampleDataset<T>()
        {
            return getDataset<T>("example.txt");
        }

        private IList<T> getLiveDataset<T>()
        {
            return getDataset<T>("input.txt");
        }

        private IList<T> getDataset<T>(
            string filename)
        {
            return
                getRawDataset(filename)
                    .Select(x => (T)Convert.ChangeType(x, typeof(T)))
                    .ToList();
        }

        private IList<string> getRawDataset(
            string filename)
        {
            var folder = getNamespaceFolder();
            var filepath = Path.Combine(folder, filename);
            return File.ReadAllLines(filepath);
        }
    }
}
