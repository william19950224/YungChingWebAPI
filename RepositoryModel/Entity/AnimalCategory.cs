using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.Entity {
	public class AnimalCategory : BaseEntity {
		public long AnimalId { get; set; }
		public string AnimalCategoryName { get; set; }
	}
}
