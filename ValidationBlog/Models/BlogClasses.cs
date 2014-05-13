using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ValidationBlog.Models
{
    public class Blog : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [StringLengthValidator(1, 25,
            MessageTemplateResourceType = typeof(Resources),
            MessageTemplateResourceName = "BloggerNameMessage")]
        [DisplayName("Blogger Name")]
        public string BloggerName { get; set; }

        //[DataType(DataType.MultilineText)]
        //public string Address { get; set; }
   
        [StringLengthValidator(1, 50,
            MessageTemplateResourceType = typeof(Resources),
            MessageTemplateResourceName = "StreetAddressMessage")]
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }


        [StringLengthValidator(1, 30,
            MessageTemplateResourceType = typeof(Resources),
            MessageTemplateResourceName = "CityMessage")]
        public string City { get; set; }


        [StringLengthValidator(2, 2,
            MessageTemplateResourceType = typeof(Resources),
            MessageTemplateResourceName = "StateMessage")]
        public string State { get; set; }
        
     
        [RegexValidator(@"^\d{5}$",
            MessageTemplateResourceType = typeof(Resources),
            MessageTemplateResourceName = "ZipCodeMessage")]
        public string ZipCode { get; set; }

        [Required]
        [Range(1,10)]
        public int Rating { get; set; }
        
        [Required]
        [DisplayName("Date Created")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)] or
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        
        public virtual ICollection<Post> Posts { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var field = new[] { "DateCreated" };

            if (DateCreated > DateTime.Now)
            {
                yield return new ValidationResult("Date created cannot be in the future.", field);
            }

            if (DateCreated <= DateTime.Now.AddDays(-1))
            {
                yield return new ValidationResult("Date created cannot be in the past.", field);
            }
        }
    }


    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }

    public class Comment
    {

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }

        //navigation back to parent
        public Post Post { get; set; }
    }
}