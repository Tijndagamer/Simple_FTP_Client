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
            }
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
    }
}
