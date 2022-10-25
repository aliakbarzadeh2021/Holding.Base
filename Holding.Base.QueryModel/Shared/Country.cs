using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holding.Base.QueryModel.Shared
{
    [Table("Country")]
    public class Country
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}