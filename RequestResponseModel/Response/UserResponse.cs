using System;
using System.Collections.Generic;
using System.Text;

namespace RequestResponseModel.Response {
	public class UserResponse: BaseModel {
		public string Account { get; set; }
		public string PassWord { get; set; }
		public string UserName { get; set; }
		public int CellPhone { get; set; }
	}
}
