using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Simple_FTP_Client
{
    /// <summary>
    /// The class with the methods which handle all the interactions with the user
    /// </summary>
    class UserInteraction
    {
        /// <summary>
        /// Prints all the available commands
        /// </summary>
        public static void help()
        {
            Console.WriteLine("The available commands are: \n Upload File \n Download File \n Get files in dir \n Download all files in dir \n Help");
        }

        // User interaction methods

        /// <summary>
        /// Gets the variables for the UploadFile method and then calls it.
        /// </summary>
        public static void GetVariablesForUploadFile(bool Silent)
        {
            // Get the variables
            Console.Write("FTP Address = ");
            string FTPAddress = Console.ReadLine();

            if (string.IsNullOrEmpty(Program.Username) == true)
            {
                Console.Write("Username = ");
                Program.Username = Console.ReadLine();

                Console.Write("Password = ");
                Program.Password = Console.ReadLine();
            }

            Console.Write("File = ");
            string FilePath = Console.ReadLine();

            // Call the UploadFile method
            FTP.UploadFile(FTPAddress, FilePath, Program.Username, Program.Password, Silent);
        }

        /// <summary>
        /// Gets the variables for the DownloadFile method and then calls it.
        /// </summary>
        public static void GetVariablesForDownloadFile(bool Silent)
        {
            // Get the variables
            Console.Write("FTP Server Address = ");
            string FTPAddress = Console.ReadLine();

            if (string.IsNullOrEmpty(Program.Username) == true)
            {
                Console.Write("Username = ");
                Program.Username = Console.ReadLine();

                Console.Write("Password = ");
                Program.Password = Console.ReadLine();
            }

            Console.Write("Filename on FTP Server = ");
            string FTPFile = Console.ReadLine();

            Console.Write("Local Filename = ");
            string LocalFilename = Console.ReadLine();

            // Call the DownloadFile method
            FTP.DownloadFile(FTPAddress + "/" + FTPFile, Program.Username, Program.Password, LocalFilename, Silent);
        }

        /// <summary>
        /// Get the variables to call GetFilesInDir and then print the returned values
        /// </summary>
        public static void PrintAllFilesInDir(bool Silent)
        {
            // First get the variables
            Console.Write("FTP Server Address = ");
            string FTPAddress = Console.ReadLine();

            if (string.IsNullOrEmpty(Program.Username) == true)
            {
                Console.Write("Username = ");
                Program.Username = Console.ReadLine();

                Console.Write("Password = ");
                Program.Password = Console.ReadLine();
            }

            Console.Write("Folder on server = ");
            string FolderOnServer = Console.ReadLine();

            // Call the method and store the return variables
            string[] FilesInDir = FTP.GetFilesInDir(FTPAddress + "/" + FolderOnServer, Program.Username, Program.Password, Silent);

            // Print out the contents of the FilesInDir array
            foreach (string file in FilesInDir)
            {
                Console.WriteLine(file);
            }
        }

        /// <summary>
        /// Downloads all the files in one dir on the FTP server
        /// </summary>
        /// <param name="Silent"></param>
        public static void DownloadAllFilesInDir(bool Silent)
        {
            // First get the variables
            Console.Write("FTP Server Address = ");
            string FTPAddress = Console.ReadLine();

            if (string.IsNullOrEmpty(Program.Username) == true)
            {
                Console.Write("Username = ");
                Program.Username = Console.ReadLine();

                Console.Write("Password = ");
                Program.Password = Console.ReadLine();
            }

            Console.Write("Folder on server = ");
            string FolderOnServer = Console.ReadLine();

            // Call the FilesInDir method to see which files are in the dir, and save them in a stringp[]
            string[] FilesInDir = FTP.GetFilesInDir(FTPAddress + "/" + FolderOnServer, Program.Username, Program.Password, Silent);

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
                FTP.DownloadFile(FTPFileAddress, Program.Username, Program.Password, File, Silent);
            }
        }

        /// <summary>
        /// Uploads one local folder, including it's content to a FTP server
        /// </summary>
        /// <param name="Silent"></param>
        public static void UploadLocalFolder(bool Silent)
        {
            // First get the variables
            Console.Write("FTP Server Address = ");
            string FTPAddress = Console.ReadLine();

            if (string.IsNullOrEmpty(Program.Username) == true)
            {
                Console.Write("Username = ");
                Program.Username = Console.ReadLine();

                Console.Write("Password = ");
                Program.Password = Console.ReadLine();
            }

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
                FTP.UploadFile(FTPAddress, File, Program.Username, Program.Password, Silent);
            }
        }
    }
}
