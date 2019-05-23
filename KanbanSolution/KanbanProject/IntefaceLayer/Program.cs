using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using KanbanProject;

namespace KanbanProject
{
    class Program
    {
        private static KanbanBoard kanban;
        private static bool isLogged = false;


        static void Main(string[] args)
        {
            kanban = new KanbanBoard();
            kanban.UploadData();
            Start();
        }

        public static void Start()
        {
            //UploadData();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hello and welcome to Kanban");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Please choose one of the following options:");
            Console.ResetColor();
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("NOTE : In order to return to home menu, press '- 1'.");
            Console.ResetColor();
            string choiseStart = Console.ReadLine();
            ChooseStart(choiseStart); // call's to the function that sort the users' choise.
        } // צריך לאתחל את קנבן ולטעון נתונים

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
                    case "2": // in case that the user wants to login
                        isLogged = false;
                        Login();
                        break;
                    case "3": // in case that the user wants to exit
                        Exit();
                        break;
                    default: // in all other case
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter a valid choise.");
                            Console.ResetColor();
                            //Log.Instance.warn("Invalid input - Invalid menu choice");//log 
                            choiseStart = Console.ReadLine();
                            ChooseStart(choiseStart);
                        }
                        break;
                }
            }
            else if (choiseStart.Equals("-1")) // if the user wants to go to the first menu
                Start();
            else // if the length of the option that the user entered is not correct
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid choise.");
                Console.ResetColor();
                // Log.Instance.warn("Invalid input - Invalid menu choice");//log 
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
            Console.WriteLine("NOTE : In order to return to home menu, press '-1' at any time.");
            Console.WriteLine("Please enter a valid email address, that hasn't been used in this system.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("EmailAddress :");
            string email = Console.ReadLine();
            Console.ResetColor();

            email = CheckEmail(email); //check if the email is valid and if he is new in the system
            if (email != "-1")
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("Please enter a password.");
                Console.WriteLine("Your password must be in length of 4 to 20 characters");
                Console.WriteLine("and must include at least one capital character, one small character and a number.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Password :");
                string password = Console.ReadLine();
                Console.ResetColor();
                password = CheckPassword(password);
                if (password != "-1")// the user doesn't want to go to the menu.
                {

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine("In this system you can limit the number of tasks in each column");
                    Console.WriteLine("please choose number, and this number will limit your task's quantity in each column  ");
                    Console.ResetColor();
                    string maxLength = Console.ReadLine();
                    int validLength = ValidNumber(maxLength);
                    while (validLength == -1)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("inValid number!");
                        Console.WriteLine("please enter a valid number");
                        Console.ResetColor();
                        maxLength = Console.ReadLine();
                        validLength = ValidNumber(maxLength);
                    }
                    kanban.Register(email, password, validLength);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations! Registered successfully!");
                    Console.ResetColor();
                    //Log.Instance.info("New registration - User: " + NickName);//log
                }
            }
            else //if the user wants to go back to menu
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Registretion failed");
                Console.ResetColor();
                //Log.Instance.error("Action fail - User not logged in");//log
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
                    Console.WriteLine("Invalid Email - please enter a valid email address and make sure that it hasn't been used in this system.");
                    Console.ResetColor();
                    //  Log.Instance.warn("Invalid input - Invalid Email");//log 
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("EmailAddress :");
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
                    Console.WriteLine("Invalid Password - please enter a valid password and make sure its on the right length and include all the characters you were requested !");
                    Console.ResetColor();
                    //Log.Instance.warn("Invalid input - Invalid Password");//log 
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("Password :");
                    password = Console.ReadLine();
                    return CheckPassword(password);
                }
            }
        }

        private static int ValidNumber(string maxLength)//checks if the string contains a valid number
        {
            for (int i = 0; i < maxLength.Count(); i = i + 1)
            {
                if (maxLength[i] > '9' | maxLength[i] < '0')
                    return -1;
            }
            int counter = 1;
            int output = 0;
            for (int j = maxLength.Count() - 1; j >= 0; j = j - 1)
            {
                output = (maxLength[j] - '0') * counter + output;
                output = output * 10;
            }
            return output;
        }


        public static void Login()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Hi kanban's user ! please enter your Email and password");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("NOTE : In order to return to the menu, press '-1' ");
            Console.ResetColor();
            Console.Write("Email : ");
            string email = Console.ReadLine();
            Console.Write("Password :");
            string password = Console.ReadLine();
            if (email != "-1" & password != "-1")
            {
                if (kanban.LogIn(email, password))//password and email are in the system;
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Logeedin successfully! ");
                    Console.ResetColor();
                    isLogged = true;

                    PrintBoard();
                    // מה קורה כשמתחברים??
                    UserOption();
                }
                else // if the email and password are incorrect
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Login failed!! your email or password is incorrect!");
                    Console.ResetColor();
                    Login();
                }
            }
            else // the user wants to go back to menu
                Start();

        } // צריך לאתחל את השדות יוזר ובורד בקנבןבורד

        private static void UserOption() { //user's option in his account

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Choose only one option :");
            Console.ResetColor();
            Console.WriteLine("1. AddTask ");
            Console.WriteLine("2. MoveTask");
            Console.WriteLine("3. RemoveTask");
            Console.WriteLine("4. LogOut");
            Console.WriteLine("5. Exit");
            
            string userchoise = Console.ReadLine();
            ChooseOption(userchoise); // call's to the function that sort the user's choise.
        }

    
        public static void ChooseOption(string userchoise)// sort the user's choice
        {
            if (userchoise.Length == 1)
            {
                switch (userchoise)
                {
                    case "1" :// in case the user wants to add task to his board;
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("in order to add a task please enter the following detail according to the Instructions");
                        Console.WriteLine("the number of tasks in each column is limited, make sure you have space left befor trying to add a task");
                        Console.WriteLine("your limit is: " + kanban.GetMaxLength()); // צריך שיטה כזאתת
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("task title - contain max 50 characters,  and not empty!  ");
                        Console.ResetColor();
                        Console.Write("title :");
                        string title = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("task body - can contain 0-300 characters  ");
                        Console.ResetColor();
                        Console.Write("body :");
                        string body = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("task due date - must be a date that hasn't passed yet  ");
                        Console.ResetColor();
                        Console.Write("Enter due date (e.g. 10/22/1987) : ");
                        DateTime dueDate = DateTime.Parse(Console.ReadLine());//להמשיך את מה שדורין המלכה כתבה
                        AddTask(title, body, dueDate);
                        break;
                    case "2" ://in case the user wants to further a task
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("if you start working on a task or if you finish one");
                        Console.WriteLine("you can move your task one column forward");
                        Console.WriteLine("from 'to do' to 'in progress' and from 'in progress' to 'done' ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("note: you can't move a task that located in the done column!");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("please enter the following task's details");
                        Console.ResetColor();
                        Console.Write("task Id :");
                        string taskId = Console.ReadLine();
                        int task = ValidNumber(taskId);
                        if (task == -1) //if the inputs invalid
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("invalid input! make sure you enter a valid task number");
                            Console.ResetColor();
                        }
                        else
                            MoveTask(task);
                        break;

                    case "3" ://in case the user wants to remove task
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("please enter the following task's detail,in order to remove it");
                        Console.ResetColor();
                        Console.Write("task Id :");
                        taskId = Console.ReadLine();
                        task = ValidNumber(taskId);
                        if (task == -1) //if the inputs invalid
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("invalid input! make sure you enter a valid task number");
                            Console.ResetColor();
                        }
                        else
                            RemoveTask(task);
                        break;
                    case "4" ://log out
                        LogOut();
                        break;

                    case "5" : // exit the system
                        Exit();
                        break;

                    default: //the user enter incorrect option
                        Console.WriteLine("your input is invalid! please type only one of the options");
                        UserOption();
                        break;

                }
            }
            else //the user enter incorrect option
            {
                Console.WriteLine("your input is invalid! please type only one of the options");
                UserOption();
                //same as default
            }
        }

        private static void PrintBoard()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(kanban.PrintBoard());
            Console.ResetColor();



        }

        public static void AddTask(string title, string body, DateTime dueDate)
        {
            if (!kanban.AddTask(title, body, dueDate)) // At list one of the inputs is incorrect
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("task adding has failed please make sure you are following the instruction carefully");
                UserOption();
                //ERROR "Please make sure ... title, due date, num of tasks"
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("task has been added!");
                PrintBoard();
                UserOption();
            }

                
        }

        public static void MoveTask(int task)
        {
            if (kanban.MoveTask(task))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("task was Moved");
                Console.ResetColor();
                PrintBoard();
                UserOption();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("task moving failed, at least one of your column is full or the task Id does not exist");
                Console.ResetColor();
                UserOption();
            }
        }

        public static void RemoveTask(int task)
        {
            if (kanban.RemoveTask(task))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("task was remove");
                Console.ResetColor();
                PrintBoard();
                UserOption();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("task removing failed, the task Id does not exist");
                Console.ResetColor();
                UserOption();
            }
            
        }

        public static void LogOut() // לאפס את השדות בקנבן
        {
            isLogged = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("bye bye");
            Console.ResetColor();
            Start();
        }


        public static void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Bye bye");
            Console.ResetColor();
            isLogged = false;
            System.Environment.Exit(0);
        }

    }

}

