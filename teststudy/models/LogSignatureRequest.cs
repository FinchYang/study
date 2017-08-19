
using System;

namespace  study
{
   public class LogSignatureRequest
    {
         public string Token { get; set; }
        public string SignatureFile { get; set; }
       
       public SignatureType SignatureType { get; set; }
        public string PostalAddress { get; set; }
    }
}
