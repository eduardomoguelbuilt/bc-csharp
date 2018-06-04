using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.src.extension;

namespace Org.BouncyCastle.Extension
{
    /// <summary>
    /// Extension Manager
    /// </summary>
    public static class ExtensionManager
    {
        private static List<IKeyFactoryExtension> pubKeysFactories = new List<IKeyFactoryExtension>();
        private static List<IKeyFactoryExtension> privKeysFactories = new List<IKeyFactoryExtension>();
        private static List<IDigestFactoryExtension> digestsFactories = new List<IDigestFactoryExtension>();
        private static List<IDigestSignerFactoryExtension> digestSignerFactories = new List<IDigestSignerFactoryExtension>();
        private static Dictionary<string, string> encryptionAlgs = new Dictionary<string, string>();

        public static void RegisterPublicKeyFactory(IKeyFactoryExtension factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            pubKeysFactories.Add(factory);
        }

        public static IKeyFactoryExtension FindPublicKeyFactory(SubjectPublicKeyInfo keyInfo)
        {
            if (keyInfo == null)
                throw new ArgumentException("keyInfo");
            foreach (var factory in pubKeysFactories)
            {
                if (factory.CanCreateKey(keyInfo))
                    return factory;
            }
            return null;
        }

        public static void RegisterDigestFactory(IDigestFactoryExtension factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            digestsFactories.Add(factory);
        }

        public static IDigestFactoryExtension FindDigestFactory(string algName)
        {
            foreach (var factory in digestsFactories)
            {
                if (factory.CanCreateDigest(algName))
                    return factory;
            }
            return null;
        }

        public static IDigestFactoryExtension FindDigestFactory(DerObjectIdentifier oid)
        {
            foreach (var factory in digestsFactories)
            {
                if (factory.CanCreateDigest(oid))
                    return factory;
            }
            return null;
        }

        public static void AddEncryptionAlgorithm(DerObjectIdentifier oid, string name)
        {
            if (oid == null)
                throw new ArgumentNullException("oid");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            var oidStr = oid.Id;
            if (!encryptionAlgs.ContainsKey(oidStr))
                encryptionAlgs[oidStr] = name;
        }

        public static string FindEncryptionAlgorithmName(DerObjectIdentifier oid)
        {
            if (oid == null)
                throw new ArgumentNullException("oid");
            var oidStr = oid.Id;
            if (!encryptionAlgs.ContainsKey(oidStr))
                return null;
            return encryptionAlgs[oidStr];
        }

        public static void RegisterDigestSignerFactory(IDigestSignerFactoryExtension factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            digestSignerFactories.Add(factory);
        }

        public static IDigestSignerFactoryExtension FindDigestSignerFactory(string algName)
        {
            if (algName == null)
                throw new ArgumentNullException("algName");
            foreach (var factory in digestSignerFactories)
            {
                if (factory.CanCreateSigner(algName))
                    return factory;
            }

            return null;
        }
    }
}
