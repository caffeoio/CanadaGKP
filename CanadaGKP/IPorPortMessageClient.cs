using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanadaGKP
{
    public class IPorPortMessageClient
    {
        private static IPorPortMessageClient instance;
        private IPorPortMessageClient() { }
        public static IPorPortMessageClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IPorPortMessageClient();
                }
                return instance;
            }
        }
        /// <summary>
        /// 咖啡机ip
        /// </summary>
        public string CoffeeIP { get; set; } = "192.168.0.122";
        /// <summary>
        /// 咖啡机端口号
        /// </summary>
        public string CoffeePort { get; set; } = "5555";
    }
}
