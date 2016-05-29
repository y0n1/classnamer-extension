using App.Features;
using System;

namespace App
{
    class Program
    {
        private const string FEATURE1_MESSAGE = "Hello dear user - the selected class name is ";
        private const string FEATURE2_MESSAGE = "Hello dear user - we've selected three new class names for you, and those are ";
        private const int FEATURE1_JOB_SIZE = 1;
        private const int FEATURE2_JOB_SIZE = 3;
        private const int FEATURE3_JOB_SIZE = 200;
        private static string MENU = $@"
""ClassNamer Extension Features""
=================================

1.  Show me {FEATURE1_JOB_SIZE} ClassName
2.  Show me {FEATURE2_JOB_SIZE} ClassNames
3.  Show the tag cloud from {FEATURE3_JOB_SIZE} ClassNames

(Press ESC at any time to exit)

>>> Which one do you like to see?  ";

        static void Main(string[] args)
        {
            Feature feature = null;
            ConsoleKeyInfo optionChosen;
            bool continueFlag = true;

            do
            {
                Console.Clear();
                Console.Write(MENU);
                optionChosen = Console.ReadKey();
                switch (optionChosen.Key)
                {
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.D1:
                        feature = new Feature(FEATURE1_MESSAGE);
                        feature.Process(FEATURE1_JOB_SIZE);
                        feature.ShowClassNames();
                        break;
                    case ConsoleKey.D2:
                        feature = new Feature(FEATURE2_MESSAGE);
                        feature.Process(FEATURE2_JOB_SIZE);
                        feature.ShowClassNames();
                        break;
                    case ConsoleKey.D3:
                        feature = new Feature();
                        feature.Process(FEATURE3_JOB_SIZE);
                        feature.ShowTagCloud();
                        break;
                }

                Console.Write("{0}Press ENTER to continue...", Environment.NewLine);
                Console.ReadLine();
            } while (continueFlag);
        }
    }
}
