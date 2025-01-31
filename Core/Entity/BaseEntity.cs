using System;

namespace HRIS.Core.Entity
{
    public class BaseEntity
    {
        public bool IsActive { get; set; } = true;

        public Guid? CreatedBy { get; set; } = default!;
        public string? CreatedByName { get; set; } = default!;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public Guid? ModifiedBy { get; set; } = default!;
        public string? ModifiedByName { get; set; } = default!;
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
