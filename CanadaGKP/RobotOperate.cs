using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace CanadaGKP
{
    public class RobotOperate
    {
        public static string RobotOperateUrl = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        public static int handle1 = 0;
        public static int handle2 = 0;
        public static double PI = 3.1415926535;
        public static List<string[]> listStrArr1 = new List<string[]>();//数组List，相当于可以无限扩大的二维数组。
        public static List<string[]> listStrArr2 = new List<string[]>();
        static List<string[]> listStrArr3 = new List<string[]>();//数组List，相当于可以无限扩大的二维数组。
        static List<string[]> listStrArr4 = new List<string[]>();
        public static JKTYPE.ProgramState pstatus;
        public static JKTYPE.RobotState state;
        public static JKTYPE.RobotStatus status;
        /// <summary>
        /// 左臂 停止 ，运行，暂停
        /// </summary>
        public static void CX_Start_L()
        {
            try
            {
                jakaAPI.get_program_state(ref handle1, ref pstatus);
            }
            catch (Exception)
            {
                return;
            }
        }
        /// <summary>
        /// 右臂 急停，打开电源，伺服
        /// </summary>
        public static void CX_kj_L()
        {
            try
            {
                jakaAPI.get_robot_state(ref handle1, ref state);
            }
            catch (Exception)
            {
                return;
            }
        }
        /// <summary>
        /// 左机器人 急停 打开电源，伺服
        /// </summary>
        public static void CX_status_L()
        {
            try
            {
                jakaAPI.get_robot_status(ref handle1, ref status);
            }
            catch (Exception)
            {
                return;
            }
        }
        /// <summary>
        /// 右臂 停止 ，运行，暂停
        /// </summary>
        public static void CX_Start_R()
        {
            try
            {
                jakaAPI.get_program_state(ref handle2, ref pstatus);
            }
            catch (Exception)
            {
                return;
            }
        }
        /// <summary>
        /// 左机器人 急停 打开电源，伺服
        /// </summary>
        public static void CX_kj_R()
        {
            try
            {
                jakaAPI.get_robot_state(ref handle2, ref state);
            }
            catch (Exception)
            {
                return;
            }
        }
        public static void CX_status_R()
        {
            try
            {
                jakaAPI.get_robot_status(ref handle2, ref status);
            }
            catch (Exception)
            {
                return;
            }
        }
        public static void Robot_Initialisation_double()//机器人初始化
        {
            int tool_idset1 = 0; int tool_idset2 = 0;
            int frame_idset1 = 0; int frame_idset2 = 0;
            int result1 = -1;
            while (result1 == -1)
            {
                result1 = jakaAPI.create_handler(IPorPortMessageClient.Instance.CoffeeIPL.ToCharArray(), ref handle1);
                Thread.Sleep(100);
            }
            int result2 = -1;
            while (result2 == -1)
            {
                result2 = jakaAPI.create_handler(IPorPortMessageClient.Instance.CoffeeIPR.ToCharArray(), ref handle2);
                Thread.Sleep(100);
            }
            //切换当前使用的工具坐标
            jakaAPI.set_tool_id(ref handle1, tool_idset1); Console.WriteLine("L_Robot_T " + tool_idset1);
            jakaAPI.set_tool_id(ref handle2, tool_idset2); Console.WriteLine("R_Robot_T " + tool_idset2);

            //切换当前使用的世界坐标
            jakaAPI.set_user_frame_id(ref handle1, frame_idset1); Console.WriteLine("L_Robot_F " + frame_idset1);
            jakaAPI.set_user_frame_id(ref handle2, frame_idset2); Console.WriteLine("R_Robot_F " + frame_idset2);

            StringBuilder Version = new StringBuilder("", 3000);
            int result3 = jakaAPI.get_sdk_version(ref handle1, Version);
            Console.WriteLine("SDK 版本 " + Version);
            //Form1.QK();
            //Form1.QW();
        }
    }
}
