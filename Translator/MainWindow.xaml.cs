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
using System.Drawing;
using Hardcodet.Wpf.TaskbarNotification;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Text.RegularExpressions;

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
			Loaded += MainWindow_Loaded;
			}

		string lang = "en-ru";
		int count = 0;
		string buffer_str = "";
		string buffer_str_trns = "";
		bool turn = false;

		private void inText_TextChanged(object sender, TextChangedEventArgs e)
			{
			outText.Text = obj.Translate(inText.Text, lang);
			count = inText.Text.Length;
			countlabel.Content = count;
			IdentifyEnterLang();
			}

		private void ShowStandardBalloon()
			{
			TaskbarIcon MyNotifyIcon = new TaskbarIcon();
			TranslateStringFromBuffer();

			if (buffer_str != "" && buffer_str != null && turn)
				{
				MyNotifyIcon.ShowBalloonTip(buffer_str, buffer_str_trns, BalloonIcon.Info);
				}
			}

		private void GetStringFromBuffer()
			{
				{
				if (turn)
					buffer_str = Clipboard.GetText();
				else
					buffer_str = "";
				}
			}

		private void TranslateStringFromBuffer()
			{
			GetStringFromBuffer();
			if (buffer_str != "" && buffer_str != null)
				{
				buffer_str_trns = obj.Translate(buffer_str, lang);
				}
			}

		private void TurnBuffTranslate(object sender, RoutedEventArgs e)
			{
			if (turn)
				{
				ClickBuff.Header = "Включить буффер-перевод";
				turn = false;
				}
			else
				{
				ClickBuff.Header = "Выключить буффер-перевод";
				turn = true;
				}
			}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
			{
			var hwnd = new WindowInteropHelper(this).Handle;
			var source = HwndSource.FromHwnd(hwnd);

			source.AddHook(WndProc);
			Win32.AddClipboardFormatListener(hwnd);
			}

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
			{
			if (msg == Win32.WM_CLIPBOARDUPDATE)
				{
				ShowStandardBalloon();
				}

			return IntPtr.Zero;
			}

		private void MenuItemSetting_Click(object sender, RoutedEventArgs e)
			{
			SettingPage obj_open = new SettingPage();
			obj_open.Show();
			}

		private void IdentifyEnterLang()
			{
			string inTextEnter = inText.Text;

			if (Regex.IsMatch(inTextEnter, "^[A-Za-z]+$") == true)
				{
				lang = "en-ru";
				langlabel.Content = "Английский > Русский";
				}
			else
				{
				lang = "ru-en";
				langlabel.Content = "Русский > Английский";
				}
			}
		}
	}
