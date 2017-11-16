using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser
{
    class Container
    {
        public int containerID { get; set; }
        public int pageID { get; set; }
        public int parentID { get; set; }
        public string cssID { get; set; }
        public string cssClass { get; set; }
        public string containerType { get; set; }
        public string href { get; set; }
        public string behavior { get; set; }
        public string direction { get; set; }

        public bool isOpen = true;

        public TextBlock textBlock { get; set; }
        public List<Container> containers { get; set; }

        public Container()
        {
            containers = new List<Container>();
        }

        private List<string> getSQL()
        {
            //Get this containers SQL and its textBlocks SQL
            List<string> sql = new List<string>();
            string values = "";

            if (cssID != null)
            {
                cssID = ", '" + cssID + "'";
                values += ", cssID";
            }
            if (cssClass != null)
            {
                cssClass = ", '" + cssClass + "'";
                values += ", cssClass";
            }
            if (href != null)
            {
                href = ", '" + href + "'";
                values += ", href";
            }
            if (behavior != null)
            {
                behavior = ", '" + behavior + "'";
                values += ", behavior";
            }
            if (direction != null)
            {
                direction = ", '" + direction + "'";
                values += ", direction";
            }


            sql.Add("INSERT INTO Container (page_ID, parentID, containerType"+ values + ") VALUES('" + pageID + "', '" + parentID + "', '" + containerType + "'" + cssID + cssClass + href + behavior + direction + ");");

            if(textBlock != null)
                sql.Add(textBlock.getSQL());


            return sql;
        }

        public IEnumerable<string> getAllSQL()
        {
            IEnumerable<string> all = getSQL();

            if (containers.Count != 0)
            {
                foreach (Container container in containers)
                {
                    all = all.Concat(container.getAllSQL());
                }
            }

            return all;
        }
    }
}
