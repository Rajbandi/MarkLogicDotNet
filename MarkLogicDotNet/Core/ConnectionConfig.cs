using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MarkLogicDotNet.Core
{
    /// <summary>
    /// This class represents MarkLogic configuration. This can be used by client to connect to MarkLogic server. 
    /// </summary>
    public class ConnectionConfig
    {
        /// <summary>
        /// Initializes config with default values.
        /// </summary>
        public ConnectionConfig() : this("", 8000, "", "", "", AuthenticationType.DIGEST, "")
        {
        }

        /// <summary>
        /// Initializes config from given values.
        /// </summary>
        /// <param name="host">host address</param>
        /// <param name="port">port number</param>
        /// <param name="database">database name</param>
        /// <param name="user">user name</param>
        /// <param name="password">password</param>
        /// <param name="authType">auth type</param>
        /// <param name="token">token</param>
        public ConnectionConfig(string host, int port, string database, string user, string password, AuthenticationType authType, string token)
        {
            this.Host = host;
            this.Port = port;
            this.Database = database;
            this.User = user;
            this.Password = password;
            this.AuthType = authType;
            this.Token = token;
        }

        /// <summary>
        /// Initializes config from json
        /// </summary>
        /// <param name="json">config json</param>
        public ConnectionConfig(string json)
        {
            var config = FromJson(json);

            this.Host = config.Host;
            this.Port = config.Port;
            this.Database = config.Database;
            this.User = config.User;
            this.Password = config.Password;
            this.AuthType = config.AuthType;
            this.Token = config.Token;
        }

        /// <summary>
        /// Host address
        /// </summary>
        [DataMember(Name = "host")]
        public string Host { get; set; }

        /// <summary>
        /// Port number
        /// </summary>
        [DataMember(Name = "port")]
        public int Port { get; set; }

        /// <summary>
        /// Database name
        /// </summary>
        [DataMember(Name = "database")]
        public string Database { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [DataMember(Name = "user")]
        public string User { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Authentication Type. It should be one of <see cref="AuthenticationType"/> values.
        /// </summary>
        [DataMember(Name = "authtype")]
        public AuthenticationType AuthType { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        [DataMember(Name = "token")]
        public string Token { get; set; }

        /// <summary>
        /// SSL. true if client uses SSL to connect. 
        /// </summary>
        [DataMember(Name = "ssl")]
        public bool SSL { get; set; }

        /// <summary>
        /// Agent.
        /// </summary>
        [DataMember(Name = "agent")]
        public bool Agent { get; set; }

        /// <summary>
        /// Instantiates new config object from given json data
        /// </summary>
        /// <param name="json">Json data</param>
        /// <returns></returns>
        static ConnectionConfig FromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new NullReferenceException("Config json cannot be null or empty. A valid config json is expected");
            }

            var config = JsonConvert.DeserializeObject<ConnectionConfig>(json);

            if (config == null)
            {
                throw new NullReferenceException("Config cannot instantiated from json. Please check json format ");
            }
            return config;
        }

        /// <summary>
        /// Returns config in json format.
        /// </summary>
        /// <returns>config in json format</returns>
        string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Returns string representation of this object.
        /// </summary>
        /// <returns>config in string</returns>
        public override string ToString()
        {
            var buffer = new List<String>();

            buffer.Add($"Host={Host}");
            buffer.Add($"Port={Port}");
            buffer.Add($"Database={Database}");
            buffer.Add($"User={User}");
            buffer.Add($"Password={Password}");
            buffer.Add($"AuthType={AuthType}");
            buffer.Add($"Token={Token}");
            buffer.Add($"SSL={SSL}");

            return String.Join("\n", buffer);
        }
    }
}
