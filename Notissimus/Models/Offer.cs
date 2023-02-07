using System;
using System.ComponentModel.DataAnnotations;

namespace Notissimus.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        public int Bid { get; set; }
        public int? CBid { get; set; }
        public bool Available { get; set; }
        public string? Type { get; set; }
        public string? OtherOfferProperties { get; set; }
        
    }
}
