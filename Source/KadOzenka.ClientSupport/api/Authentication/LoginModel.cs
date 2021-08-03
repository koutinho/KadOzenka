using System.ComponentModel.DataAnnotations;  
  
namespace api.Authentication  
{  
    public class LoginModel  
    {  
        [Required(ErrorMessage = "Имя пользователя не может быть пустым")]  
        public string Username { get; set; }  
  
        [Required(ErrorMessage = "Пароль не может быть пустым")]  
        public string Password { get; set; }  
    }  
}