using System;
using System.Runtime.Serialization;

namespace MarkLogicDotNet.Api.Contracts
{
    /// <summary>
    /// Base response object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class BaseResponse<T>
    {

        /// <summary>
        /// Indicates whether successful or not
        /// </summary>
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// Error code if not success
        /// </summary>
        [DataMember(Name = "errorCode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// Error message if not success
        /// </summary>
        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Response data. If T is string, returns Raw json received. 
        /// </summary>
        [DataMember(Name = "data")]
        public T Data { get; set; }
    }
}
