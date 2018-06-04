using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.src.extension
{
    public interface IDigestSignerFactoryExtension
    {
        string AlgorithmName { get; }
        DerObjectIdentifier Oid { get; }

        bool CanCreateSigner(string signatureName);
        ISigner CreateSigner(string signatureName);
    }
}
