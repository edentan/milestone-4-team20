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
    class UserHandler
    {
        private List<object[]>allUsers;
        private string fileName;

        public UserHandler(string name)
        {
           // int[] a = new int[2];
            //allUsers.Add(a);
            //assume name is valid
            this.fileName = name;
            //check if there is already a file with this needed data, and open a new one if not
            if (!File.Exists(name + ".bin"))
            {
                Stream myFileStream = File.Create(name + ".bin");
                myFileStream.Close();
                allUsers = new List<object[]>();
            }
            //deserialize the list of User's from the file with this name
            else
            {
                Stream ReadFileStream = File.OpenRead(name + ".bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                if (new FileInfo(name + ".bin").Length != 0)
                    allUsers = (List<object[]>)deserializer.Deserialize(ReadFileStream);
                else allUsers = new List<object[]>();
                ReadFileStream.Close();
            }
        }

        public bool AddUserToFile(string fileName, string email,string password, int maxLength)
        {
            if (allUsers.Count == 1000000)
                return false;
            object[] user = new object[4];
            user[0] = email;
            user[1] = password;
            user[2] = email;
            user[3] = maxLength;
            allUsers.Add(user);
            
            if (File.Exists(fileName + ".bin"))
            {
                if (DeleteFile())
                {
                    if (OpenNewFile())
                    {
                        Stream fileStream = File.OpenWrite(fileName + ".bin");
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fileStream, allUsers);
                        fileStream.Close();
                        return true;
                    }
                }
            }
            //if the update failed- dont change this list and return false
            allUsers.Remove(user);
            return false;
        }

        public bool UpdateUser(string fileName, string email, string password, int maxLength)
        {
            foreach (object[] user in allUsers)
            {
                //object[] u =user;
                if (user[0].Equals(email))
                    allUsers.Remove(user);
            }
            object[] u = new object[4];
            u[0] = email;
            u[1] = password;
            u[2] = email;
            u[3] = maxLength;
            allUsers.Add(u);

            if (File.Exists(fileName + ".bin"))
            {
                if (DeleteFile())
                {
                    if (OpenNewFile())
                    {
                        Stream fileStream = File.OpenWrite(fileName + ".bin");
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(fileStream, allUsers);
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
            get { return allUsers; }
            set { allUsers = value; }
        }

    }

}

