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

        [Get("/{version}/documents")]
        Task<ApiResponse<string>> GetDocument(DocumentRequest query, string version = "LATEST");

        [Put("/{version}/documents")]
        Task<ApiResponse<string>> CreateOrUpdateDocument(DocumentRequest query, string version = "LATEST");

        [Put("/{version}/documents")]
        Task<ApiResponse<string>> DeleteDocument(DocumentRequest query, string version = "LATEST");

        #endregion



    }
}
