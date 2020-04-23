using System;
using System.Runtime.Serialization;

namespace MarkLogicDotNet.Api.Contracts
{
    [DataContract]
    public class BaseResponse<T>
    {

        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "errorCode")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "data")]
        public T Data { get; set; }
    }
}
