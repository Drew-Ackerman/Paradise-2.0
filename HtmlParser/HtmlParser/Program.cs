using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace HtmlParser
{
    class Program
    {
        public static int containerID = 1;
        public static int textBlockID = 1;
        public const int pageID = 1;

        static void Main(string[] args)
        {
            string file = "../../HTML.html";
            string fileData = "";
            List<Container> sections = new List<Container>();


            //Make sure the file exists
            if (File.Exists(file))
            {
                using (StreamReader fileReader = new StreamReader(file))
                {
                    fileData = fileReader.ReadToEnd();
                    //Remove new lines from the html
                    fileData = fileData.Replace(Environment.NewLine, "");
                    fileData = fileData.Replace("'", "''");

                    //Split the data at > characters so we can trim out all of the unnecessary spacing.
                    //Using Regular expressions so we can keep the > characters, and because it is very fast.
                    string[] splitData = Regex.Split(fileData, @"(?<=[>])");
                    fileData = "";

                    foreach (string part in splitData)
                    {
                        fileData += part.Trim(' ');
                    }

                    //Console.Write(data);

                    //listBuilder(ref sections, splitData);
                }
            }

            file = "../../SQL.txt";

            if (File.Exists(file))
            {
                //Creating a file stream so that the file is overwritten every time
                FileStream fs = new FileStream(file, FileMode.Truncate);
                using (StreamWriter fileWriter = new StreamWriter(fs))
                {
                    /*foreach (Container container in sections)
                    {
                        var sqlList = container.getAllSQL().ToList();

                        foreach (string sql in sqlList)
                        {
                            fileWriter.WriteLine(sql);
                        }
                    }*/
                    fileWriter.WriteLine(fileData);
                }
            }
           
        }

        //Takes in a single line of data
        public static void listBuilder(ref List<Container> sections, string[] data)
        {
            LinkedList<Container> containerList = new LinkedList<Container>();

           for(int i = 0; i < data.Length; i++)
            {
                //Hit the end of a container
                if (data[i].StartsWith("</") && !data[i].StartsWith("</p"))
                {
                    if (containerList.Count != 0 && containerList.Last().isOpen == true)
                    {
                        //Then remove the container from the list, because it is now closed
                        containerList.Last().isOpen = false;
                        containerList.RemoveLast();
                    }            
                }
                //A new container needs to be made
                else if (data[i].StartsWith("<") && !data[i].StartsWith("<img") && !data[i].StartsWith("</"))
                {
                    //Build a new container
                    Container container = new Container();
                    container.containerID = containerID;
                    container.pageID = pageID;
                    containerID++;

                    //If a container hasnt been opened yet
                    if (sections.Count == 0 || containerList.Count == 0)
                    {
                        //Add the container to the list
                        sections.Add(container);

                        //This is the top of the list
                        container.parentID = 0;

                        //Fill in the containers class and id
                        getContainerData(data[i], ref container);

                        //Add the container to the hierarchy
                        containerList.AddFirst(container);
                    }
                    else if (containerList.Count != 0 && containerList.Last().isOpen == true)
                    {
                        //Fill in the containers data
                        container.parentID = containerList.Last().containerID;
                        getContainerData(data[i], ref container);

                        //Add the new container to the parent container
                        containerList.Last().containers.Add(container);

                        //Add the new container to the list
                        containerList.AddLast(container);
                    }
                }
                else if (data[i] != "" && !data[i].StartsWith("<img"))
                {
                    //Only add a text block if there isn't embedded containers
                    if (containerList.Last().containers.Count == 0)
                    {
                        //Create the text block and populate its data
                        TextBlock text = new TextBlock();
                        text.containerID = containerID - 1;

                        int endIndex = data[i].Length - containerList.Last().containerType.Length - 3;
                        string content = data[i].Substring(0, endIndex);

                        text.content = content;

                        //getTextData(data[i].Substring(0, data[i]), ref text);

                        //Get the current most textBlockID and increment it
                        text.textBlockID = textBlockID;
                        textBlockID++;

                        //Add the textblock to the correct container
                        containerList.Last().textBlock = text;

                        //Then remove the container from the list, because it is now closed
                        containerList.Last().isOpen = false;
                        containerList.RemoveLast();
                    }
                }
            }
        }

        private static void getContainerData(string data, ref Container container)
        {
            //get rid of the < at the start of a container and the > at the end
            data = data.Substring(1, data.Length - 2);

            //Get the container type
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != ' ')
                {
                    //Add the character
                    container.containerType += data[i];
                }
                else
                {
                    //Delete the data just stored
                    data = data.Remove(0, i+1);
                    data = data.Trim(' ');
                    //break out
                    break;
                }
            }

            //Storing the parameter type
            string temp = "";

            //Get the other data
            for (int i = 0; i < data.Length; i++)
            {
                //Make sure the data isn't empty
                if (data.Length != 0)
                {
                    
                    if (data[i] != '=' && data[i] != ' ' && data[i] != '"')
                    {
                        temp += data[i];
                    }
                    else if (data[i] == '"')
                    {
                        //Delete the data, the = sign, and the first "
                        data = data.Remove(0, i+1);
                        data = data.Trim(' ');

                        if (temp == "class")
                        {
                            //Get the class
                            for (int j = 0; j < data.Length; j++)
                            {
                                if (data[j] != '"')
                                {
                                    //Add the characters to create the class
                                    container.cssClass += data[j];
                                }
                                else
                                {
                                    //Delete the data just stored
                                    data = data.Remove(0, j+1);
                                    data = data.Trim(' ');
                                    //reset temp and i
                                    temp = "";
                                    i = -1;
                                    break;
                                }
                            }
                        }
                        else if (temp == "id")
                        {
                            //Get the id
                            for (int j = 0; j < data.Length; j++)
                            {
                                if (data[j] != ' ' && data[j] != '"')
                                {
                                    //Add the characters to create the class
                                    container.cssID += data[j];
                                }
                                else if (data[j] == '"')
                                {
                                    //Delete the data just stored
                                    data = data.Remove(0, j+1);
                                    data = data.Trim(' ');
                                    //reset temp and i
                                    temp = "";
                                    i = -1;
                                    break;
                                }
                            }
                        }
                        else if (temp == "href")
                        {
                            //Get the id
                            for (int j = 0; j < data.Length; j++)
                            {
                                if (data[j] != ' ' && data[j] != '"')
                                {
                                    //Add the characters to create the class
                                    container.href += data[j];
                                }
                                else if (data[j] == '"')
                                {
                                    //Delete the data just stored
                                    data = data.Remove(0, j + 1);
                                    data = data.Trim(' ');
                                    //reset temp and i
                                    temp = "";
                                    i = -1;
                                    break;
                                }
                            }
                        }
                        else if (temp == "behavior")
                        {
                            //Get the id
                            for (int j = 0; j < data.Length; j++)
                            {
                                if (data[j] != ' ' && data[j] != '"')
                                {
                                    //Add the characters to create the class
                                    container.behavior += data[j];
                                }
                                else if (data[j] == '"')
                                {
                                    //Delete the data just stored
                                    data = data.Remove(0, j + 1);
                                    data = data.Trim(' ');
                                    //reset temp and i
                                    temp = "";
                                    i = -1;
                                    break;
                                }
                            }
                        }
                        else if (temp == "direction")
                        {
                            //Get the id
                            for (int j = 0; j < data.Length; j++)
                            {
                                if (data[j] != ' ' && data[j] != '"')
                                {
                                    //Add the characters to create the class
                                    container.direction += data[j];
                                }
                                else if (data[j] == '"')
                                {
                                    //Delete the data just stored
                                    data = data.Remove(0, j + 1);
                                    data = data.Trim(' ');
                                    //reset temp and i
                                    temp = "";
                                    i = -1;
                                    break;
                                }
                            }
                        }

                    }
                }
                else
                {
                    //Break out of the loop
                    break;
                }
            }
        }

       /* private static void getTextData(string data, ref TextBlock text)
        {
            //Get the other data
            for (int i = 0; i < data.Length; i++)
            {
                //Make sure the data isn't empty
                if (data.Length != 0)
                {
                    //Break out if we hit a < such as "Hi</p>"
                    if (data[i] == '<')
                        break;
                    else
                        text.content += data[i];
                }
                else
                {
                    //Break out of the loop if its empty
                    break;
                }
            }
        }*/
    }
}
