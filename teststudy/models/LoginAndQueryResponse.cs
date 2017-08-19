namespace study
{  public class LoginAndQueryResponse : CommonResponse
    {
        public string Token { get; set; }
         public string LastToken { get; set; }
        public bool AllowedToStudy { get; set; }
        public bool Completed { get; set; }
        public bool Signed { get; set; }
        public bool FirstSigned { get; set; }
        public string AllStatus { get; set; }
        public string DrivingLicense { get; set; }
        public int DeductedMarks { get; set; }
         public byte[] Photo { get; set; }
    }
    public class SignatureQueryResponse : CommonResponse
    {      
        public bool Completed { get; set; }
        public bool Signed { get; set; }
       
        public string AllStatus { get; set; }
    
    }
}
