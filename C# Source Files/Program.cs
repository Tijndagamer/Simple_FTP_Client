using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Simple_FTP_Client
{
    class Program
    {

        // Main and etc methods.

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



        // Credentials saving & loading methods [WIP]

        private static void SaveCredentials()
        {
            // Get the credentials
            Console.Write("Username = ");
            string Username = Console.ReadLine();

            Console.Write("Password = ");
            string Password = Console.ReadLine();

            // Create a new FileStream
            FileStream FileStream = File.OpenWrite("Credentials");

            // Create a new StreamWriter
            StreamWriter writer = new StreamWriter(FileStream);

            // Write the credentials to a file
            writer.Write(Username);
            writer.Write(Password);

            // Close the streamwriter
            writer.Close();
        }
    }
}
