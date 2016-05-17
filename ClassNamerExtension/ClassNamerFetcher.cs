using System.IO;
using System.Net;

namespace ClassNamerExtension
{
    public class ClassNamerFetcher
    {
        private static readonly ClassNamerFetcher instance = new ClassNamerFetcher();
        private const string URL = "http://www.classnamer.com/index.txt?generator=generic";

        private ClassNamerFetcher() { }

        public static ClassNamerFetcher GetInstance
        {
            get
            {
                return instance;
            }
        }

        public static string Fetch()
        {
            WebRequest request = WebRequest.Create(URL);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();

            return responseFromServer.Trim();
        }
    }
}
