using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Keystore_Extractor.Models;

namespace Keystore_Extractor.Windows
{
    /// <summary>
    /// Interaction logic for NewKeystore.xaml
    /// </summary>
    public partial class NewKeystore : Window
    {
        public KeystoreModel Keystore;
        public DistinguishedNameModel DistinguishedName;
        public NewKeystore()
        {
            InitializeComponent();
        }

        private void ComboKeystoreType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
