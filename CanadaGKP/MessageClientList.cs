using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanadaGKP
{
    public class MessageClientList
    {
        private static MessageClientList instance;
        private MessageClientList() { }
        public static MessageClientList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MessageClientList();
                }
                return instance;
            }
        }
        /// <summary>
        /// 触发事件名   /// Reload_L  Reload_R
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 状态  0 关状态  1 开状态 2 已开机 3 上电 4 下电 5 已使能 6 下使能 7 已运行 8 继续运行 9 已报警 10 已暂停  11已停止 ,12 机械臂状态查询
        /// </summary>
        public double type { get; set; } = 0;
    }
    public class RobotMsg
    {
        private static RobotMsg instance;
        private RobotMsg() { }
        public static RobotMsg Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RobotMsg();
                }
                return instance;
            }
        }
        /// <summary>
        /// 已开机
        /// </summary>
        public bool Robot_YKJ_L { get; set; } = false;
        /// <summary>
        /// 已上电
        /// </summary>
        public bool Robot_SD_L { get; set; } = false;
        /// <summary>
        /// 已使能
        /// </summary>
        public bool Robot_SN_L { get; set; } = false;
        /// <summary>
        /// 已运行
        /// </summary>
        public bool Robot_YX_L { get; set; } = false;
        /// <summary>
        /// 已暂停
        /// </summary>
        public bool Robot_ZT_L { get; set; } = false;
        /// <summary>
        /// 已报警
        /// </summary>
        public bool Robot_BJ_L { get; set; } = false;
        /// <summary>
        /// 已停止
        /// </summary>
        public bool Robot_TZ_L { get; set; } = false;
        /// <summary>
        /// 已开机
        /// </summary>
        public bool Robot_YKJ_R { get; set; } = false;
        /// <summary>
        /// 已上电
        /// </summary>
        public bool Robot_SD_R { get; set; } = false;
        /// <summary>
        /// 已使能
        /// </summary>
        public bool Robot_SN_R { get; set; } = false;
        /// <summary>
        /// 已运行
        /// </summary>
        public bool Robot_YX_R { get; set; } = false;
        /// <summary>
        /// 已暂停
        /// </summary>
        public bool Robot_ZT_R { get; set; } = false;
        /// <summary>
        /// 已报警
        /// </summary>
        public bool Robot_BJ_R { get; set; } = false;
        /// <summary>
        /// 已停止
        /// </summary>
        public bool Robot_TZ_R { get; set; } = false;
    }
    public class ClientList
    {
        public MessageClientList message { get; set; }
        public DigitalMsgBol MsgBol { get; set; }
        public RobotMsg robotMsg { get; set; }
        /// <summary>
        /// 状态 0 单状态  1 全状态,2机器人状态 99 连接状态
        /// </summary>
        public int code { get; set; } = 0;
        /// <summary>
        /// 机器是否在制作中
        /// </summary>
        public bool IsMake { get; set; } = false;
            }
}
