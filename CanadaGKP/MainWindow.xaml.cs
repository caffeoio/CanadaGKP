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
using Newtonsoft.Json;

namespace CanadaGKP
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static string IPorPortUrl = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\IPorPortMessageClient.txt";
        /// <summary>
        /// 创建客户端
        /// </summary>
        Socket client;
        public bool IsMake = true;
        public void CoffeeClient()
        {
            try
            {
                ///创建客户端
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                ///IP地址
                IPAddress ip = IPAddress.Parse(IPorPortMessageClient.Instance.CoffeeIP);
                ///端口号
                IPEndPoint endPoint = new IPEndPoint(ip, int.Parse(IPorPortMessageClient.Instance.CoffeePort));
                ///建立与服务器的远程连接
                client.Connect(endPoint);
                ///线程问题
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
                LTypeKJ_L.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LTypeBJ_L.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LOpenBTDY_L.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LCloseBTDY_L.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LOpenSSN_L.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LCloseSSN_L.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LOpenYXCX_L.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LCloseTZCX_L.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LContinueZTCX_L.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LCloseJXCX_L.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
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
                    LTypeKJ_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    L_SD.Tag = "1";
                }
                else if (robotMsg.Robot_YKJ_L)
                {
                    LOpenBTDY_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    L_BTSD.Tag = "1";
                }
                else if (robotMsg.Robot_SD_L)
                {
                    LCloseBTDY_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    LOpenSSN_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    L_BTXD.Tag = "1";
                    L_SSN.Tag = "1";
                }
                else if (robotMsg.Robot_SN_L)
                {
                    LOpenYXCX_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    LCloseSSN_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    L_XSN.Tag = "1";
                    L_YXCX.Tag = "1";
                }
                else if (robotMsg.Robot_YX_L)
                {
                    LCloseTZCX_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    LContinueZTCX_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    L_ZTCX.Tag = "1";
                    L_TZCX.Tag = "1";
                }
                else if (robotMsg.Robot_TZ_L)
                {
                    LOpenYXCX_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    LCloseSSN_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    L_XSN.Tag = "1";
                    L_YXCX.Tag = "1";
                }
                else if (robotMsg.Robot_ZT_L)
                {
                    LCloseTZCX_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    LCloseJXCX_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    L_JXCX.Tag = "1";
                    L_TZCX.Tag = "1";
                }
                else if (robotMsg.Robot_BJ_L)
                {
                    LTypeBJ_L.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
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
                LTypeKJ_R.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LTypeBJ_R.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LOpenBTDY_R.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LCloseBTDY_R.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LOpenSSN_R.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LCloseSSN_R.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LOpenYXCX_R.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LCloseTZCX_R.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LContinueZTCX_R.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LCloseJXCX_R.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
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
                    LTypeKJ_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    R_SD.Tag = "1";
                }
                else if (robotMsg.Robot_YKJ_R)
                {
                    LOpenBTDY_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    R_BTSD.Tag = "1";
                }
                else if (robotMsg.Robot_SD_R)
                {
                    LCloseBTDY_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    LOpenSSN_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    R_BTXD.Tag = "1";
                    R_SSN.Tag = "1";
                }
                else if (robotMsg.Robot_SN_R)
                {
                    LOpenYXCX_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    LCloseSSN_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    R_XSN.Tag = "1";
                    R_YXCX.Tag = "1";
                }
                else if (robotMsg.Robot_YX_R)
                {
                    LCloseTZCX_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    LContinueZTCX_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    R_ZTCX.Tag = "1";
                    R_TZCX.Tag = "1";
                }
                else if (robotMsg.Robot_TZ_R)
                {
                    LOpenYXCX_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    LCloseSSN_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    R_XSN.Tag = "1";
                    R_YXCX.Tag = "1";
                }
                else if (robotMsg.Robot_ZT_R)
                {
                    LCloseTZCX_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    LCloseJXCX_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    R_JXCX.Tag = "1";
                    R_TZCX.Tag = "1";
                }
                else if (robotMsg.Robot_BJ_R)
                {
                    LTypeBJ_R.Background = new SolidColorBrush(Color.FromRgb(8, 242, 247));
                    R_BJ.Tag = "1";
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 客户端接收到服务器发送的消息
        /// </summary>
        /// <param name="o">客户端</param>
        void ReciveMsg(object o)
        {
            Socket client = o as Socket;
            while (true)
            {
                try
                {
                    ///定义客户端接收到的信息大小
                    byte[] arrList = new byte[1024 * 1024];
                    ///接收到的信息大小(所占字节数)
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
                    ///关闭客户端
                    client.Close();
                }

            }
        }
        /// <summary>
        /// 给按钮变颜色
        /// </summary>
        /// <param name="btn">要变颜色得按钮</param>
        /// <param name="type">1，正常 2，接通变1 3，不缺料 4，缺料</param>
        /// <param name="img">要变颜色得按钮</param>
        public void BtnShow(TextBlock btn, int type, Image img)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(delegate
                {
                    if (type == 0)//正常状态do
                    {
                        btn.Tag = 0;
                        img.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"灰圆.png"));
                    }
                    else if (type == 1)//接通变1do
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
        public void DOSelect(string name, int tag)
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
        /// IO点查询
        /// </summary>
        public void DISelect(DigitalMsgBol msgBol)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(delegate
                {
                    AQGS.Background = !msgBol.AQGS_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    CCSDW.Background = !msgBol.CCSDW_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    CCXDW.Background = !msgBol.CCXDW_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    DKLBQFK1.Background = !msgBol.DKLBQ1_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    DKLBQFK2.Background = !msgBol.DKLBQ2_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    DKLBQFK3.Background = !msgBol.DKLBQ3_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    CCJC.Background = !msgBol.CCJC_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    DCJC1.Background = !msgBol.DCJC1_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    DCJC2.Background = !msgBol.DCJC2_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    DCJC3.Background = !msgBol.DCJC3_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    GJC1.Background = !msgBol.QGJCDK1_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    GJC2.Background = !msgBol.QGJCDK2_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    GJC3.Background = !msgBol.QGJCDK3_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    BJC1.Background = !msgBol.QBJCSK1_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    BJC2.Background = !msgBol.QBJCSK2_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    JBCGJC.Background = !msgBol.JBCGJC_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    QKLJC.Background = !msgBol.QKLJLJC_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    NNDYWJC.Background = !msgBol.NNDYW1_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    NNGYWJC.Background = !msgBol.NNGYW2_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    NNJC.Background = !msgBol.NNQL3_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    YMNJC.Background = !msgBol.YMNQL4_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    SJC.Background = !msgBol.WATER_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    GTJC1.Background = !msgBol.GTJC1_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    GTJC2.Background = !msgBol.GTJC2_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    CTJC1.Background = !msgBol.CTJC1_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
                    CTJC2.Background = !msgBol.CTJC2_Bol ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(7, 247, 43));
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
                ///创建客户端
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                ///IP地址
                IPAddress ip = IPAddress.Parse(IPorPortMessageClient.Instance.CoffeeIP);
                ///端口号
                IPEndPoint endPoint = new IPEndPoint(ip, 5555);
                ///建立与服务器的远程连接
                client.Connect(endPoint);
                ClientList clientList = new ClientList();
                clientList.MsgBol = DigitalMsgBol.Instance;
                clientList.code = 99;
                client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientList)));

                ///线程问题
                Thread thread = new Thread(ReciveMsg);
                thread.IsBackground = true;
                thread.Start(client);

            }
            catch (Exception)
            {
                return;
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
                coffeelist.type = int.TryParse(tag, out int do1) ? do1 : 0;
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
                jiqiQuYu.Visibility = Visibility.Visible;
                jiqiquyuSel.Visibility = Visibility.Collapsed;
                JAKAQuYu.Visibility = Visibility.Collapsed;
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
                JAKAQuYu.Visibility = Visibility.Visible;
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
                                    thisProc.Kill(); //当发送关闭窗口命令无效时强行结束进程     
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
        ///   状态  0 关状态  1 开状态 2 已开机 3 上电 4 下电 5 已使能 6 下使能 7 已运行 8 继续运行 9 已报警 10 已暂停  11已停止 ,12 机械臂状态查询
        /// </summary>
        /// <param name="robotName"></param>
        /// <param name="type"> </param>
        public void Robot_CZ(string robotName, int type)
        {
            try
            {
                    ClientList clientList = new ClientList();
                    MessageClientList coffeelist = MessageClientList.Instance;
                    coffeelist.Name =robotName;
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
    }
}
