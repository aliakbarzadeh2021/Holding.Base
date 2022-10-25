using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holding.Base.QueryModel.Shared
{
    [Table( "SchoolType" )]
    public class SchoolType
    {
        [Key]
        public Guid SchoolTypeId { get; set; }
        public string Name { get; set; }
    }
}
