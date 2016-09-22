using MyMessageBoxDll;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace MyWpfMessageBoxTestExe
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        MyMessageBox _messageBox = new MyMessageBox();
        uint _characteristics = 0;

        private MyMessageBox.MessageBoxAColor mBoxColor = MyMessageBox.MessageBoxAColor.RED;
        public MyMessageBox.MessageBoxAColor MBoxColor
        {
            get
            {
                return mBoxColor;
            }
            set
            {
                mBoxColor = value;
                OnPropertyChanged("RED");
                OnPropertyChanged("BLUE");
                OnPropertyChanged("YELLOW");
                OnPropertyChanged("GREEN");
                OnPropertyChanged("GRAY");
                UpdateMyMessageBox();
            }
        }
        public bool IsRed
        {
            get { return MBoxColor == MyMessageBox.MessageBoxAColor.RED; }
            set { MBoxColor = value ? MyMessageBox.MessageBoxAColor.RED : MBoxColor; }
        }
        public bool IsBlue
        {
            get { return MBoxColor == MyMessageBox.MessageBoxAColor.BLUE; }
            set { MBoxColor = value ? MyMessageBox.MessageBoxAColor.BLUE : MBoxColor; }
        }
        public bool IsYellow
        {
            get { return MBoxColor == MyMessageBox.MessageBoxAColor.YELLOW; }
            set { MBoxColor = value ? MyMessageBox.MessageBoxAColor.YELLOW : MBoxColor; }
        }
        public bool IsGreen
        {
            get { return MBoxColor == MyMessageBox.MessageBoxAColor.GREEN; }
            set { MBoxColor = value ? MyMessageBox.MessageBoxAColor.GREEN : MBoxColor; }
        }
        public bool IsGray
        {
            get { return MBoxColor == MyMessageBox.MessageBoxAColor.GRAY; }
            set { MBoxColor = value ? MyMessageBox.MessageBoxAColor.GRAY : MBoxColor; }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events
        
        /// <summary>
        /// button_Show_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Show_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(String.Format("{0} {1}", Dec2Bin(_characteristics, 16), MBoxColor));
            _messageBox.ShowMyMessageBox("My very importen message !!", _characteristics, MBoxColor);
        }

        /// <summary>
        /// button_Hide_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Hide_Click(object sender, RoutedEventArgs e)
        {
            _messageBox.HideMyMessageBox();
        }

        /// <summary>
        /// button_Close_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
        /******************************/
        /*      Menu Events           */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        private void checkBox_Click1(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)(sender)).IsChecked) _characteristics |= MyMessageBox.NOTHING; else _characteristics &= ~MyMessageBox.NOTHING;
            Debug.WriteLine(String.Format("{0} {1}", Dec2Bin(_characteristics, 16), sender.ToString()));
            UpdateMyMessageBox();
        }
        private void checkBox_Click2(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)(sender)).IsChecked) _characteristics |= MyMessageBox.MODAL; else _characteristics &= ~MyMessageBox.MODAL;
            Debug.WriteLine(String.Format("{0} {1}", Dec2Bin(_characteristics, 16), sender.ToString()));
            UpdateMyMessageBox();
        }
        private void checkBox_Click3(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)(sender)).IsChecked) _characteristics |= MyMessageBox.CENTER_WINDOW; else _characteristics &= ~MyMessageBox.CENTER_WINDOW;
            Debug.WriteLine(String.Format("{0} {1}", Dec2Bin(_characteristics, 16), sender.ToString()));
            UpdateMyMessageBox();
        }
        private void checkBox_Click4(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)(sender)).IsChecked) _characteristics |= MyMessageBox.ACTIVATE; else _characteristics &= ~MyMessageBox.ACTIVATE;
            Debug.WriteLine(String.Format("{0} {1}", Dec2Bin(_characteristics, 16), sender.ToString()));
            UpdateMyMessageBox();
        }
        private void checkBox_Click5(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)(sender)).IsChecked) _characteristics |= MyMessageBox.TOPMOST; else _characteristics &= ~MyMessageBox.TOPMOST;
            Debug.WriteLine(String.Format("{0} {1}", Dec2Bin(_characteristics, 16), sender.ToString()));
            UpdateMyMessageBox();
        }
        private void checkBox_Click6(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)(sender)).IsChecked) _characteristics |= MyMessageBox.CLOSABLE; else _characteristics &= ~MyMessageBox.CLOSABLE;
            Debug.WriteLine(String.Format("{0} {1}", Dec2Bin(_characteristics, 16), sender.ToString()));
            UpdateMyMessageBox();
        }
        private void checkBox_Click7(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)(sender)).IsChecked) _characteristics |= MyMessageBox.MOVABLE; else _characteristics &= ~MyMessageBox.MOVABLE;
            Debug.WriteLine(String.Format("{0} {1}", Dec2Bin(_characteristics, 16), sender.ToString()));
            UpdateMyMessageBox();
        }
        private void checkBox_Click8(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)(sender)).IsChecked) _characteristics |= MyMessageBox.HIDEABLE; else _characteristics &= ~MyMessageBox.HIDEABLE;
            Debug.WriteLine(String.Format("{0} {1}", Dec2Bin(_characteristics, 16), sender.ToString()));
            UpdateMyMessageBox();
        }

        /// <summary>
        /// Window_Closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, System.EventArgs e)
        {
            _messageBox.Close();
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// UpdateMyMessageBox
        /// </summary>
        private void UpdateMyMessageBox()
        {
            Debug.WriteLine(String.Format("{0} {1}", Dec2Bin(_characteristics, 16), MBoxColor));
            if (_messageBox.IsVisible)
                _messageBox.ShowMyMessageBox("My very importen message !!", _characteristics, MBoxColor);
        }

        /// <summary>
        /// Dec2Bin
        /// </summary>
        /// <param name="dezimalzahl"></param>
        /// <param name="bitanzahl"></param>
        /// <returns></returns>
        public string Dec2Bin(long dezimalzahl, int bitanzahl)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < bitanzahl; dezimalzahl = dezimalzahl >> 1, i++)
                sb.Insert(0, dezimalzahl & 1);
            return sb.ToString();
        }

        /// <summary>
        /// OnPropertyChanged
        /// </summary>
        /// <param name="p"></param>
        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }
}
