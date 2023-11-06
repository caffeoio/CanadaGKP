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
        /// 触发事件名
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 状态 0 无状态  1 开状态 2 已开机 3 已上电 4 已使能 5 已运行 6 已报警 7 已暂停  8 已停止
        /// </summary>
        public int type { get; set; } = 0; 
    }
    public class ClientList
    {
        public MessageClientList message { get; set; } = MessageClientList.Instance;
        public DigitalMsgBol MsgBol { get; set; } = DigitalMsgBol.Instance;
        /// <summary>
        /// 状态 0 单状态  1 全状态,2机器人状态
        /// </summary>
        public int code { get; set; } = 0;
    }
}
