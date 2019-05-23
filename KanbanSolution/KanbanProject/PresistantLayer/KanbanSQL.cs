using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace KanbanProject.PresistantLayer
{
    class KanbanSQL
    {
        static string database_name = "MyKanbanDatabase.sqlite";
        static SQLiteConnection connection = new SQLiteConnection($"Data Source={database_name};Version=3;");
        SQLiteCommand command;

        public KanbanSQL()
        {
            // string database_name = "MyKanbanDatabase.sqlite";
            SQLiteConnection.CreateFile(database_name);
            // SQLiteConnection connection = new SQLiteConnection($"Data Source={database_name};Version=3;");

            connection.Open();
            string sql = "create table users (email varchar(25),password varchar(20))";
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            sql = "create table columns (userId varchar(25),boardId int,maxLength int,colName varchar(20),colNum int)";
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            sql = "create table tasks (userId varchar(25),boardId int,title varchar(50),body varchar(300), creation DATETIME, due DATETIME,colName varchar(20))";
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            sql = "create table boards (userId varchar(25),boardId int,boardName varchar(20))";
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();

        }

        public bool AddUser(string email, string password)
        {
            try
            {
                connection.Open();
                connection.Open();
                command = new SQLiteCommand(null, connection);
                command.CommandText = "INSERT INTO columns (email,password)" + "VALUES(@email,@password)";
                SQLiteParameter email_param = new SQLiteParameter(@"email", email);
                SQLiteParameter password_param = new SQLiteParameter(@"password", password);
                command.Parameters.Add(email_param);
                command.Parameters.Add(password_param);
                command.Prepare();
                int num_row_changed = command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool AddThreeColumn(string userId, int boardId, string colName)
        {
            try
            {
                connection.Open();
                // backLog
                command = new SQLiteCommand(null, connection);
                command.CommandText = "INSERT INTO users (userId,boardId,maxLength,colName,colNum)" + "VALUES(@userId,@boardId,@maxLength,@colName,@colNum)";
                SQLiteParameter userId_param = new SQLiteParameter(@"userId", userId);
                SQLiteParameter boardId_param = new SQLiteParameter(@"boardId", boardId);
                SQLiteParameter maxLength_param = new SQLiteParameter(@"maxLength", -1);
                SQLiteParameter colName_param = new SQLiteParameter(@"colName", "backlog");
                SQLiteParameter colNum_param = new SQLiteParameter(@"colNum", 1);
                command.Parameters.Add(userId_param);
                command.Parameters.Add(boardId_param);
                command.Parameters.Add(maxLength_param);
                command.Parameters.Add(colName_param);
                command.Parameters.Add(colNum_param);
                command.Prepare();
                int num_row_changed = command.ExecuteNonQuery();
                command.Dispose();

                //inprogress
                command = new SQLiteCommand(null, connection);
                command.CommandText = "INSERT INTO users (userId,boardId,maxLength,colName,colNum)" + "VALUES(@userId,@boardId,@maxLength,@colName,@colNum)";
                userId_param = new SQLiteParameter(@"userId", userId);
                boardId_param = new SQLiteParameter(@"boardId", boardId);
                maxLength_param = new SQLiteParameter(@"maxLength", -1);
                colName_param = new SQLiteParameter(@"colName", "inProgress");
                colNum_param = new SQLiteParameter(@"colNum", 2);
                command.Parameters.Add(userId_param);
                command.Parameters.Add(boardId_param);
                command.Parameters.Add(maxLength_param);
                command.Parameters.Add(colName_param);
                command.Parameters.Add(colNum_param);
                command.Prepare();
                num_row_changed = command.ExecuteNonQuery();
                command.Dispose();

                //Done
                command = new SQLiteCommand(null, connection);
                command.CommandText = "INSERT INTO users (userId,boardId,maxLength,colName,colNum)" + "VALUES(@userId,@boardId,@maxLength,@colName,@colNum)";
                userId_param = new SQLiteParameter(@"userId", userId);
                boardId_param = new SQLiteParameter(@"boardId", boardId);
                maxLength_param = new SQLiteParameter(@"maxLength", -1);
                colName_param = new SQLiteParameter(@"colName", "done");
                colNum_param = new SQLiteParameter(@"colNum", 3);
                command.Parameters.Add(userId_param);
                command.Parameters.Add(boardId_param);
                command.Parameters.Add(maxLength_param);
                command.Parameters.Add(colName_param);
                command.Parameters.Add(colNum_param);
                command.Prepare();
                num_row_changed = command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool AddBoard(string userId, int boardId, string boardName)
        {
            try
            {
                connection.Open();
                command = new SQLiteCommand(null, connection);
                command.CommandText = "INSERT INTO boards (userId,boardId,boardName)" + "VALUES(@userId,@boardId,@boardId)";
                SQLiteParameter userId_param = new SQLiteParameter(@"userId", userId);
                SQLiteParameter boardId_param = new SQLiteParameter(@"boardId", boardId);
                SQLiteParameter boardName_param = new SQLiteParameter(@"maxLength", boardName);
                command.Parameters.Add(userId_param);
                command.Parameters.Add(boardId_param);
                command.Parameters.Add(boardName_param);

                command.Prepare();
                int num_row_changed = command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
     
        }
    

        public bool AddColumn(string userId, int boardId, int maxLength, string colName)
        {
            try
            {
                connection.Open();
                command = new SQLiteCommand(null, connection);
                command.CommandText = "INSERT INTO users (userId,boardId,maxLength,colName,colNum)" + "VALUES(@userId,@boardId,@maxLength,@colName,@colNum)";
                SQLiteParameter userId_param = new SQLiteParameter(@"userId", userId);
                SQLiteParameter boardId_param = new SQLiteParameter(@"boardId", boardId);
                SQLiteParameter maxLength_param = new SQLiteParameter(@"maxLength", maxLength);
                SQLiteParameter colName_param = new SQLiteParameter(@"colName", colName);
                SQLiteParameter colNum_param = new SQLiteParameter(@"colNum", 1);
                command.Parameters.Add(userId_param);
                command.Parameters.Add(boardId_param);
                command.Parameters.Add(maxLength_param);
                command.Parameters.Add(colName_param);
                command.Parameters.Add(colNum_param);

                command.Prepare();
                int num_row_changed = command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        
        public bool AddTask(string userId,int boardId,string title,string body,DateTime creation,DateTime due,string colName)
        {
            
              try
            {
                connection.Open();
                command = new SQLiteCommand(null, connection);
                command.CommandText = "INSERT INTO tasks (userId,boardId,title,body,creation,due,colName)" + "VALUES(@userId,@boardId,@title,@body,@creation,@due,@colName)";
                SQLiteParameter userId_param = new SQLiteParameter(@"userId", userId);
                SQLiteParameter boardId_param = new SQLiteParameter(@"boardId", boardId);
                SQLiteParameter title_param = new SQLiteParameter(@"title", title);
                SQLiteParameter body_param = new SQLiteParameter(@"body", body);
                SQLiteParameter creation_param = new SQLiteParameter(@"creation", creation);
                SQLiteParameter due_param = new SQLiteParameter(@"due", due);
                SQLiteParameter colName_param = new SQLiteParameter(@"colName", colName);
                command.Parameters.Add(userId_param);
                command.Parameters.Add(boardId_param);
                command.Parameters.Add(title_param);
                command.Parameters.Add(body_param);
                command.Parameters.Add(creation_param);
                command.Parameters.Add(due_param);
                command.Parameters.Add(colName_param);

                command.Prepare();
                int num_row_changed = command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        

    }
}
