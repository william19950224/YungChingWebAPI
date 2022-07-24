using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.Entity {
	public class BaseEntity {
        public long Id { get; set; }
        public int Valid { get; set; } = 1;
        public int IsDel { get; set; } = 0;
        public long CreatorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public long ModifierId { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}
