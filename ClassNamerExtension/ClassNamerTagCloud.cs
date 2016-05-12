using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassNamerExtension
{
    class ClassNamerTagCloud
    {
        private static readonly ClassNamerTagCloud instance = new ClassNamerTagCloud();
        private SortedDictionary<string, List<string>> _mappings;

        private ClassNamerTagCloud()
        {
            _mappings = new SortedDictionary<string, List<string>>();
        }

        public static ClassNamerTagCloud GetInstance
        {
            get
            {
                return instance;
            }
        }

        public void Add(string className)
        {
            List<string> words = parse(className);
            foreach (var word in words)
            {
                if (_mappings.ContainsKey(word))
                {
                    _mappings[word].Add(className);
                }
                else
                {
                    List<string> newList = new List<string>();
                    newList.Add(className);
                    _mappings.Add(word, newList);
                }
            }
        }

        private List<string> parse(string className)
        {
            List<string> wordsList = new List<string>();
            Regex.Replace(className, @"[A-Z][a-z0-9]+", delegate (Match match) {
                wordsList.Add(match.ToString());
                return null;
            });

            return wordsList;
        }

        public void Clear()
        {
            _mappings.Clear();
        }

        public void Show()
        {
            foreach (var entry in _mappings)
            {
                string word = entry.Key;
                int count = entry.Value.Count;
                Console.WriteLine("{0}: {1}", word, count);
            }
        }

    }
}
