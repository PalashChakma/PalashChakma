using System;
using PalashChakma.Controller;

namespace PalashChakma
{
    class Program
    {
        static void Main(string[] args)
        {
            Hello();

            var cnt = new ToyRobotController(new ToyRobot());

            while (true)
            {
                string command = CommandPrompt();
                //exit with BYE command
                if (command.ToUpper() == "BYE")
                {
                    Environment.Exit(0);
                }
                Console.WriteLine(cnt.ExecCommand(command));
            }
        }
        private static string CommandPrompt()
        {
            Console.WriteLine("$$: ");
            return Console.ReadLine();
        }
        private static void Hello()
        {
            Console.WriteLine("Welcome to Toy Robot!!");
            Console.WriteLine("-----------------------------");
        }
    }
}
