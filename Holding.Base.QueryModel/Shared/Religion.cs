using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holding.Base.QueryModel.Shared
{
    [Table("Religion")]
    public class Religion
    {
        [Key]
        public string Name { get; set; }
    }
}