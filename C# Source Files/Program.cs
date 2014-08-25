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
            UploadFile(FTPAddress, FilePath, Username, Password);
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
            DownloadFile(FTPAddress + "/" + FTPFile, Username, Password, LocalFilename); 
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
            string[] FilesInDir = GetFilesInDir(FTPAddress + "/" + FolderOnServer, Username, Password);

            // Print out the contents of the FilesInDir array
            foreach(string file in FilesInDir)
            {
                Console.WriteLine(file);
            }
        }

        // FTP methods.

        /// <summary>
        /// Uploads a file 
        /// </summary>
        /// <param name="FTPAddress">The address of the FTP server</param>
        /// <param name="filePath">The path to the file that you want to upload</param>
        /// <param name="username">The username used to login</param>
        /// <param name="password">The password</param>
        private static void UploadFile(string FTPAddress, string Filepath, string Username, string Password)
        {
            try
            {
                //Create FTP request
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(Filepath));

                // Set the properties
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(Username, Password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                //Load the file
                FileStream stream = File.OpenRead(Filepath);
                byte[] buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                //Upload file
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();

                Console.WriteLine("Upload completed without errors.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! \n" + e.ToString());
            }
        }

        private static void DownloadFile(string FTPFileAddress, string Username, string Password, string LocalFileName)
        {
            try
            {
                // Create a new instance of the WebClient
                WebClient request = new WebClient();

                // Setup the credentials
                request.Credentials = new NetworkCredential(Username, Password);

                // Download the data in a byte array
                byte[] FileData = request.DownloadData(FTPFileAddress);

                // Create a FileStream so we can store the downloaded data
                FileStream file = File.Create(LocalFileName);

                // Write the raw downloaded data to the file.
                file.Write(FileData, 0, FileData.Length);

                // Close the FileStream
                file.Close();

                Console.WriteLine("Download completed.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! \n" + e.ToString());
            }
        }

        /// <summary>
        /// Returns all the files in one directory
        /// </summary>
        /// <param name="FTPFolderAddress">The folder on the FTP address, e.g.: ftp://127.0.0.1/testfolder </param>
        /// <param name="Username">The username</param>
        /// <param name="Password">The password</param>
        /// <returns>The files in the directory</returns>
        private static string[] GetFilesInDir(string FTPFolderAddress, string Username, string Password)
        {
            // Create the StringBuilder result to store the results in
            StringBuilder result = new StringBuilder();

            // Create a new instance of the FtpWebRequest class
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPFolderAddress);

            // Set the properties
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(Username, Password);
            request.UseBinary = true;
            request.UsePassive = false;
            request.KeepAlive = false;

            // Get the response from the FTP server
            WebResponse response = request.GetResponse();

            // Create a new instance of the StreamReader class to read the response
            StreamReader reader = new StreamReader(response.GetResponseStream());

            // Read one line
            string line = reader.ReadLine();

            // While there's something in the line
            while (line != null)
            {
                // Append the line
                result.Append(line);

                // And an enter
                result.Append("\n");

                // And read the next line
                line = reader.ReadLine();
            }

            // Remove the last enter
            result.Remove(result.ToString().LastIndexOf('\n'), 1);
            
            // Return the result as a string[]
            return result.ToString().Split('\n');
        }
    }
}
