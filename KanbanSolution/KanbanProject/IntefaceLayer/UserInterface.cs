using System;
using System.Linq;

namespace KanbanProject.IntefaceLayer
{
    class UserInterface
    {
        private static KanbanBoard kanban;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Start()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hello and welcome to Kanban board system!\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Please choose one of the following options:");
            Console.ResetColor();
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("NOTE : In order to go back to the start menu, press '- 1'.\n");
            Console.ResetColor();
            Console.Write("Please enter your choise: ");
            string choiseStart = Console.ReadLine();
            ChooseStart(choiseStart); // call the function that sorts the user's choise.
        }

        private static void ChooseStart(string choiseStart)
        {

            if (choiseStart.Length == 1) // if the length of the option that the user entered is correct
            {
                switch (choiseStart)
                {
                    case "1": // in case the user wants to register
                        Register();
                        Start();
                        break;
                    case "2": // in case the user wants to login
                              // isLogged = false;
                        Login();
                        break;
                    case "3": // in case the user wants to exit
                        Exit();
                        break;
                    default: // in every other case
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Log.Info("Invalid input");
                            Console.WriteLine("Please enter a valid choise.");
                            Console.ResetColor();
                            choiseStart = Console.ReadLine();
                            ChooseStart(choiseStart);
                        }
                        break;
                }
            }
            else if (choiseStart.Equals("-1")) // if the user wants to go to home menu
                Start();
            else // if the length of the option that the user entered is incorrect
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log.Info("Invalid input");
                Console.WriteLine("Please enter a valid choise.");
                Console.ResetColor();
                choiseStart = Console.ReadLine();
                ChooseStart(choiseStart);
            }

        }

        public static void Register()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Thank you for choosing to register to Kanban!");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("NOTE : In order to go back to the start menu, press '-1' at any time.");
            Console.WriteLine("Please enter a valid email address, that hasn't been used in this system.\n");
            Console.ResetColor();
            Console.Write("EmailAddress : ");
            string email = Console.ReadLine();
            email = CheckEmail(email); //check if the email is valid and if it is new in the system
            if (email != "-1")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Please enter a password.");
                Console.WriteLine("Your password must be in length of 4 to 20 characters,");
                Console.WriteLine("and must include at least one capital character, one small character and a number.\n");
                Console.ResetColor();
                Console.Write("Password : ");
                string password = Console.ReadLine();
                password = CheckPassword(password);
                if (password != "-1")// the user doesn't want to go to the menu
                {

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("In this system you can limit the number of tasks in each column.");
                    Console.WriteLine("Please insert a number which will limit your tasks quantity in each column.\n");
                    Console.ResetColor();
                    string maxLength = Console.ReadLine();
                    int validLength = ValidNumber(maxLength);
                    while (validLength == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Log.Info("Invalid input");
                        Console.WriteLine("Please enter a valid number.");
                        Console.ResetColor();
                        maxLength = Console.ReadLine();
                        validLength = ValidNumber(maxLength);
                    }
                    kanban.Register(email, password, validLength);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations! You are now registered to Kanban!");
                    Console.ResetColor();
                    Log.Info("New registration by user with email: " + email);
                }
            }
            else //if the user wants to go to home menu
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Registretion failed.");
                Console.ResetColor();
                Start();
            }
        }

        private static string CheckEmail(string email)
        {
            if (email == "-1")
                return email;
            else
            {

                bool temp = kanban.IsValidEmail(email) && kanban.IsUniqueEmail(email);
                if (temp)
                    return email;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Log.Warn("Invalid input: email isnt a valid email or already been used in the system");
                    Console.WriteLine("please enter a valid email address and make sure it hasn't been used in this system before.");
                    Console.ResetColor();
                    Console.Write("EmailAddress : ");
                    email = Console.ReadLine();
                    return CheckEmail(email);
                }

            }

        }

        private static string CheckPassword(string password)
        {
            if (password == "-1")
                return password;//if the user want to go back to the menu
            else
            {
                bool temp = kanban.IsValidPassword(password);
                if (temp)
                    return password;
                else// if the password is invalid
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Log.Warn("Invalid input: password do not contain the requested elements");
                    Console.WriteLine("please enter a valid password, make sure it has valid length and includes all the requested characters.");
                    Console.ResetColor();
                    Console.Write("Password : ");
                    password = Console.ReadLine();
                    return CheckPassword(password);
                }
            }
        }

        public static int ValidNumber(string number)//checks if the string contains a valid number
        {
            for (int i = 0; i < number.Count(); i = i + 1)
            {
                if (number[i] > '9' | number[i] < '0')
                    return -1;
            }
            int counter = 1;
            int output = 0;
            for (int j = number.Count() - 1; j >= 0; j = j - 1)
            {
                output = (number[j] - '0') * counter + output;
                counter = counter * 10;
            }
            return output;
        }

        public static void Login()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hello Kanban user! Please enter your email and password.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("NOTE : In order to go back to the menu, press '-1'\n");
            Console.ResetColor();
            Console.Write("Email : ");
            string email = Console.ReadLine();
            if (email != "-1")
            {
                if (kanban.IsValidEmail(email))
                {
                    if (!(kanban.IsUniqueEmail(email)))
                    {
                        Console.Write("Password : ");
                        string password = Console.ReadLine();
                        if (password != "-1")
                        {
                            if (kanban.LogIn(email, password))//password and email are in the system;
                            {
                                Console.WriteLine("");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Logeed in successfully!\n");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Log.Info("User with email " + email + " logged in.");
                                Console.ResetColor();
                                PrintBoard();
                                UserOption();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Log.Error("Password do not match to user.");
                                Console.WriteLine("Login failed! Your password is incorrect.\n");
                                Console.ResetColor();
                                Login();
                            }
                        }
                        else //if the user wants to go back to menu
                        {
                            Start();
                        }
                    }
                    else //if the email isnt in the system
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Log.Warn("Email address does not exist in the system.");
                        Console.WriteLine("Please enter a valid email!\n");
                        Console.ResetColor();
                        Login();
                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Log.Warn("Invalid input: Email address isnt legit.");
                    Console.WriteLine("Please enter a valid email adress.\n");
                    Console.ResetColor();
                    Login();

                }

            }
            else // the user wants to go back to menu
                Start();

        }

        private static void UserOption()
        { //user's option in his account

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Choose only one option :");
            Console.ResetColor();
            Console.WriteLine("1. AddTask ");
            Console.WriteLine("2. MoveTask");
            Console.WriteLine("3. RemoveTask");
            Console.WriteLine("4. EditTask");
            Console.WriteLine("5. LogOut");
            Console.WriteLine("6. Exit\n");
            Console.Write("Please enter your choise: ");
            string userChoise = Console.ReadLine();
            Console.ResetColor();
            Console.WriteLine("");
            ChooseOption(userChoise); // calls to the function that sorts the user's choise.
        }

        public static void ChooseOption(string userChoise)// sort the user's choice
        {
            if (userChoise.Length == 1)
            {
                switch (userChoise)
                {
                    case "1":// in case the user wants to add task to his board
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("In order to add a task please enter the following details according to the instructions.");
                        Console.WriteLine("The number of tasks in each column is limited, make sure you have space left before trying to add a task.");
                        Console.WriteLine("Your limit is: " + kanban.GetMaxLength() + "\n");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Task's title - contains max 50 characters, not empty.\n");
                        Console.ResetColor();
                        Console.Write("Title :");
                        string title = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Task's description - contains 0-300 characters.");
                        Console.ResetColor();
                        Console.Write("Description :");
                        string body = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Task's due date - must be a date that has not passed yet.");
                        Console.ResetColor();
                        Console.Write("Enter due date (dd/mm/yyyy) : ");
                        string dueDate = Console.ReadLine();
                        while (!IsDateTime(dueDate))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Log.Warn("Invalid date");
                            Console.WriteLine("Please enter a valid date as mentioned.");
                            Console.ResetColor();
                            Console.Write("Enter due date (dd/mm/yyyy) : ");
                            dueDate = Console.ReadLine();
                        }
                        AddTask(title, body, DateTime.Parse(dueDate));
                        break;

                    case "2"://in case the user wants to further a task
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("If you start working on a task or you finish one");
                        Console.WriteLine("you can move your task one column forward:");
                        Console.WriteLine("from 'to do' to 'in progress' and from 'in progress' to 'done'.");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Note: you can't move a task that is located in 'done' column!");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Please enter the following task's details.");
                        Console.ResetColor();
                        Console.Write("Task number :");
                        string taskId = Console.ReadLine();
                        int task = ValidNumber(taskId);
                        if (task == -1) //if the input is invalid
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Log.Warn("Invalid task number");
                            Console.WriteLine("Make sure you enter a valid task number.");
                            Console.ResetColor();
                            UserOption();
                        }
                        else
                            MoveTask(task);
                        break;

                    case "3"://in case the user wants to remove task
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Please enter the following task's details.");
                        Console.ResetColor();
                        Console.Write("Task number :");
                        taskId = Console.ReadLine();
                        task = ValidNumber(taskId);
                        if (task == -1) //if the inputs invalid
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Log.Warn("Invalid task number");
                            Console.WriteLine("Make sure you enter a valid task number.");
                            Console.ResetColor();
                            UserOption();
                        }
                        else
                            RemoveTask(task);
                        break;

                    case "4"://edit task
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Please enter the number of the task you would like to edit");
                        Console.ResetColor();
                        Console.Write("Task number :");
                        taskId = Console.ReadLine();
                        task = ValidNumber(taskId);
                        if (task == -1) //if the inputs invalid
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Log.Warn("Invalid task number");
                            Console.WriteLine("Make sure you enter a valid task number.");
                            Console.ResetColor();
                            UserOption();
                        }
                        else if (kanban.FindTask(task))
                        {
                            EditTaskChooseOption(task);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Log.Warn("Invalid task number");
                            Console.WriteLine("Task number doesn't exist\n");
                            Console.ResetColor();
                            UserOption();
                        }

                        break;

                    case "5"://log out
                        LogOut();
                        break;
                    case "6": // exit the system
                        Exit();
                        break;

                    default: //the user enter incorrect option
                        Console.ForegroundColor = ConsoleColor.Red;
                        Log.Info("Invalid input, this input isn't one of the options");
                        Console.WriteLine("please type only one of the options.\n");
                        Console.ResetColor();
                        UserOption();
                        break;

                }
            }
            else //the user enter incorrect option
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log.Info("Invalid input, this input isn't one of the options");
                Console.WriteLine("please type only one of the options.\n");
                Console.ResetColor();
                UserOption();
                //same as default
            }
        }

        public static bool IsDateTime(string text)
        {
            DateTime dateTime;
            bool isDateTime = false;

            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            isDateTime = DateTime.TryParse(text, out dateTime);
            return isDateTime;
        }

        private static void PrintBoard()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(kanban.PrintBoard());
            Console.ResetColor();
        }

        public static void AddTask(string title, string body, DateTime dueDate)
        {
            if (!kanban.AddTask(title, body, dueDate)) // At least one of the inputs is incorrect
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log.Warn("Task adding has failed, one of the task's inputs was invalid");
                Console.WriteLine("please make sure you are following the instruction carefully.");
                UserOption();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Task has been added.");
                Log.Info("The user added a task");
                PrintBoard();
                UserOption();
            }

        }

        public static void MoveTask(int taskId)
        {
            if (kanban.MoveTask(taskId))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Task has been moved.");
                Console.ResetColor();
                PrintBoard();
                UserOption();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log.Warn("Task moving failed. The column was full or the taskId doesn't exist or the user tried to move a task from the 'done column'");
                Console.WriteLine("Please check if your columns are full, the task Id exists \nand you are not trying to move task from 'done' column.");
                Console.ResetColor();
                UserOption();
            }

        }

        public static void RemoveTask(int taskId)
        {
            if (kanban.RemoveTask(taskId))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Task has been removed.");
                Console.ResetColor();
                PrintBoard();
                UserOption();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log.Warn("Task removing failed, task id does not exist.");
                Console.ResetColor();
                UserOption();
            }

        }

        public static void EditTaskChooseOption(int taskId)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Choose which of the following you would like to change");
            Console.WriteLine("Choose only one option :\n");
            Console.ResetColor();
            Console.WriteLine("1. Task title ");
            Console.WriteLine("2. Task description");
            Console.WriteLine("3. Task due date\n");

            Console.Write("Please enter your choise: ");
            string userChoise = Console.ReadLine();
            Console.WriteLine("");
            EditTaskOptions(userChoise, taskId);
            UserOption();
        }

        public static void EditTaskOptions(string userChoise, int taskId)
        {

            if (userChoise.Length == 1) // if the length of the option that the user entered is correct
            {
                switch (userChoise)
                {
                    case "1": // in case the user wants to register
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Task's title - contains max 50 characters, not empty.\n");
                        Console.ResetColor();
                        Console.Write("New title :");
                        string title = Console.ReadLine();
                        DateTime defult = DateTime.Parse("1/1/20");
                        EditTask(taskId, title, null, defult);

                        break;

                    case "2": // in case the user wants to login
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Task's description - contains 0-300 characters.\n");
                        Console.ResetColor();
                        Console.Write("New Description :");
                        string description = Console.ReadLine();
                        defult = DateTime.Parse("1/1/20");
                        EditTask(taskId, null, description, defult);

                        break;
                    case "3": // in case the user wants to exit
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Task's due date - must be a date that has not passed yet.");
                        Console.ResetColor();
                        Console.Write("Enter due date (dd/mm/yyyy) : ");
                        string dueDate = Console.ReadLine();
                        while (!IsDateTime(dueDate))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Log.Warn("Invalid date");
                            Console.WriteLine("Please enter a valid date as mentioned.");
                            Console.ResetColor();
                            Console.Write("Enter due date (dd/mm/yyyy) : ");
                            dueDate = Console.ReadLine();
                        }
                        EditTask(taskId, null, null, DateTime.Parse(dueDate));

                        break;
                    default: // in every other case
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Log.Info("Invalid input, this input isn't one of the options");
                            Console.WriteLine("please type only one of the options.\n");
                            Console.ResetColor();
                            userChoise = Console.ReadLine();
                            EditTaskOptions(userChoise, taskId);
                        }
                        break;
                }
            }
            else // if the length of the option that the user entered is incorrect
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log.Info("Invalid input, this input isn't one of the options");
                Console.WriteLine("please type only one of the options.\n");
                Console.ResetColor();
                userChoise = Console.ReadLine();
                EditTaskOptions(userChoise, taskId);
            }

        }

        public static void EditTask(int taskId, string title, string body, DateTime dueDate)
        {
            if (kanban.EditTask(taskId, title, body, dueDate))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("task was Successfully updated");
                Console.ResetColor();
                PrintBoard();
                UserOption();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log.Warn("Task updating has failed, one of the task's inputs was invalid.");
                Console.WriteLine("please make sure you are following the instruction carefully.");
                Console.ResetColor();
                UserOption();
            }
        }

        public static void LogOut()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Bye Bye");
            Console.ResetColor();
            Log.Info("User logged out");
            Start();
        }

        public static void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Bye Bye");
            Console.ResetColor();
            System.Environment.Exit(0);
        }
    }
}
