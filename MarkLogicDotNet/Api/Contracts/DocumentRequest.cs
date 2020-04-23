using System;
using System.Collections.Generic;
using Refit;

namespace MarkLogicDotNet.Api.Contracts
{
    public class DocumentRequest
    {
        public DocumentRequest()
        {

        }

        [AliasAs("uri")]
        public string Uri { get; set; }

        [AliasAs("category")]
        public string Category { get; set; }

        [AliasAs("metadata_param")]
        public string MetadataParam { get; set; }

        [AliasAs("extension")]
        public string Extension { get; set; }

        [AliasAs("default")]
        public string Default { get; set; }

        [AliasAs("transform")]
        public string Transform { get; set; }

        [AliasAs("trans:arg")]
        public string TransArg { get; set; }

        [AliasAs("database")]
        public string Database { get; set; }
    }
}
