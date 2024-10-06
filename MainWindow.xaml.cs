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

using Keystore_Extractor.Commands;
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
            catch { }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void AddKeystore_Click(object sender, RoutedEventArgs e)
        {
            var newKeystoreModel = new KeystoreModel().SetNew(); // Create a new instance of KeystoreModel
            var newKeystoreControl = new KeystoreUserControl(newKeystoreModel); // Create a new KeystoreUserControl instance
            Keystores.Add(newKeystoreControl); // Add the control 
        }

        // Event handler to extract SHA1 and SHA256 for each prefab
        private void ExtractAll_Click(object sender, RoutedEventArgs e)
        {
            ExtractFromItemContainer();
        }

        async void ExtractFromItemContainer()
        {
            var commandProcess = KeytoolCommandRunner.CreateKeytoolProcess();
            foreach (var item in ItemsContainer.Items)
            {
                if (item is KeystoreUserControl prefab)
                {
                    var keystoreData = prefab.Keystore;
                    KeytoolCommandRunner.RunCommandAsynchronous(keystoreData, commandProcess);
                    prefab.Keystore.SHA1=keystoreData.SHA1;
                    prefab.Keystore.SHA256 = keystoreData.SHA256;
                    await Task.CompletedTask;
                }
            }
        }

        // Event handler for Export button (implement export logic here)
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            // Export logic for the MyPrefabs
            MessageBox.Show("Export functionality not yet implemented.");
        }
    }
}
