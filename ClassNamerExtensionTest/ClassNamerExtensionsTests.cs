using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassNamerExtension.Utils;

namespace ClassNamerExtensionTest
{
    [TestClass]
    public class ClassNamerExtensionsTests
    {
        [TestMethod]
        public void SplitCamelCase_TestSplitting()
        {
            string camelCaseString = "BlockingTaskState";
            string[] expected = { "Blocking", "Task", "State" };
            string[] actual = camelCaseString.SplitCamelCase();

            Console.WriteLine(string.Join(",", actual));

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
