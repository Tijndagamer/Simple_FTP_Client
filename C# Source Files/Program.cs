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
            Console.Write("$ ");
            cmd = Console.ReadLine();

            switch(cmd.ToLower())
            {
                case "upload file":
                    GetVariablesForUploadFile();
                    break;
                case "download file":
                    GetVariablesForDownloadFile();
                    break;
                case "help":
                    help();
                    break;
                default:
                    Console.WriteLine("Woops, command not found.");
                    break;
            }
        }

        private static void help()
        {
            Console.WriteLine("The available commands are: \n upload file \n download file \n help");
        }

        /// <summary>
        /// Gets the variables for the UploadFile method and then calls it.
        /// </summary>
        private static void GetVariablesForUploadFile()
        {
            // Declare the variables
            string FTPAddress, FilePath, Username, Password;

            // Get them
            Console.Write("FTP address =");
            FTPAddress = Console.ReadLine();

            Console.Write("Username =");
            Username = Console.ReadLine();

            Console.Write("Password =");
            Password = Console.ReadLine();

            Console.Write("File =");
            FilePath = Console.ReadLine();

            // Call the UploadFile method
            UploadFile(FTPAddress, FilePath, Username, Password);
        }

        /// <summary>
        /// Gets the variables for the DownloadFile method and then calls it.
        /// </summary>
        private static void GetVariablesForDownloadFile()
        {
            // Get the variables
            Console.Write("FTP Server address =");
            string FTPAddress = Console.ReadLine();

            Console.Write("Username =");
            string Username = Console.ReadLine();

            Console.Write("Password =");
            string Password = Console.ReadLine();

            Console.Write("Filename on FTP server =");
            string FTPFile = Console.ReadLine();

            Console.Write("Local filename =");
            string LocalFilename = Console.ReadLine();

            // Call the DownloadFile method
            DownloadFile(FTPAddress + "/" + FTPFile, Username, Password, LocalFilename);
            
        }

        // FTP methods.

        /// <summary>
        /// Uploads a file 
        /// </summary>
        /// <param name="FTPAddress"></param>
        /// <param name="filePath"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private static void UploadFile(string FTPAddress, string filePath, string username, string password)
        {
            //Create FTP request
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(filePath));

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(username, password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(filePath);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();

            Console.WriteLine("Uploaded Successfully");
        }

        private static void DownloadFile(string FTPFileAddress, string Username, string Password, string LocalFileName)
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
    }
}
