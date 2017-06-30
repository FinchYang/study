namespace study
{
    public class LoginAndQueryResponse : CommonResponse
    {
        public string Token { get; set; }
        public bool AllowedToStudy { get; set; }
         public bool Completed { get; set; }
           public bool Signed { get; set; }
            public bool FirstSigned { get; set; }
        public string AllStatus { get; set; }
    }
}
