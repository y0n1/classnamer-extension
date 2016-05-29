using App.Interfaces;
using App.Helpers;
using System.Collections.Concurrent;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace App.Features
{
    class Feature : IProcessable
    {
        private readonly string _greeting;
        private string[] _classNames;

        public Feature(string greeting = null)
        {
            _greeting = greeting;
        }

        public void Process(int jobSize)
        {
            int taskGroupSize = Environment.ProcessorCount;
            List<Task> tasks = new List<Task>(taskGroupSize);
            ConcurrentStack<string> concurrentStack = new ConcurrentStack<string>();
            int taskGroupsCount = jobSize / taskGroupSize;
            int reminder = jobSize % taskGroupSize;
            int remainingJobSize = taskGroupsCount + reminder;

            while (remainingJobSize > 0)
            {
                if (taskGroupsCount == 0 && reminder > 0)
                {
                    for (int i = 0; i < reminder; i++)
                    {
                        tasks.Add(new Fetcher().Fetch(concurrentStack));
                        reminder--;
                    }

                }
                else
                {
                    for (int i = 0; i < taskGroupSize; i++)
                    {
                        tasks.Add(new Fetcher().Fetch(concurrentStack));
                    }

                    taskGroupsCount--;
                }
                
                Task.WaitAll(tasks.ToArray());
                remainingJobSize = taskGroupsCount + reminder;
            }
            
            _classNames = concurrentStack.ToArray();
        }

        public void ShowTagCloud()
        {
            Console.Clear();
            var tagCloud = TagCloud.GetInstance.CreateFromArray(_classNames);
            Console.WriteLine(tagCloud.ToString());
            tagCloud.Clear();
        }

        public void ShowClassNames()
        {
            Console.Clear();
            Console.WriteLine("{0} {1}", _greeting, string.Join(", ", _classNames));
        }
    }
}
