using System;
namespace MarkLogicDotNet.Core
{
    public enum AuthenticationType
    {
        NONE,
        KERBEROS,
        CERTIFICATE,
        DIGEST,
        SAML
    }
}
