using System.ComponentModel.DataAnnotations;

namespace eStartap.View_Models
{
    public class RegisterVM
    {
        [Required ,StringLength(maximumLength:20)]
        public string Name { get; set; }
        [Required, StringLength(maximumLength: 20)]
        public string Surname { get; set; }
        [Required, StringLength(maximumLength: 20)]
        public string Username { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
