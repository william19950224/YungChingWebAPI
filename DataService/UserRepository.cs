using Dapper;
using RepositoryModel.Entity;
using RequestResponseModel.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Utility.Const;

namespace RepositoryService {
	public class UserRepository: BaseRepository {

        public  List<User> GetAllUser() {
			var results = new List<User>();
			using (SqlConnection conn = new SqlConnection(ConnString)) {
				string strSql = "Select * from [User] where 1=1  " + BaseConst.valid_isdel;
				results = conn.Query<User>(strSql).ToList();
			}
			return results;
		}
		public User GetUser(long id) {
			var result = new User();
			using (SqlConnection conn = new SqlConnection(ConnString)) {
				var idValue = new { Id = id };
				string strSql = "Select * from [User] where 1=1  and Id =@Id" + BaseConst.valid_isdel;
				result = conn.Query<User>(strSql, idValue).FirstOrDefault();
			}
			return result;
		}
		public User GetUser(string account, string pwd) {
			var result = new User();
			using (SqlConnection conn = new SqlConnection(ConnString)) {
				var value = new { Account = account, PassWord= pwd };
				string strSql = "Select * from [User] where 1=1  and Account =@Account and PassWord =@PassWord  " + BaseConst.valid_isdel;
				result = conn.Query<User>(strSql, value).FirstOrDefault();
			}
			return result;
		}
		public int InsertUser(UserRequest userModel) {
			int result = 0;
			using (SqlConnection conn = new SqlConnection(ConnString)) {
				string statement = @"
                                 INSERT INTO [User]
                                 VALUES (@Account, @PassWord, @UserName, @CellPhone,@Valid,@IsDel,
                                         @CreatorId, @CreatedAt, @ModifierId, @ModifiedAt);
                                 SELECT SCOPE_IDENTITY();
                                 ";
				result = conn.ExecuteScalar<int>(statement, userModel);
			}
			return result;
		}
		public int ModifyUser(UserRequest userModel) {
			int result = 0;
			using (SqlConnection conn = new SqlConnection(ConnString)) {
				string statement = @"
                                  UPDATE [User]
                                  SET Account = @Account,PassWord = @PassWord,
                                      UserName = @UserName,CellPhone = @CellPhone,
                                      ModifierId= @ModifierId
                                  WHERE Id= @Id;;                 
                                 ";
				conn.ExecuteScalar<int>(statement, userModel);
				result = Convert.ToInt32(userModel.Id);
			}
			return result;
		}
		public int DeleteUser(long id) {
			int result = 0;
			using (SqlConnection conn = new SqlConnection(ConnString)) {
				string statement = @"
                                  UPDATE [User]
                                  SET Valid = 0
                                  WHERE Id= @Id;
                                 ";
				result= conn.ExecuteScalar<int>(statement, new
				{
					Id = id
				});
				result = Convert.ToInt32(id);
			}
			return result;
		}
	}
}
