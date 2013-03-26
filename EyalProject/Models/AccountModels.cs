using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace EyalProject.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמא נוכחית")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} חייבת להיות באורך" +
                                          " {2} לפחות.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמא חדשה")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "וודא סיסמא חדשה")]
        [Compare("NewPassword", ErrorMessage = "שני הסיסמאות החדשות אינם זהות")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "שם משתמש")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמא")]
        public string Password { get; set; }

        [Display(Name = "זכור אותי?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "שם משתמש")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} חייבת להיות באורך" +
                                  " {2} לפחות.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמא")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "וודא סיסמא")]
        [Compare("Password", ErrorMessage = "שני הסיסמאות אינם זהות")]
        public string ConfirmPassword { get; set; }
    }
}
