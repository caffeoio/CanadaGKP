using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
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
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace CanadaGKP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly SolidColorBrush ControlInactiveBrush = CreateFrozenBrush(33, 64, 103);
        private static readonly SolidColorBrush ControlActiveBrush = CreateFrozenBrush(46, 127, 198);
        private static readonly SolidColorBrush StatusOnBrush = CreateFrozenBrush(46, 170, 89);

        public MainWindow()
        {
            InitializeComponent();
            ConfigureToolTips();
        }

        private static SolidColorBrush CreateFrozenBrush(byte red, byte green, byte blue)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(red, green, blue));
            brush.Freeze();
            return brush;
        }

        private void ConfigureToolTips()
        {
            foreach (FieldInfo field in GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType != typeof(TextBlock))
                {
                    continue;
                }

                TextBlock textBlock = field.GetValue(this) as TextBlock;
                if (textBlock == null || string.IsNullOrWhiteSpace(textBlock.Name))
                {
                    continue;
                }

                if (!textBlock.Name.EndsWith("_btn", StringComparison.OrdinalIgnoreCase)
                    && !textBlock.Name.StartsWith("L_", StringComparison.OrdinalIgnoreCase)
                    && !textBlock.Name.StartsWith("R_", StringComparison.OrdinalIgnoreCase)
                    && !textBlock.Name.EndsWith("_T", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string actionText = NormalizeActionText(textBlock.Text);
                textBlock.ToolTip = $"Action: {actionText}. Click to send this command.";
                textBlock.Cursor = Cursors.Hand;
            }

            SetElementToolTip(exit, "Double-click to close the application.");
            SetElementToolTip(exit1, "Return to the machine status panel.");
            SetElementToolTip(show, "Open the JAKA robot control panel.");
            SetElementToolTip(control1, "Open machine command controls.");
            SetElementToolTip(control2, "Open machine status monitoring.");
            SetElementToolTip(Reload_L, "Refresh left robot status.");
            SetElementToolTip(Reload_R, "Refresh right robot status.");
            SetElementToolTip(FY, "Switch between machine pages.");
        }

        private static string NormalizeActionText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "Command";
            }

            string[] parts = text
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(part => part.Trim())
                .Where(part => !string.IsNullOrWhiteSpace(part))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            return parts.Length == 0 ? "Command" : parts[0];
        }

        private static void SetElementToolTip(FrameworkElement element, string tooltip)
        {
            if (element == null)
            {
                return;
            }

            element.ToolTip = tooltip;
            element.Cursor = Cursors.Hand;
        }

        public static string IPorPortUrl = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\IPorPortMessageClient.txt";
        /// <summary>
        /// Create a socket client.
        /// </summary>
        Socket client;
        public bool IsMake = true;
        public int cz = 0;
        public void CoffeeClient()
        {
            try
            {
                /// Create the client.
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                /// IP address
                IPAddress ip = IPAddress.Parse(IPorPortMessageClient.Instance.CoffeeIP);
                /// Port
                IPEndPoint endPoint = new IPEndPoint(ip, int.Parse(IPorPortMessageClient.Instance.CoffeePort));
                /// Establish a remote connection with the server
                client.Connect(endPoint);
                /// Start receive thread
                Thread thread = new Thread(ReciveMsg);
                //thread.IsBackground = true;
                thread.Start(client);
            }
            catch (Exception)
            {
                Thread.Sleep(100);
                CoffeeClient();
            }

        }
        public void RobotSel_L(RobotMsg robotMsg)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(delegate
                {
                    robot_kj_L.Source = robotMsg.Robot_YKJ_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_sd_L.Source = robotMsg.Robot_SD_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_sn_L.Source = robotMsg.Robot_SN_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_yx_L.Source = robotMsg.Robot_YX_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_zt_L.Source = robotMsg.Robot_ZT_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_bj_L.Source = robotMsg.Robot_BJ_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_tz_L.Source = robotMsg.Robot_TZ_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png")); ;
                    RobotYN_L(robotMsg);
                }));
            }
            catch (Exception)
            {
                return;
            }
        }
        public void RobotSel_R(RobotMsg robotMsg)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(delegate
                {
                    robot_kj_R.Source = robotMsg.Robot_YKJ_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_sd_R.Source = robotMsg.Robot_SD_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_sn_R.Source = robotMsg.Robot_SN_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_yx_R.Source = robotMsg.Robot_YX_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_zt_R.Source = robotMsg.Robot_ZT_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_bj_R.Source = robotMsg.Robot_BJ_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    robot_tz_R.Source = robotMsg.Robot_TZ_L ? new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png")) : new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png")); ;
                    RobotYN_R(robotMsg);
                }));
            }
            catch (Exception)
            {
                return;
            }
        }
        public void RobotYN_L(RobotMsg robotMsg)
        {
            try
            {
                LTypeKJ_L.Background = ControlInactiveBrush;
                LTypeBJ_L.Background = ControlInactiveBrush;
                LOpenBTDY_L.Background = ControlInactiveBrush;
                LCloseBTDY_L.Background = ControlInactiveBrush;
                LOpenSSN_L.Background = ControlInactiveBrush;
                LCloseSSN_L.Background = ControlInactiveBrush;
                LOpenYXCX_L.Background = ControlInactiveBrush;
                LCloseTZCX_L.Background = ControlInactiveBrush;
                LContinueZTCX_L.Background = ControlInactiveBrush;
                LCloseJXCX_L.Background = ControlInactiveBrush;
                L_SD.Tag = "0";
                L_BJ.Tag = "0";
                L_BTSD.Tag = "0";
                L_BTXD.Tag = "0";
                L_SSN.Tag = "0";
                L_XSN.Tag = "0";
                L_YXCX.Tag = "0";
                L_ZTCX.Tag = "0";
                L_TZCX.Tag = "0";
                L_JXCX.Tag = "0";
                if (!robotMsg.Robot_YKJ_L && !robotMsg.Robot_BJ_L && !robotMsg.Robot_YX_L && !robotMsg.Robot_SN_L && !robotMsg.Robot_ZT_L && !robotMsg.Robot_SD_L && !robotMsg.Robot_TZ_L)
                {
                    LTypeKJ_L.Background = ControlActiveBrush;
                    L_SD.Tag = "1";
                }
                else if (robotMsg.Robot_YKJ_L)
                {
                    LOpenBTDY_L.Background = ControlActiveBrush;
                    L_BTSD.Tag = "1";
                }
                else if (robotMsg.Robot_SD_L)
                {
                    LCloseBTDY_L.Background = ControlActiveBrush;
                    LOpenSSN_L.Background = ControlActiveBrush;
                    L_BTXD.Tag = "1";
                    L_SSN.Tag = "1";
                }
                else if (robotMsg.Robot_SN_L)
                {
                    LOpenYXCX_L.Background = ControlActiveBrush;
                    LCloseSSN_L.Background = ControlActiveBrush;
                    L_XSN.Tag = "1";
                    L_YXCX.Tag = "1";
                }
                else if (robotMsg.Robot_YX_L)
                {
                    LCloseTZCX_L.Background = ControlActiveBrush;
                    LContinueZTCX_L.Background = ControlActiveBrush;
                    L_ZTCX.Tag = "1";
                    L_TZCX.Tag = "1";
                }
                else if (robotMsg.Robot_TZ_L)
                {
                    LOpenYXCX_L.Background = ControlActiveBrush;
                    LCloseSSN_L.Background = ControlActiveBrush;
                    L_XSN.Tag = "1";
                    L_YXCX.Tag = "1";
                }
                else if (robotMsg.Robot_ZT_L)
                {
                    LCloseTZCX_L.Background = ControlActiveBrush;
                    LCloseJXCX_L.Background = ControlActiveBrush;
                    L_JXCX.Tag = "1";
                    L_TZCX.Tag = "1";
                }
                else if (robotMsg.Robot_BJ_L)
                {
                    LTypeBJ_L.Background = ControlActiveBrush;
                    L_BJ.Tag = "1";
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        public void RobotYN_R(RobotMsg robotMsg)
        {
            try
            {
                LTypeKJ_R.Background = ControlInactiveBrush;
                LTypeBJ_R.Background = ControlInactiveBrush;
                LOpenBTDY_R.Background = ControlInactiveBrush;
                LCloseBTDY_R.Background = ControlInactiveBrush;
                LOpenSSN_R.Background = ControlInactiveBrush;
                LCloseSSN_R.Background = ControlInactiveBrush;
                LOpenYXCX_R.Background = ControlInactiveBrush;
                LCloseTZCX_R.Background = ControlInactiveBrush;
                LContinueZTCX_R.Background = ControlInactiveBrush;
                LCloseJXCX_R.Background = ControlInactiveBrush;
                R_SD.Tag = "0";
                R_BJ.Tag = "0";
                R_BTSD.Tag = "0";
                R_BTXD.Tag = "0";
                R_SSN.Tag = "0";
                R_XSN.Tag = "0";
                R_YXCX.Tag = "0";
                R_ZTCX.Tag = "0";
                R_TZCX.Tag = "0";
                R_JXCX.Tag = "0";
                if (!robotMsg.Robot_YKJ_R && !robotMsg.Robot_BJ_R && !robotMsg.Robot_YX_R && !robotMsg.Robot_SN_R && !robotMsg.Robot_ZT_R && !robotMsg.Robot_SD_R && !robotMsg.Robot_TZ_R)
                {
                    LTypeKJ_R.Background = ControlActiveBrush;
                    R_SD.Tag = "1";
                }
                else if (robotMsg.Robot_YKJ_R)
                {
                    LOpenBTDY_R.Background = ControlActiveBrush;
                    R_BTSD.Tag = "1";
                }
                else if (robotMsg.Robot_SD_R)
                {
                    LCloseBTDY_R.Background = ControlActiveBrush;
                    LOpenSSN_R.Background = ControlActiveBrush;
                    R_BTXD.Tag = "1";
                    R_SSN.Tag = "1";
                }
                else if (robotMsg.Robot_SN_R)
                {
                    LOpenYXCX_R.Background = ControlActiveBrush;
                    LCloseSSN_R.Background = ControlActiveBrush;
                    R_XSN.Tag = "1";
                    R_YXCX.Tag = "1";
                }
                else if (robotMsg.Robot_YX_R)
                {
                    LCloseTZCX_R.Background = ControlActiveBrush;
                    LContinueZTCX_R.Background = ControlActiveBrush;
                    R_ZTCX.Tag = "1";
                    R_TZCX.Tag = "1";
                }
                else if (robotMsg.Robot_TZ_R)
                {
                    LOpenYXCX_R.Background = ControlActiveBrush;
                    LCloseSSN_R.Background = ControlActiveBrush;
                    R_XSN.Tag = "1";
                    R_YXCX.Tag = "1";
                }
                else if (robotMsg.Robot_ZT_R)
                {
                    LCloseTZCX_R.Background = ControlActiveBrush;
                    LCloseJXCX_R.Background = ControlActiveBrush;
                    R_JXCX.Tag = "1";
                    R_TZCX.Tag = "1";
                }
                else if (robotMsg.Robot_BJ_R)
                {
                    LTypeBJ_R.Background = ControlActiveBrush;
                    R_BJ.Tag = "1";
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Handle messages sent by the server to the client
        /// </summary>
        /// <param name="o">Client socket</param>
        void ReciveMsg(object o)
        {
            Socket client = o as Socket;
            while (true)
            {
                try
                {
                    /// Buffer for incoming data
                    byte[] arrList = new byte[1024 * 1024];
                    /// Received message length (bytes)
                    int length = client.Receive(arrList);
                    string msg = Encoding.UTF8.GetString(arrList, 0, length);
                    var ClientList = JsonConvert.DeserializeObject<ClientList>(msg);
                    if (ClientList != null)
                    {
                        if (ClientList.code == 0)
                        {
                            DOSelect(ClientList.message.Name, ClientList.message.type);
                        }
                        else if (ClientList.code == 1)
                        {
                            DISelect(ClientList.MsgBol);
                        }
                        else if (ClientList.code == 2)
                        {
                            //if (ClientList.message.type == 12)
                            //{
                            if (ClientList.message.Name == "Reload_L")
                            {
                                RobotSel_L(ClientList.robotMsg);
                            }
                            else if (ClientList.message.Name == "Reload_R")
                            {
                                RobotSel_R(ClientList.robotMsg);
                            }
                            //}
                            IsMake = ClientList.IsMake;
                        }
                    }
                }
                catch (Exception)
                {
                    /// Close the client
                    client.Close();
                }

            }
        }
        /// <summary>
        /// Update button indicator state
        /// </summary>
        /// <param name="btn">Target button text block</param>
        /// <param name="type">0 = off, 1 = on</param>
        /// <param name="img">Indicator image</param>
        public void BtnShow(TextBlock btn, double type, Image img)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(delegate
                {
                    if (type == 0)// OFF state
                    {
                        btn.Tag = 0;
                        img.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    }
                    else if (type == 1)// ON state
                    {
                        btn.Tag = 1;
                        img.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"绿圆.png"));
                    }
                }));
            }
            catch (Exception)
            {
                return;
            }
        }
        public void DOSelect(string name, double tag)
        {
            try
            {
                switch (name)
                {
                    case "DO1":
                        BtnShow(DO1_btn, tag, DO1_img);
                        break;
                    case "DO2":
                        BtnShow(DO2_btn, tag, DO2_img);
                        break;
                    case "DO3":
                        BtnShow(DO3_btn, tag, DO3_img);
                        break;
                    case "DO4":
                        BtnShow(DO4_btn, tag, DO4_img);
                        break;
                    case "DO5":
                        BtnShow(DO5_btn, tag, DO5_img);
                        break;
                    case "DO6":
                        BtnShow(DO6_btn, tag, DO6_img);
                        break;
                    case "DO7":
                        BtnShow(DO7_btn, tag, DO7_img);
                        break;
                    case "DO8":
                        BtnShow(DO8_btn, tag, DO8_img);
                        break;
                    case "DO9":
                        BtnShow(DO9_btn, tag, DO9_img);
                        break;
                    case "DO10":
                        BtnShow(DO10_btn, tag, DO10_img);
                        break;
                    case "DO11":
                        BtnShow(DO11_btn, tag, DO11_img);
                        break;
                    case "DO12":
                        BtnShow(DO12_btn, tag, DO12_img);
                        break;
                    case "DO13":
                        BtnShow(DO13_btn, tag, DO13_img);
                        break;
                    case "DO15":
                        BtnShow(DO15_btn, tag, DO15_img);
                        break;
                    case "DO16":
                        BtnShow(DO16_btn, tag, DO16_img);
                        break;
                    case "DO19":
                        BtnShow(DO19_btn, tag, DO19_img);
                        break;
                    case "DO20":
                        BtnShow(DO20_btn, tag, DO20_img);
                        break;
                    case "DO21":
                        BtnShow(DO21_btn, tag, DO21_img);
                        break;
                    case "DO22":
                        BtnShow(DO22_btn, tag, DO22_img);
                        break;
                    case "DO23":
                        BtnShow(DO23_btn, tag, DO23_img);
                        break;
                    case "DO24":
                        BtnShow(DO24_btn, tag, DO24_img);
                        break;
                    case "DO25":
                        BtnShow(DO25_btn, tag, DO25_img);
                        break;
                    case "DO26":
                        BtnShow(DO26_btn, tag, DO26_img);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        /// <summary>
        /// IO point query
        /// </summary>
        public void DISelect(DigitalMsgBol msgBol)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(delegate
                {
                    AQGS.Background = !msgBol.AQGS_Bol ? ControlInactiveBrush : StatusOnBrush;
                    CCSDW.Background = !msgBol.CCSDW_Bol ? ControlInactiveBrush : StatusOnBrush;
                    CCXDW.Background = !msgBol.CCXDW_Bol ? ControlInactiveBrush : StatusOnBrush;
                    DKLBQFK1.Background = !msgBol.DKLBQ1_Bol ? ControlInactiveBrush : StatusOnBrush;
                    DKLBQFK2.Background = !msgBol.DKLBQ2_Bol ? ControlInactiveBrush : StatusOnBrush;
                    DKLBQFK3.Background = !msgBol.DKLBQ3_Bol ? ControlInactiveBrush : StatusOnBrush;
                    CCJC.Background = !msgBol.CCJC_Bol ? ControlInactiveBrush : StatusOnBrush;
                    DCJC1.Background = !msgBol.DCJC1_Bol ? ControlInactiveBrush : StatusOnBrush;
                    DCJC2.Background = !msgBol.DCJC2_Bol ? ControlInactiveBrush : StatusOnBrush;
                    DCJC3.Background = !msgBol.DCJC3_Bol ? ControlInactiveBrush : StatusOnBrush;
                    GJC1.Background = !msgBol.QGJCDK1_Bol ? ControlInactiveBrush : StatusOnBrush;
                    GJC2.Background = !msgBol.QGJCDK2_Bol ? ControlInactiveBrush : StatusOnBrush;
                    GJC3.Background = !msgBol.QGJCDK3_Bol ? ControlInactiveBrush : StatusOnBrush;
                    BJC1.Background = !msgBol.QBJCSK1_Bol ? ControlInactiveBrush : StatusOnBrush;
                    BJC2.Background = !msgBol.QBJCSK2_Bol ? ControlInactiveBrush : StatusOnBrush;
                    JBCGJC.Background = !msgBol.JBCGJC_Bol ? ControlInactiveBrush : StatusOnBrush;
                    QKLJC.Background = !msgBol.QKLJLJC_Bol ? ControlInactiveBrush : StatusOnBrush;
                    NNZYYBJ.Background = !msgBol.BXJC1_Bol ? ControlInactiveBrush : StatusOnBrush;
                    NNZYQLJC.Background = !msgBol.BXJC2_Bol ? ControlInactiveBrush : StatusOnBrush;
                    NNJC.Background = !msgBol.QKLJLJC_Bol ? ControlInactiveBrush : StatusOnBrush;
                    YMNJC.Background = !msgBol.BXJC3_Bol ? ControlInactiveBrush : StatusOnBrush;
                    YMNYBJ.Background = !msgBol.BXJC4_Bol ? ControlInactiveBrush : StatusOnBrush;
                    SJC.Background = !msgBol.WATER_Bol ? ControlInactiveBrush : StatusOnBrush;
                    GTJC1.Background = !msgBol.GTJC1_Bol ? ControlInactiveBrush : StatusOnBrush;
                    GTJC2.Background = !msgBol.GTJC2_Bol ? ControlInactiveBrush : StatusOnBrush;
                    CTJC1.Background = !msgBol.CTJC1_Bol ? ControlInactiveBrush : StatusOnBrush;
                    CTJC2.Background = !msgBol.CTJC2_Bol ? ControlInactiveBrush : StatusOnBrush;
                    ChaiC_Txt.Text = msgBol.ChaiTIint.ToString();
                    MOC_Txt.Text = msgBol.MoCIint.ToString();
                    BaiST_Txt.Text = msgBol.BaistIint.ToString();
                    QiaoKL_Txt.Text = msgBol.QKLIint1.ToString();
                    QiaoKL2_Txt.Text = msgBol.QKLIint2.ToString();
                    QiaoKL3_Txt.Text = msgBol.QKLIint3.ToString();
                }));
            }
            catch (Exception)
            {
                return;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(IPorPortUrl))
                {
                    using (FileStream stream = File.Create(IPorPortUrl))
                    {
                        stream.Close();
                        stream.Dispose();
                        IPorPortMessageClient porPortInfo1 = IPorPortMessageClient.Instance;
                        File.WriteAllText(IPorPortUrl, JsonConvert.SerializeObject(porPortInfo1));
                    }
                }
                else
                {
                    var porPortInfo = JsonConvert.DeserializeObject<IPorPortMessageClient>(File.ReadAllText(IPorPortUrl));
                    IPorPortMessageClient.Instance.CoffeeIP = porPortInfo.CoffeeIP;
                    IPorPortMessageClient.Instance.CoffeePort = porPortInfo.CoffeePort;
                }


                //  Client();

            }
            catch (Exception)
            {
                return;
            }
        }
        public void Client()
        {
            try
            {
                /// Create the client.
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                /// IP address
                IPAddress ip = IPAddress.Parse(IPorPortMessageClient.Instance.CoffeeIP);
                /// Port
                IPEndPoint endPoint = new IPEndPoint(ip, 5555);
                /// Establish a remote connection with the server
                client.Connect(endPoint);
                ClientList clientList = new ClientList();
                clientList.MsgBol = DigitalMsgBol.Instance;
                clientList.code = 99;
                client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientList)));
                /// Start receive thread
                Thread thread = new Thread(ReciveMsg);
                thread.IsBackground = true;
                thread.Start(client);
            }
            catch (Exception)
            {
                Client();
            }
        }
        private void Reload_L_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Robot_CZ("Reload_L", 12);
            }
            catch (Exception)
            {
                return;
            }
        }
        public void Send(string name, string tag)
        {
            try
            {
                ClientList clientList = new ClientList();
                MessageClientList coffeelist = MessageClientList.Instance;
                coffeelist.Name = name;
                coffeelist.type = double.TryParse(tag, out double do1) ? do1 : 1;
                clientList.message = coffeelist;
                clientList.code = 0;
                client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientList)));
            }
            catch (Exception)
            {
                return;
            }
        }
        private void DO1_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO1", DO1_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }
        private void DO4_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DO4_btn.Tag.ToString() == "0")
                    Send("DO4", DO4_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }
        private void DO5_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DO5_btn.Tag.ToString() == "0")
                    Send("DO5", DO5_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }
        private void DO2_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO2", DO2_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO8_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO8", DO8_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO9_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO9", DO9_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO10_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO10", DO10_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO3_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO3", DO3_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO11_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO11", DO11_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO15_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO15", DO15_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO16_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO16", DO16_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO6_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO6", DO6_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO7_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO7", DO7_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO12_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO12", DO12_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO13_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO13", DO13_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO19_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO19", DO19_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO20_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO20", DO20_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO21_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO21", DO21_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO22_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO22", DO22_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO23_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO23", DO23_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO24_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO24", DO24_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO25_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO25", DO25_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DO26_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("DO26", DO26_btn.Tag.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void control1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                jiqiQuYu.Visibility = Visibility.Collapsed;
                jiqiquyuSel.Visibility = Visibility.Collapsed;
                JAKAQuYu.Visibility = Visibility.Collapsed;
                MiMa.Visibility = Visibility.Visible;
                exit1.Visibility = Visibility.Collapsed;
                cz = 1;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void control2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                jiqiQuYu.Visibility = Visibility.Collapsed;
                jiqiquyuSel.Visibility = Visibility.Visible;
                JAKAQuYu.Visibility = Visibility.Collapsed;
                exit1.Visibility = Visibility.Collapsed;
                MiMa.Visibility = Visibility.Collapsed;
                cz = 0;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void show_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                jiqiQuYu.Visibility = Visibility.Collapsed;
                jiqiquyuSel.Visibility = Visibility.Collapsed;
                JAKAQuYu.Visibility = Visibility.Collapsed;
                MiMa.Visibility = Visibility.Visible;
                exit1.Visibility = Visibility.Collapsed;
                cz = 2;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
                {
                    MessageBoxResult result = MessageBox.Show("Exit orders will not be made,Do you really want to quit?", "Exit", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
                        {
                            if (thisProc.ProcessName == "CanadaCoffee")
                            {
                                if (!thisProc.CloseMainWindow())
                                    thisProc.Kill(); // Force kill when the close command fails     
                            }
                        }
                        this.Dispatcher.BeginInvoke(new Action(delegate
                        {
                            System.Windows.Application.Current.Shutdown();
                        }));
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Reload_R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Robot_CZ("Reload_R", 12);
            }
            catch (Exception)
            {
                return;
            }
        }
        /// <summary>
        /// Status: 0 off, 1 on, 2 powered up, 3 power on, 4 power off, 5 enabled, 6 disabled, 7 running, 8 resume, 9 alarm, 10 paused, 11 stopped, 12 robot status query
        /// </summary>
        /// <param name="robotName"></param>
        /// <param name="type"> </param>
        public void Robot_CZ(string robotName, int type)
        {
            try
            {
                ClientList clientList = new ClientList();
                MessageClientList coffeelist = MessageClientList.Instance;
                coffeelist.Name = robotName;
                coffeelist.type = type;
                clientList.message = coffeelist;
                clientList.code = 2;
                client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientList)));
            }
            catch (Exception)
            {
                return;
            }
        }
        private void L_SD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && L_SD.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_L", 2);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void L_BJ_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && L_BJ.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_L", 9);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void L_BTSD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && L_BTSD.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_L", 3);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void L_BTXD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && L_BTXD.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_L", 4);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void L_SSN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && L_SSN.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_L", 5);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void L_XSN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && L_XSN.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_L", 6);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void L_YXCX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && L_YXCX.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_L", 7);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void L_TZCX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && L_TZCX.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_L", 11);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void L_ZTCX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && L_ZTCX.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_L", 10);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void L_JXCX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && L_JXCX.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_L", 8);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void R_SD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && R_SD.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_R", 2);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void R_BJ_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && R_BJ.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_R", 9);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void R_BTSD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && R_BTSD.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_R", 3);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void R_BTXD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && R_BTXD.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_R", 4);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void R_SSN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && R_SSN.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_R", 5);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void R_XSN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && R_XSN.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_R", 6);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void R_YXCX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && R_YXCX.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_R", 7);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void R_TZCX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && R_TZCX.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_R", 11);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void R_ZTCX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && R_ZTCX.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_R", 10);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void R_JXCX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake && R_JXCX.Tag.ToString() == "1")
                {
                    Robot_CZ("Reload_R", 8);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake)
                {
                    Send("SFW1", DO1_btn.Tag.ToString());
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake)
                {
                    Send("SFW2", DO1_btn.Tag.ToString());
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake)
                {
                    Send("JZK", DO1_btn.Tag.ToString());
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void TextBlock_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMake)
                {
                    Send("JZG", DO1_btn.Tag.ToString());
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            jiqiQuYu.Visibility = Visibility.Collapsed;
            jiqiquyuSel.Visibility = Visibility.Visible;
            JAKAQuYu.Visibility = Visibility.Collapsed;
            MiMa.Visibility = Visibility.Collapsed;
            exit1.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pas.Text == "123456")
                {
                    if (cz == 1)
                    {
                        jiqiQuYu.Visibility = Visibility.Visible;
                        jiqiquyuSel.Visibility = Visibility.Collapsed;
                        JAKAQuYu.Visibility = Visibility.Collapsed;
                        MiMa.Visibility = Visibility.Collapsed;
                        exit1.Visibility = Visibility.Visible;
                        pas.Text = "";
                    }
                    else if (cz == 2)
                    {
                        jiqiQuYu.Visibility = Visibility.Collapsed;
                        jiqiquyuSel.Visibility = Visibility.Collapsed;
                        JAKAQuYu.Visibility = Visibility.Visible;
                        MiMa.Visibility = Visibility.Collapsed;
                        exit1.Visibility = Visibility.Visible;
                        pas.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("The password is incorrect");
                    pas.Text = "";
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        private void FY_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (FY.Tag.ToString() == "0")
                {
                    FY.Tag = 1;
                    this.Dispatcher.BeginInvoke(new Action(delegate
                    {
                        FY.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"向左.png"));
                        jiqi1.Visibility = Visibility.Collapsed;
                        jiqi2.Visibility = Visibility.Visible;
                    }));
                }
                else
                {
                    FY.Tag = 0;
                    this.Dispatcher.BeginInvoke(new Action(delegate
                    {
                        FY.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"向右.png"));
                        jiqi2.Visibility = Visibility.Collapsed;
                        jiqi1.Visibility = Visibility.Visible;
                    }));
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        private void ChaiC_T_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("ChaiC", ChaiC_Txt.Text);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void MOC_T_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("MOC", MOC_Txt.Text);

            }
            catch (Exception)
            {
                return;
            }
        }

        private void BaiST_T_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("BaiST", BaiST_Txt.Text);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void QiaoKL_T_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("QKL1", QiaoKL_Txt.Text);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void QiaoKL2_T_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("QKL2", QiaoKL2_Txt.Text);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void QiaoKL3_T_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Send("QKL3", QiaoKL3_Txt.Text);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}


