using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Simple_FTP_Client
{
    /// <summary>
    /// This class handles the loading and saving of the user's credentials. [WIP]
    /// </summary>
    class CredentialsHandling
    {
        public static void SaveCredentials()
        {
            if (string.IsNullOrEmpty(Program.username))
            {
                // Get the credentials
                Console.Write("Username = ");
                Program.username = Console.ReadLine();

                Console.Write("Password = ");
                Program.password = Console.ReadLine();
            }

            // Create a new FileStream
            FileStream FileStream = File.OpenWrite("Credentials");

            // Create a new StreamWriter
            StreamWriter Writer = new StreamWriter(FileStream);

            // Write the credentials to a file
            Writer.WriteLine(Program.username);
            Writer.WriteLine(Program.password);

            // Close the streamwriter
            Writer.Close();

            // Tell the user the credentials are saved
            Program.Check(false, "Saved credentials.");
        }
        public static void LoadCredentials()
        {
            // First check if the file exists
            if (File.Exists("Credentials"))
            {
                // Create a new FileStream
                FileStream FileStream = File.OpenRead("Credentials");

                // Create a new StreamReader instance to read the file
                StreamReader Reader = new StreamReader(FileStream);

                // Read the file and assign the variables
                Program.username = Reader.ReadLine();
                Program.password = Reader.ReadLine();

                // Close the reader
                Reader.Close();

                // Tell the user the credentials are now loaded
                Program.Check(false, "Loaded credentials.");
            }
            else
            {
                Console.WriteLine("Error! \n" + "the Credentials file doesn't exist");
            }
        }
    }
}
