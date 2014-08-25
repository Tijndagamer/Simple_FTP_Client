using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Simple_FTP_Client
{
    class FTP
    {
        /// <summary>
        /// Uploads a file to a FTP server
        /// </summary>
        /// <param name="FTPAddress">The address of the FTP server</param>
        /// <param name="filePath">The path to the file that you want to upload</param>
        /// <param name="username">The username used to login</param>
        /// <param name="password">The password</param>
        public static void UploadFile(string FTPAddress, string Filepath, string Username, string Password, bool Silent)
        {
            try
            {
                // Create FTP request
                if (Silent == false)
                {
                    Console.WriteLine("    Create a new request and setting up the properties...");
                }
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(Filepath));

                // Set the properties
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(Username, Password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                // Load the file
                if (Silent == false)
                {
                    Console.WriteLine("    Loading the file...");
                }
                FileStream stream = File.OpenRead(Filepath);
                byte[] buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                // Upload file
                if (Silent == false)
                {
                    Console.WriteLine("    Uploading the file...");
                }
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

        /// <summary>
        /// Download a file from a FTP server
        /// </summary>
        /// <param name="FTPFileAddress">The address to the file.</param>
        /// <param name="Username">The username</param>
        /// <param name="Password">The password</param>
        /// <param name="LocalFileName">The local filename</param>
        public static void DownloadFile(string FTPFileAddress, string Username, string Password, string LocalFileName, bool Silent)
        {
            try
            {
                // Create a new instance of the WebClient
                WebClient request = new WebClient();

                // Setup the credentials
                if (Silent == false)
                {
                    Console.WriteLine("    Setting up the credentials...");
                }
                request.Credentials = new NetworkCredential(Username, Password);

                // Download the data in a byte array
                if (Silent == false)
                {
                    Console.WriteLine("    Downloading the raw data...");
                }
                byte[] FileData = request.DownloadData(FTPFileAddress);

                // Create a FileStream so we can store the downloaded data
                FileStream file = File.Create(LocalFileName);

                // Write the raw downloaded data to the file.
                if (Silent == false)
                {
                    Console.WriteLine("    Writing the raw data to a file...");
                }
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
        public static string[] GetFilesInDir(string FTPFolderAddress, string Username, string Password, bool Silent)
        {
            try
            {
                // Create the StringBuilder result to store the results in
                StringBuilder result = new StringBuilder();

                // Create a new instance of the FtpWebRequest class
                if (Silent == false)
                {
                    Console.WriteLine("    Create a new request and setting up the properties...");
                }
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPFolderAddress);

                // Set the properties
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(Username, Password);
                request.UseBinary = true;
                request.UsePassive = false;
                request.KeepAlive = false;

                // Get the response from the FTP server
                if (Silent == false)
                {
                    Console.WriteLine("    Get the Response...");
                }
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
            catch (Exception e)
            {
                // Print the error
                Console.WriteLine("Error! \n" + e.ToString());

                // Stop the method.
                string[] error = new string[10];

                // Return nothing.
                return error;
            }
        }
    }
}
