using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDTO
    {
        public int? userId { get; set; }

   
        public string email { get; set; }
        public string? password { get; set; }
        public string? name{get;set;}
        public string? avatar{get;set;}
        public string? token{get;set;}
    }
}