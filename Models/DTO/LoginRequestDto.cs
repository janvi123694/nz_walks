namespace nz_walks.Models.DTO;
using System.ComponentModel.DataAnnotations;

public class LoginRequestDto
{
    public LoginRequestDto()
    {
        
    }
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set;  }
    
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string Password { get; set;  }
}