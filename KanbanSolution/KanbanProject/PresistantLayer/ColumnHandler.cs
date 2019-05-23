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
    class ColumnHandler
    {
        private List<object[]> allColumns; 
        private string fileName;

        public ColumnHandler(string name)
        {

            //assume name is valid
            this.fileName = name;
            //check if there is already a file with this needed data, and open a new one if not
            if (!File.Exists(name + ".bin"))
            {
                Stream myFileStream = File.Create(name + ".bin");
                myFileStream.Close();
                allColumns = new List<object[]>();
            }
            //deserialize the list of User's from the file with this name
            else
            {
                Stream ReadFileStream = File.OpenRead(name + ".bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                if (new FileInfo(name + ".bin").Length != 0)
                    allColumns = (List<object[]>)deserializer.Deserialize(ReadFileStream);
                else allColumns = new List<object[]>();
                ReadFileStream.Close();
            }
        }

        public bool Add3ColumnToFile(int boardId)
        {
            object[] column1 = new object[3];
            column1[0] = boardId;
            column1[1] = 0; //length number of tasks
            column1[2] = 1; //column to-do
            allColumns.Add(column1);
            object[] column2 = new object[3];
            column2[0] = boardId;
            column2[1] = 0; //length number of tasks
            column2[2] = 2; //column in progress
            allColumns.Add(column2);
            object[] column3 = new object[3];
            column3[0] = boardId;
            column3[1] = 0; //length number of tasks
            column3[2] = 3; //column done
            allColumns.Add(column3);

           
            if (File.Exists(fileName + ".bin"))
            {
                if (DeleteFile())
                {
                    if (OpenNewFile())
                    {
                        Stream fileStream = File.OpenWrite(fileName+ ".bin");
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fileStream, allColumns);
                        fileStream.Close();
                        return true;
                    }
                }
            }
            //if the update failed- dont change this list and return false
            allColumns.Remove(column1);
            allColumns.Remove(column2);
            allColumns.Remove(column3);
            return false;
        }

        public bool UpdateColumn(int boardId, int length, int columnNumber)
        {
            foreach (object[] column in allColumns)
            {
                //object[] u =user;
                if (column[0].Equals(boardId))
                {
                    if(column[2].Equals(columnNumber))
                    allColumns.Remove(column);
                }
                    
            }
            object[] col = new object[3];
            col[0] = boardId;
            col[1] = length; //length number of tasks
            col[2] = columnNumber; //column to-do
            allColumns.Add(col);
            

            if (File.Exists(fileName + ".bin"))
            {
                if (DeleteFile())
                {
                    if (OpenNewFile())
                    {
                        Stream fileStream = File.OpenWrite(fileName + ".bin");
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fileStream, allColumns);
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
            get { return allColumns ; }
            set { allColumns = value; }
        }

    }

}

