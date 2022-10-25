using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holding.Base.QueryModel.Shared
{
    [Table("SchoolSegment")]
    public class SchoolSegment
    {
        [Key]
        public Guid SegmentId { get; set; }
        public string Name { get; set; }
    }
}