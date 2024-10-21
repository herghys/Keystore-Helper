using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using Keystore_Extractor.Commands;
using Keystore_Extractor.Helper;
using Keystore_Extractor.Models;
using Keystore_Extractor.UserControls.KeystoreUC;
using Keystore_Extractor.Windows;

namespace Keystore_Extractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<KeystoreUserControl> Keystores { get; set; }
        NewKeystore newKeystoreWindow;

        public MainWindow()
        {
            InitializeComponent();
            InitializeEvents();
            Keystores=new ObservableCollection<KeystoreUserControl>();
            ItemsContainer.ItemsSource=Keystores;
        }

        public void InitializeEvents()
        {

            EventsAndActions.OnKeystoreRemoved+=OnKeystoreRemoved;
            EventsAndActions.OnCreateNewKeystoreSuccess += OnCreateNewKeystoreSuccess;
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
            foreach (var item in ItemsContainer.Items)
            {
                if (item is KeystoreUserControl prefab)
                {
                    var keystoreData = prefab.Keystore;
                    KeytoolCommandRunner.ExtractKeystoreCommand(keystoreData);
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

        private void CreateKeystore_Click(object sender, RoutedEventArgs e)
        {
            if (newKeystoreWindow != null)
                return;

            newKeystoreWindow = new NewKeystore();
            newKeystoreWindow.Closing += OnNewKeystoreClosing;
            newKeystoreWindow.Show();

            button_CreateKeystore.IsEnabled = false;

        }

        private void OnNewKeystoreClosing(object sender, CancelEventArgs e)
        {
            EventsAndActions.OnCreateNewKeystoreSuccess -= OnCreateNewKeystoreSuccess;
            newKeystoreWindow.Closing -= OnNewKeystoreClosing;

            newKeystoreWindow = null;

            button_CreateKeystore.IsEnabled = true;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #region Actions

        private void OnCreateNewKeystoreSuccess(KeystoreModel model1, DistinguishedNameModel model2)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
