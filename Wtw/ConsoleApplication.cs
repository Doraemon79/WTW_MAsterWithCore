using System;
using Wtw.Models;
using Wtw.Services;

namespace Wtw
{
    public class ConsoleApplication
    {
        private readonly TriangleService _triangleService;
        private readonly CommandLine _commandLine;

        public ConsoleApplication(
            TriangleService triangleService,
            CommandLine commandLine
        )
        {
            _triangleService = triangleService;
            _commandLine = commandLine;
        }

        public void Run()
        {
            try
            {
                var inputs = _triangleService.LoadFromFile(_commandLine.InputFileName);
                var report = _triangleService.Accumulate(inputs);
                _triangleService.WriteToFile(_commandLine.OutputFileName, report);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
