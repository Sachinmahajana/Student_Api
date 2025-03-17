namespace Student_Api.Exceptions
{
    public class EmployeNotFoundException : Exception
    {
        public EmployeNotFoundException(int employeId)
            : base($"Employee with Id {employeId} not Found.")
        {

        }
        public class InValidEmployeDataException : Exception
        {
            public InValidEmployeDataException(string message)
                : base(message)
            {

            }
        }
    }
}