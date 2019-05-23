using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject
{
    [Serializable]
    class TaskHandler
    {
        private List<object[]> allTasks; 
        private string fileName;

        public TaskHandler(string name)
        {

            //assume name is valid
            this.fileName = name;
            //check if there is already a file with this needed data, and open a new one if not
            if (!File.Exists(name + ".bin"))
            {
                Stream myFileStream = File.Create(name + ".bin");
                myFileStream.Close();
                allTasks = new List<object[]>();
            }
            //deserialize the list of User's from the file with this name
            else
            {
                Stream ReadFileStream = File.OpenRead(name + ".bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                if (new FileInfo(name + ".bin").Length != 0)
                    allTasks = (List<object[]>)deserializer.Deserialize(ReadFileStream);
                else allTasks = new List<object[]>();
                ReadFileStream.Close();
            }
        }

        public bool AddTaskToFile(string boardId, int taskId, string title, string body, DateTime creationDate, DateTime dueDate, int column)
        {
            object[] task = new object[7];
            task[0] = boardId;
            task[1] = taskId;
            task[2] = title;
            task[3] = body;
            task[4] = creationDate;
            task[5] = dueDate;
            task[6] = column;
            allTasks.Add(task);
                     
            if (File.Exists(fileName + ".bin"))
            {
                if (DeleteFile())
                {
                    if (OpenNewFile())
                    {
                        Stream fileStream = File.OpenWrite(fileName+ ".bin");
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fileStream, allTasks);
                        fileStream.Close();
                        return true;
                    }
                }
            }
            //if the update failed- dont change this list and return false
            
            allTasks.Remove(task);
            return false;
        }

        public bool UpdateTask(string boardId, int taskId, string title, string body, DateTime creationDate, DateTime dueDate, int column)
        {
            foreach (object[] task in allTasks)
            {
                
                if (task[0].Equals(boardId))
                {
                    if(task[1].Equals(taskId))
                    allTasks.Remove(task);
                }
                    
            }
            object[] t = new object[7];
            t[0] = boardId;
            t[1] = taskId;
            t[2] = title;
            t[3] = body;
            t[4] = creationDate;
            t[5] = dueDate;
            t[6] = column;
            allTasks.Add(t);


            if (File.Exists(fileName + ".bin"))
            {
                if (DeleteFile())
                {
                    if (OpenNewFile())
                    {
                        Stream fileStream = File.OpenWrite(fileName + ".bin");
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fileStream, allTasks);
                        fileStream.Close();
                        return true;
                    }
                }
            }
            //if the update failed- dont change this list and return false
            //list.Remove(user);
            return false;
        }

        public bool RemoveTask(string boardId, int taskId, string title, string body, DateTime creationDate, DateTime dueDate, int column)
        {
            foreach (object[] task in allTasks)
            {

                if (task[0].Equals(boardId))
                {
                    if (task[1].Equals(taskId))
                        allTasks.Remove(task);
                }

            }
            if (File.Exists(fileName + ".bin"))
            {
                if (DeleteFile())
                {
                    if (OpenNewFile())
                    {
                        Stream fileStream = File.OpenWrite(fileName + ".bin");
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fileStream, allTasks);
                        fileStream.Close();
                        return true;
                    }
                }
            }
            //if the update failed- dont change this list and return false
            //list.Remove(user);
            return false;
        }

            /// <summary>
            /// delete the file with this name
            /// </summary>
            /// <returns>true if succsed, else false</returns>
            private bool DeleteFile()
        {
            //assume there is a file with this name
            File.Delete(fileName + ".bin");
            return !(File.Exists(fileName + ".bin"));
        }

        /// <summary>
        /// open a new file with this name
        /// </summary>
        /// <returns>true if succsed, else false</returns>
        private bool OpenNewFile()
        {
            //assume there isnt a file with this name
            Stream fileStream = File.Create(fileName + ".bin");
            fileStream.Close();
            return File.Exists(fileName + ".bin");
        }

        /// <summary>
        /// geters and seters of this list
        /// </summary>
        public List<object[]> List
        {
            get { return allTasks ; }
            set { allTasks = value; }
        }

    }

}

