using System;
using System.IO;

namespace DAN_XLV_Kristina_Garcia_Francisco.Model
{
    /// <summary>
    /// Logs actions to a file
    /// </summary>
    class Logger
    {
        private readonly string managerLogger = "managerLog.txt";

        /// <summary>
        /// Writes the message to the log file.
        /// </summary>
        /// <param name="message">Message that will be written to the file</param>
        public void WriteToFile(string message)
        {
            // Save all the routes to file
            using (StreamWriter streamWriter = new StreamWriter(managerLogger, append: true))
            {
                string logMessage = "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "] " + message;
                streamWriter.WriteLine(logMessage.ToString());
            }
        }
    }
}
