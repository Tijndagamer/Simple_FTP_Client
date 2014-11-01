using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Simple_FTP_Client
{
    class Log
    {
        /// <summary>
        /// Logs something to the Simple_FTP_Client.log file like this:
        /// dd-MM-yyyy HH:mm:ss LogMessage
        /// </summary>
        /// <param name="Log">The action to be logged</param>
        public static void LogOneLine(string LogMessage)
        {
            // Create the timestamp
            string Timestamp = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");

            // Create the full message
            string WriteMessage = Timestamp + LogMessage;

            // Add to the LogList
            Program.LogList.Add(WriteMessage);
        }

        public static void WriteLogToFile()
        {
            // <add a comment here because I want a comment for every line but I don't know a good comment for this line>
            Console.WriteLine("Writing the log to the log file...");
            
            // Create a filestream to the log
            FileStream fileStream = File.OpenWrite("Simple_FTP_Client.log");

            // Create a StreamWriter to write to the file
            StreamWriter writer = new StreamWriter(fileStream);

            for (int i = Program.LogList.Count; i > 0; i-- )
            {
                // Tell the user the progress
                Console.Write("Progress: ");
                Console.Write(i / Program.LogList.Count);
                Console.Write("\n");

                // Write the line to the file
                writer.WriteLine(Program.LogList[i - 1]);
            }
            // Close the Writer
            writer.Close();

            Program.LogList = null;
        }
    }
}
