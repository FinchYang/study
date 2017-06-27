using System;

namespace  study
{
    public class StudyStatusRequest
    {
        public string Token { get; set; }
         public string Identity { get; set; }
        public string CourseTitle { get; set; }
      
        public long StartTime { get; set; }
        public long EndTime { get; set; }
          public byte[] Pictures { get; set; }
    }
}