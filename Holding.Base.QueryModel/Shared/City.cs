using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holding.Base.QueryModel.Shared
{
    [Table("CityView")]
    public class City
    {
        [Key]
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public string ProvinceName { get; set; }
    }
}