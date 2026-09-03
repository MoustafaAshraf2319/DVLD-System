using System;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public static class clsEventLog
    {
        private static string sourceName = "DVLD";

        public static void LogException(Exception ex)
        {
            try
            {
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                string ErrorMessage =
                   $"Exception Message: {ex.Message}\n" +
                   $"Stack Trace: {ex.StackTrace}\n" +
                   $"Target Site: {ex.TargetSite}";

                EventLog.WriteEntry(sourceName, ErrorMessage, EventLogEntryType.Error);
            }
            catch
            {
            }
        }
    }
}
