using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
	{
	static class CheckClass
		{

		static string inTextCheck = "";
		static string outTextCheck = "";
		static string selectedlang = "";

		public static  string GetInText
			{
			get { return inTextCheck; }
			set { inTextCheck = value; }
			}
		public static string GetSelectedLang
			{
			get {return selectedlang;}
			set {selectedlang=value;}
			}
		public static void GetRequestForTranslate()
			{
			ApiYandex request = new ApiYandex();
			outTextCheck=request.Translate(inTextCheck, selectedlang);
			Swaplang();
			}
		public static bool CheckResult()
			{
			return inTextCheck == outTextCheck ? false : true;
			}
		public static void Swaplang()
			{
			MainWindow SendResult = new MainWindow();
			if(CheckResult()==true)
				{
				//
				}
			else
				{
				Console.WriteLine("Else");
				//swap radiobutton and send request again
				}
			}
		}
	}
