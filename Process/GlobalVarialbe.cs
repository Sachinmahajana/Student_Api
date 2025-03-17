using Student_Api.Model;

namespace Student_Api.Process
{
    public class GlobalVarialbe: IDisposable
    {
        public User CurrentUser { get; set; }
        public void Dispose() { GC.SuppressFinalize(this); }
    }
}
        