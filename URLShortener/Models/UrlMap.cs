namespace URLShortener.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UrlMaps")]
    public class UrlMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment PK
        public long Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; } = string.Empty;

        [MaxLength(20)]
        [Required]
        public string ShortUrl { get; set; } = string.Empty;
    }
}
