using AutoMapper;
using LogicService.Interface;
using RepositoryModel.Entity;
using RepositoryService;
using RequestResponseModel.Request;
using RequestResponseModel.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Enums;

namespace LogicService.Service {
    public class UserService : IUserService {
        UserRepository userRepository = new UserRepository();
        #region 使用者

        /// <summary>
        /// 取所有使用者
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RServiceProvider<List<UserResponse>>> GetUsers() {
            RServiceProvider<List<UserResponse>> rsp = new RServiceProvider<List<UserResponse>>();
            try {
                List<User> modelList = new List<User>();
                List<UserResponse> responsesModelList = new List<UserResponse>();
                modelList = userRepository.GetAllUser();
                rsp.Success = true;
                if (modelList != null && modelList.Count > 0) {
                    Mapper.Map<List<User>, List<UserResponse>>(modelList, responsesModelList);
                    rsp.ReturnCode = RetCode.Success.ToString();
                    rsp.HasResult = true;
                    rsp.Result = responsesModelList;
                }
            } catch (Exception ex) {
                rsp.Success = false;
                rsp.Message = ex.Message;
                rsp.ReturnCode = ((int)Utility.Enums.RetCode.Exception).ToString();
            }
            return rsp;
        }

        /// <summary>
        /// 取某使用者
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RServiceProvider<UserResponse>> GetUser(long id) {
            RServiceProvider<UserResponse> rsp = new RServiceProvider<UserResponse>();
            try {
                User model = new User();
                UserResponse responsesModel = new UserResponse();
                model = userRepository.GetUser(id);
                rsp.Success = true;
                if (model != null) {
                    Mapper.Map<User, UserResponse>(model, responsesModel);
                    rsp.ReturnCode = RetCode.Success.ToString();
                    rsp.HasResult = true;
                    rsp.Result = responsesModel;
                }
            } catch (Exception ex) {
                rsp.Success = false;
                rsp.Message = ex.Message;
                rsp.ReturnCode = ((int)Utility.Enums.RetCode.Exception).ToString();
            }
            return rsp;
        }

        /// <summary>
        /// 塞入某使用者
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RServiceProvider<int>> InsertUser(UserRequest request) {
            RServiceProvider<int> rsp = new RServiceProvider<int>();
            try {
                int resultId = 0;
                resultId = userRepository.InsertUser(request);
                if (resultId != 0) {
                    rsp.Success = true;
                    rsp.ReturnCode = RetCode.Success.ToString();
                    rsp.HasResult = true;
                    rsp.Result = resultId;
                }
            } catch (Exception ex) {
                rsp.Success = false;
                rsp.Message = ex.Message;
                rsp.ReturnCode = ((int)Utility.Enums.RetCode.Exception).ToString();
            }
            return rsp;
        }
        /// <summary>
        /// 塞入或更新某使用者
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RServiceProvider<int>> ModifyUser(long id, UserRequest request) {
            RServiceProvider<int> rsp = new RServiceProvider<int>();
            try {
                int resultId = 0;
                var userModel = userRepository.GetUser(id);
                //如有資料則update
                if (id != 0 && userModel!=null) {
                    request.Id = id;
                    resultId = userRepository.ModifyUser(request);
                    if (resultId != 0) {
                        rsp.Result = resultId;
                    }
                } else {
                    resultId = userRepository.InsertUser(request);
                    if (resultId != 0) {                       
                        rsp.Result = resultId;
                    }
                }
                rsp.Success = true;
                rsp.ReturnCode = RetCode.Success.ToString();
                rsp.HasResult = true;

            } catch (Exception ex) {
                rsp.Success = false;
                rsp.Message = ex.Message;
                rsp.ReturnCode = ((int)Utility.Enums.RetCode.Exception).ToString();
            }
            return rsp;
        }
        /// <summary>
        /// 刪除某使用者
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RServiceProvider<int>> DeleteUser(long id) {
            RServiceProvider<int> rsp = new RServiceProvider<int>();
            try {
                int resultId = 0;
                resultId = userRepository.DeleteUser(id);
                if (resultId != 0) {
                    rsp.Success = true;
                    rsp.ReturnCode = RetCode.Success.ToString();
                    rsp.HasResult = true;
                    rsp.Result = resultId;
                }
            } catch (Exception ex) {
                rsp.Success = false;
                rsp.Message = ex.Message;
                rsp.ReturnCode = ((int)Utility.Enums.RetCode.Exception).ToString();
            }
            return rsp;
        }
        #endregion

    }
}
