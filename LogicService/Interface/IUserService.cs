using RequestResponseModel.Request;
using RequestResponseModel.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicService.Interface {
	public interface IUserService {

        /// <summary>
        /// 使用者 查詢多筆
        /// </summary>
        /// <returns></returns>
		Task<RServiceProvider<List<UserResponse>>> GetUsers();
        /// <summary>
        /// 使用者 查詢單筆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RServiceProvider<UserResponse>> GetUser(long id);
        /// <summary>
        /// 使用者 查詢單筆 帳號
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RServiceProvider<UserResponse>> GetUser(string account,string pwd);
        /// <summary>
        /// 使用者 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RServiceProvider<int>> InsertUser(UserRequest request);
        /// <summary>
        /// 使用者 新增或編輯
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RServiceProvider<int>> ModifyUser(long id,UserRequest request);
        /// <summary>
        /// 使用者 刪除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RServiceProvider<int>> DeleteUser(long id);
    }
}
