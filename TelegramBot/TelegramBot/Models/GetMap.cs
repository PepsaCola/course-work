using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class GetMap
    { 
        //public Data Data { get; set; }
        public MapImages Images { get; set; }
    }
    public class Data
    {
        public MapImages Images { get; set; }

    }
    public class MapImages
    {
        public string Pois { get; set; }

    }

}
