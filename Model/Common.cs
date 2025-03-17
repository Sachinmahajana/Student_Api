namespace Student_Api.Model
{
    
    
        public struct ApiResponse
        {
            public byte Status { get; set; }
            public string Message { get; set; }
            public string DetailedError { get; set; }
            public object Data { get; set; }
        }

        public enum StatusFlags : byte
        {
            Success = 1,
            Failed = 2,
            AlreadyExists = 3,
            DependancyExists = 4,
            NotPermitted = 5,
        }
}
