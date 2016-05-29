using App.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace App.Helpers
{
    class TagCloud : ConcurrentDictionary<string, List<string>>
    {
        private static volatile TagCloud instance;
        private static object syncRoot = new object();

        private TagCloud() : base() { }

        public static TagCloud GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new TagCloud();
                        }
                    }
                }

                return instance;
            }
        }

        public void Add(string className)
        {
            lock (syncRoot)
            {
                string[] words = className.SplitCamelCase();
                foreach (var word in words)
                {
                    if (ContainsKey(word))
                    {
                        base[word].Add(className);
                    }
                    else
                    {
                        List<string> newList = new List<string>();
                        newList.Add(className);
                        TryAdd(word, newList);
                    }
                }
            }
        }

        public TagCloud CreateFromArray(string[] classNames)
        {
            foreach (var item in classNames)
            {
                Add(item);
            }

            return this;
        }

        public override string ToString()
        {
            lock (syncRoot)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var word in Keys)
                {
                    int count = base[word].Count;
                    sb.AppendFormat("{0}: {1}{2}", word, count, Environment.NewLine);
                }

                return sb.ToString();
            }
        }

    }
}
