using System;

namespace SharedModels.ViewModels
{
    public class LoginTokens
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
