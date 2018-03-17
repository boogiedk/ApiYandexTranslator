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
    public partial class MainWindow : Window
    {
        private ApiYandex connectApiYandex;

        private string lang;


        public MainWindow()
        {
            InitializeComponent();
            connectApiYandex = new ApiYandex();
            Loaded += MainWindow_Loaded;
            lang = "en-ru";
        }

        private void inText_TextChanged(object sender, TextChangedEventArgs e)
        {
            int count = 0;
            outText.Text = connectApiYandex.Translate(inText.Text, lang);
            count = inText.Text.Length;
            countlabel.Content = count;
            IdentifyEnterLang();
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
                // method for ballon show
            }

            return IntPtr.Zero;
        }

        private void IdentifyEnterLang()
        {
            string inTextEnter = inText.Text;
            if (Regex.IsMatch(inTextEnter, "^[A-Za-z' ']+$") == true)
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
