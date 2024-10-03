using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Keystore_Extractor.Helper;
using Keystore_Extractor.UserControls;
using Keystore_Extractor.UserControls.KeystoreUC;

namespace Keystore_Extractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<KeystoreUserControl> Keystores { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            EventsAndActions.OnRemove+=OnKeystoreRemoved;
            Keystores=new ObservableCollection<KeystoreUserControl>();
            ItemsContainer.ItemsSource=Keystores;
        }

        private void OnKeystoreRemoved(Guid guid)
        {
            var data = Keystores.Where(x => x.Keystore.Id==guid).Single();
            try
            {
                Keystores.Remove(data);
            }
            catch (Exception e)
            {
                throw e; 
            }
        }

        private void AddPrefab_Click(object sender, RoutedEventArgs e)
        {
            var newKeystoreModel = new KeystoreModel().SetNew(); // Create a new instance of KeystoreModel
            var newKeystoreControl = new KeystoreUserControl(newKeystoreModel); // Create a new KeystoreUserControl instance
            Keystores.Add(newKeystoreControl); // Add the control 
        }

        // Event handler to extract SHA1 and SHA256 for each prefab
        private void ExtractAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ItemsContainer.Items)
            {
                if (item is KeystoreUserControl prefab)
                {
                    string alias = prefab.Keystore.Alias;
                    string keystore = prefab.Keystore.FilePath;
                    string storepass = prefab.Keystore.StorePass;

                    // Build the command to run keytool
                    string keytoolCmd = $"-list -v -alias {alias} -keystore \"{keystore}\" -storepass {storepass}";

                    // Execute the command and extract SHA1 and SHA256
                    Process process = new Process();
                    process.StartInfo.FileName="keytool";
                    process.StartInfo.Arguments=keytoolCmd;
                    process.StartInfo.RedirectStandardOutput=true;
                    process.StartInfo.UseShellExecute=false;
                    process.StartInfo.CreateNoWindow=true;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    // Extract SHA1 and SHA256 from the output
                    string sha1 = ExtractSHAValue(output, "SHA1");
                    string sha256 = ExtractSHAValue(output, "SHA256");

                    // Bind the extracted values back to the prefab
                    prefab.Keystore.SHA1=sha1;
                    prefab.Keystore.SHA256=sha256;
                }
            }
        }

        // Function to extract SHA1 or SHA256 from keytool output
        private string ExtractSHAValue(string output, string shaType)
        {
            string shaPattern = $"{shaType}:";
            int shaIndex = output.IndexOf(shaPattern);
            if (shaIndex!=-1)
            {
                int startIndex = shaIndex+shaPattern.Length;
                string shaValue = output.Substring(startIndex).Trim();
                return shaValue.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)[0];
            }
            return string.Empty;
        }

        // Event handler for Export button (implement export logic here)
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            // Export logic for the MyPrefabs
            MessageBox.Show("Export functionality not yet implemented.");
        }
    }
}
