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
using System.Windows.Shapes;
using System.Text.RegularExpressions;


namespace Translator
	{
	/// <summary>
	/// Логика взаимодействия для SettingPage.xaml
	/// </summary>
	public partial class SettingPage : Window
		{
		public SettingPage()
			{
			InitializeComponent();
			Loaded += SettingPage_Loaded;
			}

		String combination_s = " ";
		String[] str;
		bool flag = false;
		String[] example_combinations = { "TAB", "CTRL", "ALT", "Tab", "Ctrl", "Alt", "tab","ctrl","alt"};
		private void GetCombinationForTranslate()
			{
				try
					{
					combination_s = combinLabel.Text.ToString();
					str = combination_s.Split(new char[] { '+', ';' }, StringSplitOptions.RemoveEmptyEntries);
					combin_now.Text = str[0] + "+" + str[1] + ";";
					}
				catch (IndexOutOfRangeException)
					{}
			}
		
		private void TestStringForTrue()
			{
			GetCombinationForTranslate();
			if(str!=null)
				{
				bool flag_1 = false;
				for (int i = 0; i < example_combinations.Length;i++ )
					{
					if (str.Contains(example_combinations[i]))
						{
						flag_1 = true;
						}
					}
				if(flag_1)
					{
					if (Regex.IsMatch(str[1], "^[A-Za-z+' ']+$") == true)
						{
						flag = true;
						}
					else
						{
						flag = false;
						}
					}
				}
			}

		private void MessageForUser()
			{
			TestStringForTrue();
			if(!flag)
				{
				enter_combinateLabel.Foreground = System.Windows.Media.Brushes.Red;
				errorNotify.Content = "Неправильный формат комбинации!";
				buttonSave.IsEnabled = false;
				}
			}

		private void buttonSave_Click(object sender, RoutedEventArgs e)
			{
			MessageForUser();
			if (flag)
				{
				Properties.Settings.Default.combinate_trns = combinLabel.Text;
				Properties.Settings.Default.Save();
				this.Close();
				}
			}

		private void SettingPage_Loaded(object sender, RoutedEventArgs e)
			{
			combin_now.Text = Properties.Settings.Default.combinate_trns;
			combin_now.IsEnabled = false;
			}

		private void combinLabel_TextChanged(object sender, TextChangedEventArgs e)
			{
			if (buttonSave.IsEnabled == false)
				{
				buttonSave.IsEnabled = true;
				enter_combinateLabel.Foreground = System.Windows.Media.Brushes.Black;
				}
			}
		}
	}
