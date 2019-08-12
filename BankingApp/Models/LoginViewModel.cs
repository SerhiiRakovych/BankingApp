using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankingApp.Models
{
    [NotMapped]
    public class LoginViewModel
        {
            [Required(ErrorMessage = "Не заполнено поле \"Логин\"")]
            [Display(Name = "Логин")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Не заполнено поле \"Пароль\"")]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }
        }
}