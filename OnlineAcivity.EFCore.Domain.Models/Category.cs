using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineActivity.EFCore.Domain.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }
        [MaxLength(100)]
        public string CategoryName { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
    }
}
