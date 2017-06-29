using System;

namespace study
{
    public class CompleteCoursesRequest
    {
        public string Token { get; set; }
        public string AllStatus { get; set; }
        public byte[] AllRecords { get; set; }
    }
}