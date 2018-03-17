using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Translator
{
    class ApiYandex
    {
        public string Translate(string s, string lang)
        {
            const string
                apiKey =
                    "trnsl.1.1.20170809T111737Z.11cf930211c4be06.71600bfdff1e34a5d9f647ddcd774bcd7de4f1fe"; // you need insert your api=key

            if (s.Length > 0)
            {
                var request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
                                                + "key=" + apiKey + "&text=" + s + "&lang=" + lang);

                var response = request.GetResponse();

                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    string line;

                    if ((line = stream.ReadLine()) != null)
                    {
                        var tranlation = JsonConvert.DeserializeObject<Translation>(line);
                        s = "";

                        foreach (string str in tranlation.Text)
                        {
                            s += str;
                        }
                    }
                }

                return s;
            }
            else
                return "";
        }

        class Translation
        {
            public string Code { get; set; }
            public string Lang { get; set; }
            public string[] Text { get; set; }
        }
    }
}
