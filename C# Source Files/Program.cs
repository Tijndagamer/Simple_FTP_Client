using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_FTP_Client
{
    class Program
    {
        // Global variables
        public static string username, password, ftpServer;
        public static bool Logging = false;
        public static List<string> LogList = new List<string>();
        public static FTP ftp;

        static void Main(string[] args)
        {
            // Welcome the user.
            Console.WriteLine("Welcome to Simple FTP Client.");
            Console.WriteLine("Type help for a list of commands.");

            UserInteraction.GetStartVariables();

            ftp = new FTP(username, password, ftpServer);

            while (true)
            {
                CMD();
            }
        }

        static void CMD()
        {
            // Declare variable
            string cmd;

            // Get it.
            Console.Write(username + "@" + ftpServer + " $");
            cmd = Console.ReadLine();

            // Log the command if logging is enabled
            Check(true, cmd);

            switch(cmd.ToLower())
            {
                // FTP commands
                case "upload file":
                    UserInteraction.GetVariablesForUploadFile(false);
                    break;
                case "upload file -silent":
                    UserInteraction.GetVariablesForUploadFile(true);
                    break;
                case "download file":
                    UserInteraction.GetVariablesForDownloadFile(false);
                    break;
                case "download file -silent":
                    UserInteraction.GetVariablesForDownloadFile(true);
                    break;
                case "download all files in dir":
                    UserInteraction.DownloadAllFilesInDir(false);
                    break;
                case "download all files in dir -silent":
                    UserInteraction.DownloadAllFilesInDir(true);
                    break;
                case "get files in dir":
                    UserInteraction.PrintAllFilesInDir(false);
                    break;
                case "get files in dir -silent":
                    UserInteraction.PrintAllFilesInDir(true);
                    break;
                case "upload folder":
                    UserInteraction.UploadLocalFolder(false);
                    break;
                case "upload folder -silent":
                    UserInteraction.UploadLocalFolder(true);
                    break;

                // Credentials commands
                case "save credentials":
                    CredentialsHandling.SaveCredentials();
                    break;
                case "load credentials":
                    CredentialsHandling.LoadCredentials();
                    break;

                // Logging commands
                case "enable logging":
                    Logging = true;
                    Console.WriteLine("Logging is now enabled.");
                    break;
                case "disable logging":
                    Logging = false;
                    Console.WriteLine("Logging is now disabled");
                    break;
                case "write log to file":
                    Log.WriteLogToFile();
                    break;

                // Main Commands
                case "help":
                    UserInteraction.help();
                    break;
                case "exit":
                    Log.WriteLogToFile();
                    System.Threading.Thread.Sleep(500);
                    Environment.Exit(0);
                    break;
                case "quit":
                    Log.WriteLogToFile();
                    System.Threading.Thread.Sleep(500);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Woops, command not found.");
                    break;
            }
        }

        /// <summary>
        /// The if/else statement for logging/output, to reduce the amount of copy code and lines
        /// </summary>
        /// <param name="Silent"></param>
        /// <param name="message"></param>
        public static void Check(bool silent, string message)
        {
            if (silent == false)
            {
                Console.WriteLine(message);
            }
            if (Program.Logging == true)
            {
                Log.LogOneLine(message);
            }
        }
    }
}
