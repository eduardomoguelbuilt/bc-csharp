using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Extension
{
    public interface IDigestFactoryExtension
    {
        string AlgorithmName { get; }
        DerObjectIdentifier Oid { get; }

        bool CanCreateDigest(string algName);
        bool CanCreateDigest(DerObjectIdentifier oid);
        IDigest CreateDigest();
    }
}
