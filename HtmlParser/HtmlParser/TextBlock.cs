using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser
{
    class TextBlock
    {
        public int textBlockID { get; set; }
        public int containerID { get; set; }
        public string content { get; set; }

        public string getSQL()
        {
            string sql ="INSERT INTO TextBlock (container_ID, content) VALUES('" + containerID + "', '" + content + "');";
            return sql;
        }
    }
}
