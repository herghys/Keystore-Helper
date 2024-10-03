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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.ComponentModel;

namespace Keystore_Extractor.UserControls
{
    /// <summary>
    /// Interaction logic for KeystoreUserControl.xaml
    /// </summary>
    public partial class KeystoreUserControl : UserControl, INotifyPropertyChanged
    {
        private string _filePath;
        private string _alias;
        private string _storePass;
        private string _sha1;
        private string _sha256;

        public string FilePath
        {
            get => _filePath;
            set { _filePath=value; OnPropertyChanged(nameof(FilePath)); }
        }

        public string Alias
        {
            get => _alias;
            set { _alias=value; OnPropertyChanged(nameof(Alias)); }
        }

        public string StorePass
        {
            get => _storePass;
            set { _storePass=value; OnPropertyChanged(nameof(StorePass)); }
        }

        public string SHA1
        {
            get => _sha1;
            set { _sha1=value; OnPropertyChanged(nameof(SHA1)); }
        }

        public string SHA256
        {
            get => _sha256;
            set { _sha256=value; OnPropertyChanged(nameof(SHA256)); }
        }


        public KeystoreUserControl()
        {
            InitializeComponent();
            DataContext=this;
        }

        private void BrowseFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog()==true)
            {
                FilePath=openFileDialog.FileName;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
