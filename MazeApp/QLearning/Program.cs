using System;
using System.Collections.Generic;

namespace Q_Learning
{
    public class Program
    {
        /// <summary>
        /// Start the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Begin Q-learning maze demo");
            Console.WriteLine("Setting up mazes and rewards");

            List<MazeModel> mazes = new List<MazeModel>();
            //mazes.Add(MazeExamples.Example_1());
            //mazes.Add(MazeExamples.Example_2());

            var intelligence = new Intelligence();
            intelligence.RunExamplesInConsole(mazes);

            Console.WriteLine("End demo");
            Console.ReadLine();
        }
    }
        
}