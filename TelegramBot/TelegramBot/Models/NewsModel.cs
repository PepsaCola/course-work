using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class NewsModel
    {
        public BR br { get; set; }
        //public DataNews data { get; set; }
    }
    //public class DataNews
    //{
    //    public BR br { get; set; }
    //}
    public class BR
    {
        public string image { get; set; }
        public List<Motds> motds { get; set; }
        
    }
   public class Motds
    {
        public string title { get; set; }
        public string body { get; set; }
        public string image { get; set; }
    }

}
