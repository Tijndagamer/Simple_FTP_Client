using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Simple_FTP_Client
{
    /// <summary>
    /// The class with the methods which handle all the interactions with the user
    /// </summary>
    class UserInteraction
    {
        // User interaction methods

        /// <summary>
        /// Prints all the available commands
        /// </summary>
        public static void Help()
        {
            Console.WriteLine("The available commands are: \n Upload File \n Download File \n Get files in dir \n Download all files in dir \n Enable logging \n Disable logging \n Write log to file \n Help");
        }

        public static void Exit()
        {
            if (Program.Logging == true)
            {
                // Write the log to the file
                Log.WriteLogToFile();

                // Wait for 500 miliseconds, I'm not sure why but the code doesn't work properly without it.
                System.Threading.Thread.Sleep(500);
            }

            // Actually exit the program
            Environment.Exit(0);
        }

        /// <summary>
        /// Gets the variables needed from the user to create a FTP object
        /// </summary>
        public static void GetStartVariables()
        {
            if (File.Exists("Credentials"))
            {
                CredentialsHandling.LoadCredentials();
            }
            else
            {
                Console.Write("Username = ");
                string username = Console.ReadLine();

                Console.Write("Password = ");
                string password = Console.ReadLine();

                Program.username = username;
                Program.password = password;
            }

            Console.Write("FTP Server = ");
            string ftpServer = Console.ReadLine();

            Program.ftpServer = ftpServer;
        }

        /// <summary>
        /// Gets the variables for the UploadFile method and then calls it.
        /// </summary>
        public static void GetVariablesForUploadFile(bool silent)
        {
            // First get the variable
            Console.Write("File = ");
            string filePath = Console.ReadLine();

            // Call the UploadFile method
            Program.ftp.UploadFile(filePath, silent);
        }

        /// <summary>
        /// Gets the variables for the DownloadFile method and then calls it.
        /// </summary>
        public static void GetVariablesForDownloadFile(bool silent)
        {
            // First get the variables
            Console.Write("Filename on FTP Server = ");
            string ftpServerFileName = Console.ReadLine();

            Console.Write("Local Filename = ");
            string localFileName = Console.ReadLine();

            // Call the DownloadFile method
            Program.ftp.DownloadFile(Program.ftpServer + "/" + ftpServerFileName, localFileName, silent);
        }

        /// <summary>
        /// Get the variables to call GetFilesInDir and then print the returned values
        /// </summary>
        public static void PrintAllFilesInDir(bool silent)
        {
            // First get the variable directoryOnServer
            Console.Write("Folder on server = ");
            string directoryOnServer = Console.ReadLine();

            // Call the method and store the return variables
            string[] FilesInDir = Program.ftp.GetFilesInDir(Program.ftpServer + "/" + directoryOnServer, silent);

            // Print out the contents of the FilesInDir array
            foreach (string file in FilesInDir)
            {
                Console.WriteLine(file);
            }
        }

        /// <summary>
        /// Downloads all the files in one dir on the FTP server
        /// </summary>
        /// <param name="silent"></param>
        public static void DownloadAllFilesInDir(bool silent)
        {
            // First get the variables
            Console.Write("Folder on server = ");
            string directoryOnServer = Console.ReadLine();

            // Call the FilesInDir method to see which files are in the dir, and save them in a stringp[]
            string[] filesInDir = Program.ftp.GetFilesInDir(Program.ftpServer + "/" + directoryOnServer, silent);

            // Create a new local dir if it doesn't exists already
            if (!(Directory.Exists("/" + directoryOnServer)))
            {
                Directory.CreateDirectory(directoryOnServer);
                Console.WriteLine("Created new dir...");
            }

            // Foreach file in the folder, download it
            foreach (string file in filesInDir)
            {
                // Print which file is going to be uploaded
                Console.WriteLine("    File : " + file);

                // Make a FTP File address
                string ftpFileAddress = Program.ftpServer + "/" + file;

                // Download the file
                Program.ftp.DownloadFile(ftpFileAddress, file, silent);
            }
        }

        /// <summary>
        /// Uploads one local folder, including it's content to a FTP server
        /// </summary>
        /// <param name="silent"></param>
        public static void UploadLocalFolder(bool silent)
        {
            // First get the variable localFolder
            Console.Write("Folder = ");
            string localFolder = Console.ReadLine();

            // Get the contents of the LocalFolder
            string[] FilesInLocalFolder = Directory.GetFiles(localFolder);

            // Foreach file in the local folder, upload it
            foreach (string File in FilesInLocalFolder)
            {
                // Print which file is going to be uploaded
                Console.WriteLine("    File : " + File);

                // Upload the file
                Program.ftp.UploadFile(File, silent);
            }
        }
    }
}
