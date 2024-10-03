using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Keystore_Extractor.UserControls.KeystoreUC;

namespace Keystore_Extractor.Helper
{
    internal class EventsAndActions
    {
        public static Action<Guid> OnRemove { get; set; } = delegate { };
    }
}
