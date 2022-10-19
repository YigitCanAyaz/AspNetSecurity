using System.ComponentModel.DataAnnotations.Schema;

namespace DataProtection.Web.Models
{
    public class CustomProduct
    {
        [NotMapped]
        public string EncryptedId { get; set; }
    }
}
