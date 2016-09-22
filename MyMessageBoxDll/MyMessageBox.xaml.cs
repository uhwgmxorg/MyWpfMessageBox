using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace MyMessageBoxDll
{
    /// <summary>
    /// Interaktionslogik für MessageBoxA.xaml
    /// </summary>
    public partial class MyMessageBox : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public enum MessageBoxAColor
        {
            RED,
            BLUE,
            YELLOW,
            GREEN,
            GRAY
        }

        public const uint NOTHING       = 0x00000000;
        public const uint MODAL         = 0x00000001;
        public const uint CENTER_WINDOW = 0x00000002;
        public const uint ACTIVATE      = 0x00000004;
        public const uint TOPMOST       = 0x00000008;
        public const uint CLOSABLE      = 0x00000010;
        public const uint MOVABLE       = 0x00000020;
        public const uint HIDEABLE      = 0x00000040;

        private double _desktopWorkingAreaRight = 0.0;
        private double _desktopWorkingAreaBottom = 0.0;
        private bool _closable = false;
        private bool _movable = false;
        private bool _hideable = false;

        /// <summary>
        /// Properties
        /// </summary>
        private double transparents;
        public double Transparents { get { return transparents; } set { transparents = value; OnPropertyChanged("Transparents"); } }
        private string message { get; set; }
        public string Message { get { return message; } set { message = value; OnPropertyChanged("Message"); } }

        private Color color1;
        public Color Color1 { get { return color1; } set { color1 = value; OnPropertyChanged("Color1"); } }
        private Color color2;
        public Color Color2 { get { return color2; } set { color2 = value; OnPropertyChanged("Color2"); } }

        private Brush backgrount1;
        public Brush Backgrount1 { get { return backgrount1; } set { backgrount1 = value; OnPropertyChanged("Backgrount1"); } }
        private Brush backgrount2;
        public Brush Backgrount2 { get { return backgrount2; } set { backgrount2 = value; OnPropertyChanged("Backgrount2"); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public MyMessageBox()
        {
            InitializeComponent();
            DataContext = this;
            Transparents = 1;
            Hide();
            SetMessageBoxAColor(MessageBoxAColor.RED);
        }

        /******************************/
        /*      Public Functions      */
        /******************************/
        #region  Public Functions

        /// <summary>
        /// ShowMessageBoaA
        /// </summary>
        public void ShowMyMessageBox(string message,uint characteristics, MessageBoxAColor messageBoxAColor = MessageBoxAColor.RED)
        {
            SetMessageBoxAColor(messageBoxAColor);
            Message = message;
            Transparents = 1;
            if ((CENTER_WINDOW & characteristics) != NOTHING) CenterWindow(this, _desktopWorkingAreaRight, _desktopWorkingAreaBottom);
            if ((ACTIVATE & characteristics) != NOTHING) Activate();
            if ((TOPMOST & characteristics) != NOTHING) Topmost = true; else Topmost = false;
            if ((CLOSABLE & characteristics) != NOTHING) _closable = true; else _closable = false;
            if ((MOVABLE & characteristics) != NOTHING) _movable = true; else _movable = false;
            if ((HIDEABLE & characteristics) != NOTHING) _hideable = true; else _hideable = false;

            if ((MODAL & characteristics) != NOTHING)
            {
                if(IsVisible)
                    Hide();
                ShowDialog();
            }
            else
                Show();
        }

        /// <summary>
        /// HideMessageBoaA
        /// </summary>
        public void HideMyMessageBox()
        {
            Hide();
        }

        #endregion
        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// button_CloseClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_CloseClick(object sender, RoutedEventArgs e)
        {
            if (!_closable) { System.Console.Beep(); return; }
            Hide();
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

        /// <summary>
        /// Window_Main_Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Main_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;

            _desktopWorkingAreaRight = desktopWorkingArea.Right;
            _desktopWorkingAreaBottom = desktopWorkingArea.Bottom;

            CenterWindow(this,_desktopWorkingAreaRight,_desktopWorkingAreaBottom);

            LoadStrings();
        }

        /// <summary>
        /// Window_Main_LostFocus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Main_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Window_MouseLeftButtonDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>Window_MouseRightButtonDown
        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!_movable) return;
            DragMove();
        }

        /// <summary>
        /// Window_MouseRightButtonDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        /// <summary>
        /// Window_MouseWheel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (!_hideable) return;
            double d = (double)e.Delta / 1000;
            double o = Transparents;

            o -= d;
            if (0.2 <= o && o <= 1)
                Transparents = o;
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// SetMessageBoxAColor
        /// </summary>
        /// <param name="messageBoxColor"></param>
        private void SetMessageBoxAColor(MessageBoxAColor messageBoxColor)
        {
            switch (messageBoxColor)
            {
                case MessageBoxAColor.RED:
                    Color1 = StringToColor("#FFFF3F3F");
                    Color2 = StringToColor("#FFFF8A8A");
                    break;
                case MessageBoxAColor.BLUE:
                    Color1 = StringToColor("#FF3F50FF");
                    Color2 = StringToColor("#FF8A8AFF");
                    break;
                case MessageBoxAColor.YELLOW:
                    Color1 = StringToColor("#FFEEFF3F");
                    Color2 = StringToColor("#FFFAFF8A");
                    break;
                case MessageBoxAColor.GREEN:
                    Color1 = StringToColor("#FF007E1D");
                    Color2 = StringToColor("#FFAAFF8A");
                    break;
                case MessageBoxAColor.GRAY:
                    Color1 = StringToColor("#FF9C9A9A");
                    Color2 = StringToColor("#FFCFCFCF");
                    break;
            }
            Backgrount1 = new SolidColorBrush(Color1);
            Backgrount2 = new SolidColorBrush(Color2);
        }

        /// <summary>
        /// StringToColor
        /// </summary>
        /// <param name="colorString"></param>
        /// <returns></returns>
        private Color StringToColor(string colorString)
        {
            Color c = new Color();
            if (colorString.StartsWith("#"))
            {
                colorString = colorString.Replace("#", "");
                byte a = System.Convert.ToByte("ff", 16);
                byte pos = 0;
                if (colorString.Length == 8)
                {
                    a = System.Convert.ToByte(colorString.Substring(pos, 2), 16);
                    pos = 2;
                }
                byte r = System.Convert.ToByte(colorString.Substring(pos, 2), 16);
                pos += 2;
                byte g = System.Convert.ToByte(colorString.Substring(pos, 2), 16);
                pos += 2;
                byte b = System.Convert.ToByte(colorString.Substring(pos, 2), 16);
                c = Color.FromArgb(a, r, g, b);
                return c;
            }

            return c;
        }

        /// <summary>
        /// CenterWindow
        /// </summary>
        /// <param name="win"></param>
        /// <param name="desktopWorkingAreaRight"></param>
        /// <param name="desktopWorkingAreaBottom"></param>
        private void CenterWindow(Window win, double desktopWorkingAreaRight, double desktopWorkingAreaBottom)
        {
            if (win == null || desktopWorkingAreaRight < 0 || desktopWorkingAreaBottom < 0)
                return;
            win.Left = (desktopWorkingAreaRight / 2 + win.Width / 2) - win.Width;
            win.Top = (desktopWorkingAreaBottom / 2 + win.Height / 2) - win.Height;
        }

        /// <summary>
        /// LoadStrings
        /// </summary>
        private void LoadStrings()
        {
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
