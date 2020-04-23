using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Refit;

namespace MarkLogicDotNet.Api.Contracts
{
    /// <summary>
    /// Document request used to provide document related information
    /// </summary>
    [DataContract]
    public class DocumentRequest
    {
        public DocumentRequest()
        {

        }

        /// <summary>
        /// Document uri
        /// </summary>
        [DataMember(Name = "uri")]
        [AliasAs("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Document uri
        /// </summary>
        [DataMember(Name = "content")]
        public string Content { get; set; }

        /// <summary>
        /// Database name. 
        /// </summary>
        [DataMember(Name = "database")]
        [AliasAs("database")]
        public string Database { get; set; }


        /// <summary>
        /// Response format. Accepted values are xml or json.  
        /// </summary>
        [DataMember(Name = "format")]
        [AliasAs("format")]
        public string Format { get; set; }


        /// <summary>
        /// Category. For multiple categories, separate with ;
        /// </summary>
        [DataMember(Name = "category")]
        [AliasAs("category")]
        public string Category { get; set; }

        /// <summary>
        /// Meta data parameter
        /// </summary>
        [DataMember(Name = "metadata_param")]
        [AliasAs("metadata_param")]
        public string MetadataParam { get; set; }

        /// <summary>
        /// Extension
        /// </summary>
        [DataMember(Name = "extension")]
        [AliasAs("extension")]
        public string Extension { get; set; }


        /// <summary>
        /// Default. 
        /// </summary>
        [DataMember(Name = "default")]
        [AliasAs("default")]
        public string Default { get; set; }

        /// <summary>
        /// Transform
        /// </summary>
        [DataMember(Name = "transform")]
        [AliasAs("transform")]
        public string Transform { get; set; }

        [DataMember(Name = "trans:arg")]
        [AliasAs("trans:arg")]
        public string TransArg { get; set; }


        /// <summary>
        /// Multi-statement transaction id
        /// </summary>
        [DataMember(Name = "txid")]
        [AliasAs("txid")]
        public string TxId { get; set; }

        [DataMember(Name = "forest-name")]
        [AliasAs("forest-name")]
        public string ForestName { get; set; }


    }
}
