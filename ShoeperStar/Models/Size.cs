using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoeperStar.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public int Quantity { get; set; }
        public string SKU { get; set; }

        public int VariantId { get; set; }
        [ForeignKey(nameof(VariantId))]
        public Variant Variant { get; set; }
    }
}
