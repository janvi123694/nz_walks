using System.ComponentModel.DataAnnotations;

namespace nz_walks.Models.DTO;

public class RegisterRequestDto
{
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set;  }
    
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string Password { get; set;  }
    
    public string[] Roles { get; set;  }
}