using System;

using Keystore_Extractor.Models;

namespace Keystore_Extractor.Helper
{
    internal class EventsAndActions
    {
        public static Action<Guid> OnKeystoreRemoved { get; set; } = delegate { };
        public static Action<KeystoreModel, DistinguishedNameModel> OnCreateNewKeystoreSuccess { get; set; } = delegate { };

        public static Action OnCreateNewKeystoreCancelled { get; set; } = delegate { };
    }
}