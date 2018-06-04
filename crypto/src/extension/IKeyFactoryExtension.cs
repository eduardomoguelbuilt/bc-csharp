using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Extension
{
    public interface IKeyFactoryExtension
    {
        bool CanCreateKey(SubjectPublicKeyInfo keyInfo);
        AsymmetricKeyParameter CreateKey(SubjectPublicKeyInfo key);
    }
}
