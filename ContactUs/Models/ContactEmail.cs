using System.ComponentModel.DataAnnotations;

namespace ContactUs.Models
{
    public class ContactEmail
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Feedback Type")]
        public string FeedBackType { get; set; }
        [Required]
        [Display(Name = "You Are")]
        public string YouAre { get; set; }
        [Required]

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]

        public string Name { get; set; }
        [Required]


        public string Company { get; set; }
        [Required]
        [Display(Name = "Business Email")]
        [EmailAddress]
        public string BusinessEmail { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public int PhoneNo { get; set; }
        [Required]

        public string Message { get; set; }
        [Required]

        public bool TermsCondistion { get; set; }

    }
}
