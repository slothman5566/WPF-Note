using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Model
    {
        [Required(ErrorMessage = "You must enter a username.")]
        [StringLength(10, MinimumLength = 4,
            ErrorMessage = "The username must be between 4 and 10 characters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The username must only contain letters (a-z, A-Z).")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter a name.")]
        public string Name { get; set; }
    }
}
