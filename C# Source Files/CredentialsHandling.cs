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
    /// This class handles the loading and saving of the user's credentials. [WIP]
    /// </summary>
    class CredentialsHandling
    {
        public static void SaveCredentials()
        {
            // Get the credentials
            Console.Write("Username = ");
            string Username = Console.ReadLine();

            Console.Write("Password = ");
            string Password = Console.ReadLine();

            // Create a new FileStream
            FileStream FileStream = File.OpenWrite("Credentials");

            // Create a new StreamWriter
            StreamWriter Writer = new StreamWriter(FileStream);

            // Write the credentials to a file
            Writer.Write(Username);
            Writer.Write(Password);

            // Close the streamwriter
            Writer.Close();
        }
        public static void LoadCredentials()
        {
            // Create a new FileStream
            FileStream FileStream = File.OpenRead("Credentials");

            // Create a new StreamReader instance to read the file
            StreamReader Reader = new StreamReader(FileStream);

            // Read the file and assign the variables
            Program.Username = Reader.ReadLine();
            Program.Password = Reader.ReadLine();

            // Close the reader
            Reader.Close();
        }
    }
}
