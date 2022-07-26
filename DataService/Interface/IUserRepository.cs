using RepositoryModel.Entity;
using RequestResponseModel.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryService.Interface {
	public interface IUserRepository {
		List<User> GetAllUser();
		User GetUser(long id);
		User GetUser(string account, string pwd);
		int InsertUser(UserRequest userModel);
		int ModifyUser(UserRequest userModel);
		int DeleteUser(long id);
	}
}
