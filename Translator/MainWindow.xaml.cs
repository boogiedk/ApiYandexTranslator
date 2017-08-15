using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Translator
	{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
		{
		ApiYandex obj;
		public MainWindow()
			{
			InitializeComponent();
			obj = new ApiYandex();
			}

		string lang = "en-ru";
		string inTextForTranslate;
		int count = 0;

		private void Button_Click(object sender, RoutedEventArgs e)
			{
			if (lang=="en-ru")
				{
				langlabel.Content = "Русский -> Английский";
				lang = "ru-en";
				}
			else
				{
				lang = "en-ru";
				langlabel.Content = "Английский -> Русский";
				}
			}

		private void inText_TextChanged(object sender, TextChangedEventArgs e)
			{
			inTextForTranslate = inText.Text;
		    outText.Text = obj.Translate(inText.Text, lang);
			count = inTextForTranslate.Length;
			countlabel.Content = count;
			}
		}
	}
