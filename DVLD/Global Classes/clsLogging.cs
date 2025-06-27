using System;

using System.Diagnostics;

namespace DVLD.Global_Classes
{

    /// <summary>
    /// this class will make logging to the Event Viwer
    /// 
    /// </summary>
    public class clsLogging
    {
       public  enum enLevel { Information=1,Error=2,Warning=3};

       private const string _SourseName= "DVLD";
        /// <summary>
        /// In which WindowsLog Catgory want save it the call it LogName
        /// </summary>
        private const string _WindowsLog= "Application";

       private static EventLogEntryType _EventLogEntryType;






        /// <summary>
        /// Ensures that the event source exists in the Windows Event Log.
        /// </summary>
        private static void _EnsureEventLogSourceExists()
        {
            if (!EventLog.SourceExists(_SourseName))
            {
                EventLog.CreateEventSource(_SourseName, _WindowsLog);

            }

        }

        /// <summary>
        /// Adds a new log entry to the Windows Event Log.
        /// </summary>
        /// <param name="locationHappened">Location where the event occurred.</param>
        /// <param name="message">Message to log.</param>
        /// <param name="level">Log level (Information, Error, Warning).</param>
        public static  void  AddNewLogging(string LocationHappened, string Message, enLevel Level)
        {
            _EnsureEventLogSourceExists();          

            _GetEventLogEntryType(Level);

            string logEntry = string.Concat("Location Happened: ", LocationHappened, " Message: ", Message);
            EventLog.WriteEntry(_SourseName, logEntry, _EventLogEntryType);
              


        }

        private static void _GetEventLogEntryType(enLevel Level)
        {
            switch(Level)
            {
                case enLevel.Information:
                    _EventLogEntryType = EventLogEntryType.Information;
                    break;

                case enLevel.Error:
                    _EventLogEntryType = EventLogEntryType.Error;
                    break;

                case enLevel.Warning:
                    _EventLogEntryType = EventLogEntryType.Warning;
                    break;
            }
        }

    }
}
