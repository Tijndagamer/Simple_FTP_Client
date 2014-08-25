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
            StreamWriter writer = new StreamWriter(FileStream);

            // Write the credentials to a file
            writer.Write(Username);
            writer.Write(Password);

            // Close the streamwriter
            writer.Close();
        }
    }
}
