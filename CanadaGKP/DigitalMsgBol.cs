using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanadaGKP
{
    public class DigitalMsgBol
    {
        private static DigitalMsgBol instance;
        private DigitalMsgBol() { }

        public static DigitalMsgBol Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DigitalMsgBol();
                }
                return instance;
            }
        }
        /// <summary>
        /// 安全光栅  true开 false关
        /// </summary>
        public bool AQGS_Bol { get; set; } = false;
        /// <summary>
        /// 出餐上到位  true开 false关
        /// </summary>
        public bool CCSDW_Bol { get; set; } = false;
        /// <summary>
        /// 出餐下到位  true开 false关
        /// </summary>
        public bool CCXDW_Bol { get; set; } = false;
        /// <summary>
        /// 单孔落杯器反馈1  true开 false关
        /// </summary>
        public bool DKLBQ1_Bol { get; set; } = false;
        /// <summary>
        /// 单孔落杯器反馈2  true开 false关
        /// </summary>
        public bool DKLBQ2_Bol { get; set; } = false;
        /// <summary>
        /// 单孔落杯器反馈3  true开 false关
        /// </summary>
        public bool DKLBQ3_Bol { get; set; } = false;
        /// <summary>
        /// 出餐检测  true开 false关
        /// </summary>
        public bool CCJC_Bol { get; set; } = false;
        /// <summary>
        /// 豆仓检测1  true开 false关
        /// </summary>
        public bool DCJC1_Bol { get; set; } = false;
        /// <summary>
        /// 豆仓检测2  true开 false关
        /// </summary>
        public bool DCJC2_Bol { get; set; } = false;
        /// <summary>
        /// 豆仓检测3  true开 false关
        /// </summary>
        public bool DCJC3_Bol { get; set; } = false;
        /// <summary>
        /// 缺盖检测单孔1  true开 false关
        /// </summary>
        public bool QGJCDK1_Bol { get; set; } = false;
        /// <summary>
        /// 缺盖检测单孔2  true开 false关
        /// </summary>
        public bool QGJCDK2_Bol { get; set; } = false;
        /// <summary>
        /// 缺盖检测单孔3  true开 false关
        /// </summary>
        public bool QGJCDK3_Bol { get; set; } = false;
        /// <summary>
        /// 缺杯检测四孔1  true开 false关
        /// </summary>
        public bool QBJCSK1_Bol { get; set; } = false;
        /// <summary>
        /// 缺杯检测四孔2  true开 false关
        /// </summary>
        public bool QBJCSK2_Bol { get; set; } = false;
        /// <summary>
        /// 接杯成功检测  true开 false关
        /// </summary>
        public bool JBCGJC_Bol { get; set; } = false;
        /// <summary>
        /// 接盖成功检测  true开 false关
        /// </summary>
        public bool JGCGJC_Bol { get; set; } = false;
        /// <summary>
        /// 柴拿铁粉检测  true开 false关
        /// </summary>
        public bool CNYFJC_Bol { get; set; } = false;
        /// <summary>
        /// 巧克力缺料检测  true开 false关
        /// </summary>
        public bool QKLQLJC_Bol { get; set; } = false;
        /// <summary>
        /// 牛奶缺料检测（液位）  true开 false关
        /// </summary>
        public bool QKLJLJC_Bol { get; set; } = false;
        /// <summary>
        /// 牛奶转液预报警  true开 false关
        /// </summary>
        public bool BXJC1_Bol { get; set; } = false;
        /// <summary>
        /// 牛奶转液缺料检测（液位）  true开 false关
        /// </summary>
        public bool BXJC2_Bol { get; set; } = false;
        /// <summary>
        /// 燕麦奶缺料检测（液位）  true开 false关
        /// </summary>
        public bool BXJC3_Bol { get; set; } = false;
        /// <summary>
        /// 燕麦奶预报警（液位）  true开 false关
        /// </summary>
        public bool BXJC4_Bol { get; set; } = false;
        /// <summary>
        /// 水位检测  true开 false关
        /// </summary>
        public bool WATER_Bol { get; set; } = true;
        /// <summary>
        /// 果糖检测1  true开 false关
        /// </summary>
        public bool GTJC1_Bol { get; set; } = false;
        /// <summary>
        /// 果糖检测2  true开 false关
        /// </summary>
        public bool GTJC2_Bol { get; set; } = false;
        /// <summary>
        /// 果糖检测3  true开 false关
        /// </summary>
        public bool GTJC3_Bol { get; set; } = false;
        /// <summary>
        /// 茶汤检测1  true开 false关
        /// </summary>
        public bool CTJC1_Bol { get; set; } = false;
        /// <summary>
        /// 茶汤检测2  true开 false关
        /// </summary>
        public bool CTJC2_Bol { get; set; } = false;
        /// <summary>
        /// 茶汤检测3  true开 false关
        /// </summary>
        public bool CTJC3_Bol { get; set; } = false;
    }
}
