using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineActivity.EFCore.Domain.Models
{
    [Table("Document")]
    public class Document
    {
        [Key]
        [Required]
        public Guid DocumentID { get; set; }
        [Required]
        public string DocumentName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
    }
}
