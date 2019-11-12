    using System;
    using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

    namespace Unicorns.Models
    {
		// User is being referenced in mycontexts.cs "DbSet<User>"
        public class User
        {
            // auto-implemented properties need to match the columns in your table
            // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
            [Key]
            public int UserID {get; set;}
            // MySQL VARCHAR and TEXT types can be represeted by a string

			// User Name
			[Required(ErrorMessage="Username is required!")]
			[MinLength(4, ErrorMessage="No abbreviated names, please.")]
            public string Name {get; set;}

			// Email
			[Required(ErrorMessage="Email is required!")]
			[MinLength(8, ErrorMessage="Enter a valid Email.")]
            public string Email {get; set;}

			// Password
			[DataType(DataType.Password)]
			[Required]
			[MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
            public string Password {get; set;}

			// Password Confirmation
			[NotMapped]
			[Compare("Password", ErrorMessage="Password must match")]
			[DataType(DataType.Password)]
			public string ConfirmPassword {get;set;}

            // The MySQL DATETIME type can be represented by a DateTime
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
        }
    }