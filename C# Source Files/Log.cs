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

            // Create a filestream to the log
            FileStream FileStream = File.OpenWrite("Simple_FTP_Client.log");

            // Create a StreamWriter to write to the file
            StreamWriter Writer = new StreamWriter(FileStream);

            // Write the LogMessage to the file
            Writer.WriteLine(WriteMessage);

            // Close the file stream
            Writer.Close();
        }
    }
}
