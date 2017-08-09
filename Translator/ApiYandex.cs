using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Translator
	{
	class ApiYandex
		{
		public string Translate(string s,string lang)
			{
			string ApiKey="trnsl.1.1.20170809T111737Z.11cf930211c4be06.71600bfdff1e34a5d9f647ddcd774bcd7de4f1fe";

			if(s.Length>0)
				{
				WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
					+"key=" + ApiKey + "&text=" + s + "&lang=" + lang);

				WebResponse response=request.GetResponse();

				using (StreamReader stream = new StreamReader(response.GetResponseStream()))
					{
					string line;

					if((line=stream.ReadLine())!=null)
						{
						Translation tranlation = JsonConvert.DeserializeObject<Translation>(line);
						s="";

						foreach(string str in tranlation.text)
							{
							s+=str;
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
			public string code {get;set;}
			public string lang {get;set;}
			public string[] text{get; set;}
			}
		}
	}
