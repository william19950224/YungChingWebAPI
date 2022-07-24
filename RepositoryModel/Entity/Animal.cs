using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.Entity {
	public class Animal : BaseEntity {
		public long UserId { get; set; }
		public string AnimalName { get; set; }
		public string AnimalAge { get; set; }

	}
}
