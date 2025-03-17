using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Student_Api.Data;
using Student_Api.Model;

namespace Student_Api.Process
{
    public class ExceptionLoggingService
    {
        private readonly DefaultDbContext db;
        public ExceptionLoggingService() { db = new DefaultDbContext(); }

        public void LogException(Exception ex, [CallerFilePath] string filepath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string methodName = "")
        {
            try
            {
                var innerExceptionMessage = GetInnerExceptionMessages(ex); // Capture all inner exception messages

                // Create a new ExceptionLog object to store in the database
                var exceptionLog = new ExceptionLog
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    TimeStamp = DateTime.UtcNow,
                    Source = ex.Source,
                    //  InnerException = innerExceptionMessage, // Save full inner exception chain
                    InnerException = string.IsNullOrEmpty(innerExceptionMessage) ? null : innerExceptionMessage, // Store null if no inner exception
                    FilePath = filepath,
                    LineNumber = lineNumber,
                    MethodName = methodName,
                   StartTime=DateTime.UtcNow,
                    EndTime=DateTime.UtcNow,
                };

                // Add the exception log to the database
                db.ExceptionLogs.Add(exceptionLog);
                db.SaveChanges(); // Save to database
            }
            catch (Exception loggingEx)
            {
                // Handle any exceptions that happen while logging the original exception
                // In real-world scenarios, you'd want to capture this as well, perhaps logging to a file if DB fails
                Console.WriteLine($"Logging failed: {loggingEx.Message}");
            }
        }
        // Filter logs by method name and date range (start and end time)
        public async Task<List<ExceptionLog>> FilterLogsAsync(string methodName, DateTime startDate, DateTime endDate)
        {
            var logs = await db.ExceptionLogs
                         .Where(log => log.MethodName == methodName && log.TimeStamp >= startDate && log.TimeStamp <= endDate)
                         .ToListAsync();
            return logs;
        }

        // Recursive method to get all inner exception messages
        private string GetInnerExceptionMessages(Exception ex)
        {
            var innerExceptionMessages = new StringBuilder();
            while (ex.InnerException != null)
            {
                innerExceptionMessages.AppendLine($"Inner Exception: {ex.InnerException.Message}");
                innerExceptionMessages.AppendLine($"StackTrace: {ex.InnerException.StackTrace}");
                ex = ex.InnerException;
            }
            return innerExceptionMessages.ToString();
        }

    }
}
