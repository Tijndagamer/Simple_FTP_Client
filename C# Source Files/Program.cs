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
                    GetVariablesForUploadFile(false);
                    break;
                case "upload file -silent":
                    GetVariablesForUploadFile(true);
                    break;
                case "download file":
                    GetVariablesForDownloadFile(false);
                    break;
                case "download file -silent":
                    GetVariablesForDownloadFile(true);
                    break;
                case "download all files in dir":
                    DownloadAllFilesInDir(false);
                    break;
                case "download all files in dir -silent":
                    DownloadAllFilesInDir(true);
                    break;
                case "get files in dir":
                    PrintAllFilesInDir(false);
                    break;
                case "get files in dir -silent":
                    PrintAllFilesInDir(true);
                    break;
                case "upload folder":
                    UploadLocalFolder(false);
                    break;
                case "upload folder -silent":
                    UploadLocalFolder(true);
                    break;
                case "help":
                    help();
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

        private static void help()
        {
            Console.WriteLine("The available commands are: \n Upload File \n Download File \n Get files in dir \n Download all files in dir \n Help");
        }

        /// <summary>
        /// Gets the variables for the UploadFile method and then calls it.
        /// </summary>
        private static void GetVariablesForUploadFile(bool Silent)
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
            FTP.UploadFile(FTPAddress, FilePath, Username, Password, Silent);
        }

        /// <summary>
        /// Gets the variables for the DownloadFile method and then calls it.
        /// </summary>
        private static void GetVariablesForDownloadFile(bool Silent)
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
            FTP.DownloadFile(FTPAddress + "/" + FTPFile, Username, Password, LocalFilename, Silent); 
        }

        /// <summary>
        /// Get the variables to call GetFilesInDir and then print the returned values
        /// </summary>
        private static void PrintAllFilesInDir(bool Silent)
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
            string[] FilesInDir = FTP.GetFilesInDir(FTPAddress + "/" + FolderOnServer, Username, Password, Silent);

            // Print out the contents of the FilesInDir array
            foreach(string file in FilesInDir)
            {
                Console.WriteLine(file);
            }
        }

        /// <summary>
        /// Downloads all the files in one dir on the FTP server
        /// </summary>
        /// <param name="Silent"></param>
        private static void DownloadAllFilesInDir(bool Silent)
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

            // Call the FilesInDir method to see which files are in the dir, and save them in a stringp[]
            string[] FilesInDir = FTP.GetFilesInDir(FTPAddress + "/" + FolderOnServer, Username, Password, Silent);

            // Create a new local dir if it doesn't exists already
            if (!(Directory.Exists("/" + FolderOnServer)))
            {
                Directory.CreateDirectory(FolderOnServer);
                Console.WriteLine("Created new dir...");
            }

            // Foreach file in the folder, download it
            foreach (string File in FilesInDir)
            {
                // Print which file is going to be uploaded
                Console.WriteLine("    File : " + File);

                // Make a FTP File address
                string FTPFileAddress = FTPAddress + "/" + File;

                // Download the file
                FTP.DownloadFile(FTPFileAddress, Username, Password, File, Silent);
            }
        }

        /// <summary>
        /// Uploads one local folder, including it's content to a FTP server
        /// </summary>
        /// <param name="Silent"></param>
        private static void UploadLocalFolder(bool Silent)
        {
            // First get the variables
            Console.Write("FTP Server Address = ");
            string FTPAddress = Console.ReadLine();

            Console.Write("Username = ");
            string Username = Console.ReadLine();

            Console.Write("Password = ");
            string Password = Console.ReadLine();

            Console.Write("Folder = ");
            string LocalFolder = Console.ReadLine();

            // Get the contents of the LocalFolder
            string[] FilesInLocalFolder = Directory.GetFiles(LocalFolder);

            // Foreach file in the local folder, upload it
            foreach (string File in FilesInLocalFolder)
            {
                // Print which file is going to be uploaded
                Console.WriteLine("    File : " + File);

                // Upload the file
                FTP.UploadFile(FTPAddress, File, Username, Password, Silent);
            }
        }
    }
}
