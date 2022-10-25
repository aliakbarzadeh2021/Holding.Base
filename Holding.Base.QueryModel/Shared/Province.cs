using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holding.Base.QueryModel.Shared
{
    [Table("Province")]
    public class Province
    {
        [Key]
        public Guid ProvinceId { get; set; }
        public string Name { get; set; }
    }
}