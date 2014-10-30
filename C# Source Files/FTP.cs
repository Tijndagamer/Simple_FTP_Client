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
    /// The class with the methods which handle all the FTP related things.
    /// </summary>
    class FTP
    {
        private string username, password, ftpServer;

        public string FTPServer
        {
            get
            {
                return ftpServer;
            }
            private set
            {
                ftpServer = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            private set
            {
                username = value;
            }
        }

        public FTP(string username, string password, string ftpServer)
        {
            this.username = username;
            this.password = password;
            this.ftpServer = ftpServer;
        }

        /// <summary>
        /// Uploads a file to a FTP server
        /// </summary>
        /// <param name="FTPAddress">The address of the FTP server</param>
        /// <param name="filePath">The path to the file that you want to upload</param>
        /// <param name="username">The username used to login</param>
        /// <param name="password">The password</param>
        public void UploadFile(string filePath, bool silent)
        {
            try
            {
                // Create FTP request
                Program.Check(silent, "    Create a new request and setting up the properties...");
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(ftpServer + "/" + Path.GetFileName(filePath));

                // Set the properties
                request.Method = WebRequestMethods.Ftp.UploadFile;
                Program.Check(silent, "    Logging in using username: " + username);
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                // Load the file
                Program.Check(silent, "    Loading the file...");
                FileStream stream = File.OpenRead(filePath);
                byte[] buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                // Upload file
                Program.Check(silent, "    Uploading the file...");
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
        /// <param name="ftpFileAddress">The address to the file.</param>
        /// <param name="Username">The username</param>
        /// <param name="Password">The password</param>
        /// <param name="localFileName">The local filename</param>
        public void DownloadFile(string ftpFileAddress, string localFileName, bool silent)
        {
            try
            {
                // Create a new instance of the WebClient
                WebClient request = new WebClient();

                // Setup the credentials
                Program.Check(silent, "    Logging in using username: " + username);
                request.Credentials = new NetworkCredential(Username, password);

                // Download the data in a byte array
                Program.Check(silent, "    Downloading the raw data...");
                byte[] FileData = request.DownloadData(ftpFileAddress);

                // Create a FileStream so we can store the downloaded data
                FileStream file = File.Create(localFileName);

                // Write the raw downloaded data to the file.
                Program.Check(silent, "    Writing the raw data to a file...");
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
        /// <param name="ftpFolderAddress">The folder on the FTP address, e.g.: ftp://127.0.0.1/testfolder </param>
        /// <param name="Username">The username</param>
        /// <param name="Password">The password</param>
        /// <returns>The files in the directory</returns>
        public string[] GetFilesInDir(string ftpFolderAddress, bool silent)
        {
            try
            {
                // Create the StringBuilder result to store the results in
                StringBuilder result = new StringBuilder();

                // Create a new instance of the FtpWebRequest class
                Program.Check(silent, "    Create a new request and setting up the properties...");
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(ftpFolderAddress);

                // Set the properties
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                Program.Check(silent, "    Logging in using username: " + username);
                request.Credentials = new NetworkCredential(username, password);
                request.UseBinary = true;
                request.UsePassive = false;
                request.KeepAlive = false;

                // Get the response from the FTP server
                Program.Check(silent, "    Get the Response...");
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
