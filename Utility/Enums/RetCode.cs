using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Utility.Enums {
    public enum RetCode {

        /// <summary>
        /// Success
        /// </summary>
        [Description("Success")]
        Success = 0,
        /// <summary>
        /// Success
        /// </summary>
        [Description("Fail")]
        Fail = 1,
        /// <summary>
        /// Exception
        /// </summary>
        [Description("Exception")]
        Exception = 9000000,
        /// <summary>
        /// 查無此資料，不可編輯
        /// </summary>
        [Description("查無此資料，不可編輯")]
        Exception_Modify = 9000001,
        /// <summary>
        /// 已有此資料，不可新增
        /// </summary>
        [Description("已有此資料，不可新增")]
        Exception_Insert = 9000002,
        /// <summary>
        /// 無資料
        /// </summary>
        [Description("無資料")]
        Exception_NoData = 9000003,

    }
}
