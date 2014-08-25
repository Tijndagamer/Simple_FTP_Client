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
                    GetVariablesForUploadFile();
                    break;
                case "download file":
                    GetVariablesForDownloadFile();
                    break;
                case "get files in dir":
                    PrintAllFilesInDirectory();
                    break;
                case "help":
                    help();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Woops, command not found.");
                    break;
            }
        }

        private static void help()
        {
            Console.WriteLine("The available commands are: \n Upload File \n Download File \n Get files in dir \n Help");
        }

        /// <summary>
        /// Gets the variables for the UploadFile method and then calls it.
        /// </summary>
        private static void GetVariablesForUploadFile()
        {
            // Get the variables
            Console.Write("FTP Address = ");
            string FTPAddress = Console.ReadLine();

            Console.Write("Username = ");
            string Username = Console.ReadLine();

            Console.Write("Password = ");
            string Password = Console.ReadLine();

            Console.Write("File = ");
            string FilePath = Console.ReadLine();

            // Call the UploadFile method
            FTP.UploadFile(FTPAddress, FilePath, Username, Password);
        }

        /// <summary>
        /// Gets the variables for the DownloadFile method and then calls it.
        /// </summary>
        private static void GetVariablesForDownloadFile()
        {
            // Get the variables
            Console.Write("FTP Server Address = ");
            string FTPAddress = Console.ReadLine();

            Console.Write("Username = ");
            string Username = Console.ReadLine();

            Console.Write("Password = ");
            string Password = Console.ReadLine();

            Console.Write("Filename on FTP Server = ");
            string FTPFile = Console.ReadLine();

            Console.Write("Local Filename = ");
            string LocalFilename = Console.ReadLine();

            // Call the DownloadFile method
            FTP.DownloadFile(FTPAddress + "/" + FTPFile, Username, Password, LocalFilename); 
        }

        /// <summary>
        /// Get the variables to call GetFilesInDir and then print the returned values
        /// </summary>
        private static void PrintAllFilesInDirectory()
        {
            // First get the variables
            Console.Write("FTP Server Address = ");
            string FTPAddress = Console.ReadLine();

            Console.Write("Username = ");
            string Username = Console.ReadLine();

            Console.Write("Password = ");
            string Password = Console.ReadLine();

            Console.Write("Folder on server = ");
            string FolderOnServer = Console.ReadLine();

            // Call the method and store the return variables
            string[] FilesInDir = FTP.GetFilesInDir(FTPAddress + "/" + FolderOnServer, Username, Password);

            // Print out the contents of the FilesInDir array
            foreach(string file in FilesInDir)
            {
                Console.WriteLine(file);
            }
        }
    }
}
