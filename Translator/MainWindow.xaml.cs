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
			count_wordslabel.Content = Properties.Settings.Default.count_words;
			ChangeColorTextInLabel();
			Loaded += MainWindow_Loaded;
			//
			}

		string lang = "en-ru";
		int count = 0;
		string buffer_str = "";
		string buffer_str_trns = "";
		bool turn = false;

		private void Button_Click(object sender, RoutedEventArgs e)
			{
			if (lang == "en-ru")
				{
				lang = "ru-en";
				langlabel.Content = "Русский -> Английский";
				}
			else
				{
				lang = "en-ru";
				langlabel.Content = "Английский -> Русский";
				}
			}

		private void inText_TextChanged(object sender, TextChangedEventArgs e)
			{
		   outText.Text = obj.Translate(inText.Text, lang);
			Properties.Settings.Default.count_words += count;
			count_wordslabel.Content = Properties.Settings.Default.count_words;
			count = inText.Text.Length;
			countlabel.Content = count;
			Properties.Settings.Default.Save();
			ChangeColorTextInLabel();
			}

		private void ChangeColorTextInLabel()
			{
			if (Properties.Settings.Default.count_words>=8000)
				{
				count_wordslabel.Foreground = System.Windows.Media.Brushes.Red;
				}
			}

		private void ShowStandardBalloon()
			{
			TaskbarIcon MyNotifyIcon = new TaskbarIcon();
			TranslateStringFromBuffer();

			if (buffer_str != "" && buffer_str != null)
				{
				MyNotifyIcon.ShowBalloonTip(buffer_str, buffer_str_trns, BalloonIcon.Info);
				}
			}

		private void GetStringFromBuffer()
			{
				{
				buffer_str = Clipboard.GetText();
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
				// Буфер обмена обновился, в переменной text лежит скопированный текст
				var text = Clipboard.GetText();

				buffer_str = text.ToString(); // Запихаем в текстбокс для отображения
				ShowStandardBalloon();
				}

			return IntPtr.Zero;
			}
		}
	}
