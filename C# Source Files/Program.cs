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
        public static string Username, Password;

        static void Main(string[] args)
        {
            // Welcome the user.
            Console.WriteLine("Welcome to Simple FTP Client.");
            Console.WriteLine("Type help for a list of commands.");

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
            Console.Write("> ");
            cmd = Console.ReadLine();

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

                // Main Commands
                case "help":
                    UserInteraction.help();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                case "quit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Woops, command not found.");
                    break;
            }
        }
    }
}
