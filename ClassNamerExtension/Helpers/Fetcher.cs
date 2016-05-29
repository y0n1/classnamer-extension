using App.Interfaces;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace App.Helpers
{
    public class Fetcher : IFetchable
    {
        private const string DEFAULT_URL = "http://www.classnamer.com/index.txt?generator=generic";

        public Task Fetch(ConcurrentStack<string> stack)
        {

            return Task.Run(() =>
            {
                WebRequest request = WebRequest.Create(DEFAULT_URL);
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string className = reader.ReadToEnd().Trim();
                        stack.PushRange(new string[] { className });
                    }
                }
            });
        }
    }
}
