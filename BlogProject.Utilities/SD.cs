using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject12.Utilities
{
    public class SD
    {

        public const string Proc_Tag_Create = "usp_CreateTagModel";
        public const string Proc_Tag_Get = "usp_GetTagModel";
        public const string Proc_Tag_GetAll = "usp_GetTagModels";
        public const string Proc_Tag_Update = "usp_UpdateTagModel";
        public const string Proc_Tag_Delete = "usp_DeleteTagModel";


        //Payment status
        public const string PaymentInitiated = "Initiated";
        public const string PaymentSuccessful = "Successful";
        public const string PaymentPending = "Pending";
        public const string PaymentFailed = "Failed";
        public const string PaymentCallBackUrl = "https://localhost:44381/Payment/Payment/HandleResponse";

    }
}
