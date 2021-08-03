using System.ComponentModel.DataAnnotations;  
  
namespace api.Authentication  
{  
    public class RegisterModel  
    {  
        [Required(ErrorMessage = "Имя пользователя не может быть пустым")]  
        public string Username { get; set; }  
  
        [EmailAddress]  
        [Required(ErrorMessage = "Почта не может быть пустым")]  
        public string Email { get; set; }  
  
        [Required(ErrorMessage = "Пароль не может быть пустым")]  
        public string Password { get; set; }  
  
    }  
}