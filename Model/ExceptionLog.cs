namespace Student_Api.Model
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Source { get; set; }
        public string InnerException { get; set; }
        public string FilePath { get; set; }
        public int LineNumber { get; set; }
        public string MethodName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set;}
    }
}
