using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ClassNamerExtension
{
    class Program
    {
        private const int SPINNER_VELOCITY = 35;
        private const string FEATURE1_MESSAGE = "Hello dear user - the selected class name is ";
        private const string FEATURE2_MESSAGE = "Hello dear user - we've selected three new class names for you, and those are ";
        private const int FEATURE2_REFERENCES_NUMBER = 200;
        private static string MENU = $@"
""ClassNamer Extension Features""
=================================

1.  Get me a ClassName
2.  Get me 3 ClassNames
3.  Display a tag cloud from {FEATURE2_REFERENCES_NUMBER} ClassNames

(Press ESC at any time to exit)

>>> Which one do you like to see?  ";

        static void Main(string[] args)
        {
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
                        showClassNames(FEATURE1_MESSAGE, 1);
                        break;
                    case ConsoleKey.D2:
                        showClassNames(FEATURE2_MESSAGE, 3);
                        break;
                    case ConsoleKey.D3:
                        showTagCloud(FEATURE2_REFERENCES_NUMBER);
                        break;
                }

                Console.Write("{0}Press ENTER to continue...", Environment.NewLine);
                Console.ReadLine();
            } while (continueFlag);
        }

        private static void showTagCloud(int numberOfReferences)
        {
            Console.Clear();
            ClassNamerTagCloud tagCloud = ClassNamerTagCloud.GetInstance;

            Console.CursorVisible = false;            
            for (int i = 0; i < numberOfReferences; i++)
            {
                tagCloud.Add(ClassNamerFetcher.Fetch());
                showSpinner(SPINNER_VELOCITY);
            }
            Console.CursorVisible = true;

            tagCloud.Show();
            tagCloud.Clear();
        }

        private static async void showSpinner(int spinVelocity)
        {
            await Task.Run(() =>
            {
                String[] characters = { "/", "-", "\\", "|" };
                foreach (var character in characters)
                {
                    Console.Write(character);
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Thread.Sleep(spinVelocity);
                }
            });
        }

        private static void showClassNames(string message, int quantity)
        {
            Console.Clear();
            showSpinner(SPINNER_VELOCITY);
            string aggregateMessage = message;
            if (quantity == 1)
            {
                Console.WriteLine(aggregateMessage + ClassNamerFetcher.Fetch());
            }
            else
            {
                for (int i = 0; i < quantity; i++)
                {
                    string className = ClassNamerFetcher.Fetch();
                    if (i < quantity - 2)
                    {
                        aggregateMessage += className + ", ";
                    }
                    else if (i < quantity - 1)
                    {
                        aggregateMessage += className + " and ";
                    }
                    else
                    {
                        aggregateMessage += className;
                    }
                }

                Console.WriteLine(aggregateMessage + Environment.NewLine);
            }
        }
    }
}
