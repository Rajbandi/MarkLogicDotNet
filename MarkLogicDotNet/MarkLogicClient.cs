using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MarkLogicDotNet.Api;
using MarkLogicDotNet.Api.Contracts;
using MarkLogicDotNet.Core;
using Refit;

namespace MarkLogicDotNet
{
    /// <summary>
    /// Client implementation for MarkLogic rest api. Use this client to call MarkLogic Api methods.
    /// </summary>
    public class MarkLogicClient
    {
        /// <summary>
        /// Connection config
        /// </summary>
        public ConnectionConfig Config { get; }

        /// <summary>
        /// Credential cache for digest authentication
        /// </summary>
        private CredentialCache _crendentails;

        /// <summary>
        /// Authorization header for basic, JWT and SAML tokens
        /// </summary>
        private string _authHeader;

        /// <summary>
        /// Api interface
        /// </summary>
        private IMarkLogicApi _api;

        /// <summary>
        /// Marklogic Rest api base url
        /// </summary>
        private Uri _uri;

        /// <summary>
        /// Initializes client with config
        /// </summary>
        /// <param name="config">Connection config <see cref="ConnectionConfig"/></param>
        public MarkLogicClient(ConnectionConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("Invalid config provided. Config cannot be null");
            }
            this.Config = config;
            InitClient();
        }

        /// <summary>
        /// Validates config and Initializes client.
        /// </summary>
        protected void InitClient()
        {
            if (string.IsNullOrWhiteSpace(Config.Host))
            {
                throw new NullReferenceException("Invalid host address provided. Host cannot be null or empty. A valid address is required.");
            }

            if (Config.Port <= 0)
            {
                Config.Port = 8000;
            }

            var uriBuilder = new UriBuilder(Config.Host);
            uriBuilder.Port = Config.Port;
            uriBuilder.Scheme = Config.SSL ? "https" : "http";

            _uri = uriBuilder.Uri;

            var authType = Config.AuthType;
            if (authType != AuthenticationType.NONE &&
                authType != AuthenticationType.CERTIFICATE && authType != AuthenticationType.KERBEROS
                && authType != AuthenticationType.SAML)
            {
                if (string.IsNullOrWhiteSpace(Config.User) || string.IsNullOrWhiteSpace(Config.Password))
                {
                    throw new NullReferenceException($"Cannot create client without user or password for {Config.Host} host and {Config.Port} port");
                }
            }

            if (authType == AuthenticationType.SAML)
            {
                var token = Config.Token;
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new NullReferenceException($"Cannot create SAML client without valid token string for {Config.Host} host and {Config.Port} port");
                }
            }
            else
            if (authType == AuthenticationType.DIGEST)
            {
                _crendentails = new CredentialCache();
                _crendentails.Add(_uri, "Digest", new NetworkCredential(Config.User, Config.Password));
            }
            else
                if (authType != AuthenticationType.DIGEST && authType != AuthenticationType.CERTIFICATE)
            {

            }


        }

        /// <summary>
        /// Init the api with config
        /// </summary>
        protected void InitApi()
        {
            var clientHandler = new HttpClientHandler();
            if (_crendentails != null)
            {
                clientHandler.Credentials = _crendentails;
            }
            var httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = _uri
            };
            if (!string.IsNullOrWhiteSpace(_authHeader))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", _authHeader);
            }
            _api = RestService.For<IMarkLogicApi>(httpClient);
        }

        #region Document api methods

        public async Task<BaseResponse<string>> SaveDocument(DocumentRequest docRequest)
        {
            if (docRequest == null)
            {
                throw new NullReferenceException("Invalid document request data. docRequest cannot be null");
            }

            if (string.IsNullOrEmpty(docRequest.Uri))
            {
                throw new NullReferenceException("Invalid document request data. docRequest Uri cannot be null or empty");
            }

            if (string.IsNullOrEmpty(docRequest.Content))
            {
                throw new NullReferenceException("Invalid document request data. docRequest Content cannot be null or empty");
            }

            var content = docRequest.Content;
            docRequest.Content = null;
            var response = await ParseApiRequest(() => Api.SaveDocument(docRequest, content));

            return response;
        }

        /// <summary>
        /// Gets the document. 
        /// </summary>
        /// <param name="docRequest">document data. For details <see cref="DocumentRequest"/></param>
        /// <returns></returns>
        public async Task<BaseResponse<string>> GetDocument(DocumentRequest docRequest)
        {

            var response = await ParseApiRequest(() => Api.GetDocument(docRequest));

            return response;
        }

        public void DeleteDocument()
        {

        }
        #endregion

        #region Api related


        /// <summary>
        /// Generic method to call api 
        /// </summary>
        /// <typeparam name="T">Respone data type</typeparam>
        /// <param name="apiMethod">Method to call</param>
        /// <returns>BaseResponse with response data <see cref="BaseResponse{T}"/></returns>
        private async Task<BaseResponse<T>> ParseApiRequest<T>(Func<Task<ApiResponse<T>>> apiMethod)
        {
            var response = new BaseResponse<T>();

            try
            {
                var message = await apiMethod();
                response.Success = message.IsSuccessStatusCode;

                if (response.Success)
                {
                    response.Data = message.Content;
                }
                else
                {
                    response.ErrorMessage = message.ReasonPhrase;
                }

            }
            catch (ApiException aex)
            {
                response.ErrorCode = "101";
                response.ErrorMessage = aex.Message;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorCode = "100";
                response.ErrorMessage = ex.Message;
                response.Success = false;
            }

            return response;

        }


        /// <summary>
        /// Returns api singleton instance
        /// </summary>
        public IMarkLogicApi Api
        {
            get
            {
                if (_api == null)
                {
                    InitApi();
                }

                return _api;
            }
        }

        #endregion
    }
}
