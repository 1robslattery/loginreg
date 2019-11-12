    using System;
    using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

    namespace Unicorns.Models
    {
		// User is being referenced in mycontexts.cs "DbSet<User>"
        public class LoginUser
        {

			// Email
			[Required(ErrorMessage="Email is required!")]
			[MinLength(8, ErrorMessage="Enter a valid Email.")]
            public string LoginEmail {get; set;}

			// Password
			[DataType(DataType.Password)]
			[Required]
			[MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
            public string LoginPassword {get; set;}

        }
    }