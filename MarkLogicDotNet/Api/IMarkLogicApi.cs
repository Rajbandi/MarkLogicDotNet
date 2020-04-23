using System;
using System.Net.Http;
using System.Threading.Tasks;
using MarkLogicDotNet.Api.Contracts;
using Refit;

namespace MarkLogicDotNet.Api
{
    public interface IMarkLogicApi
    {

        #region documents

        /// <summary>
        /// Gets the document 
        /// </summary>
        /// <param name="query">Document request data</param>
        /// <param name="version">Version. Default is Latest. </param>
        /// <returns></returns>
        [Get("/{version}/documents")]
        Task<ApiResponse<string>> GetDocument(DocumentRequest query, string version = "LATEST");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [Put("/{version}/documents")]
        Task<ApiResponse<string>> SaveDocument(DocumentRequest query, [Body] string body, string version = "LATEST");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [Post("/{version}/documents")]
        Task<ApiResponse<string>> SaveDocuments(DocumentRequest query, string version = "LATEST");

        /// <summary>
        /// /
        /// </summary>
        /// <param name="query"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [Put("/{version}/documents")]
        Task<ApiResponse<string>> DeleteDocument(DocumentRequest query, string version = "LATEST");

        #endregion



    }
}
