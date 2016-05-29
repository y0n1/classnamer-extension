using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassNamerExtension.Helpers;

namespace ClassNamerExtensionTest
{
    [TestClass]
    public class ClassNameFetcherTests
    {
        [TestMethod]
        public async void FetchAsync_Test()
        {
            ClassNameFetcher fetcher = new ClassNameFetcher();
            string expected = await fetcher.FetchAsync();
            //string[] actual = camelCaseString.SplitCamelCase();

            Console.WriteLine(expected);

            Assert.IsNotNull(expected);
        }
    }
}
