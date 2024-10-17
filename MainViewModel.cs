using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Keystore_Extractor.Commands;
using Keystore_Extractor.UserControls.KeystoreUC;

namespace Keystore_Extractor
{
    internal class MainViewModel : INotifyPropertyChanged
    { 
        public ObservableCollection<KeystoreModel> Keystores { get; set; }

        public MainViewModel()
        {
            Keystores=new ObservableCollection<KeystoreModel>();
            // Load or initialize keystores here if needed
        }

        public void AddKeystore(string filePath = "")
        {
            Keystores.Add(GetNewKeystore(filePath));
        }

        public void RemoveKeystore(Guid id)
        {
            var keystoreToRemove = Keystores.FirstOrDefault(k => k.Id==id);
            if (keystoreToRemove!=null)
            {
                Keystores.Remove(keystoreToRemove);
            }
        }

        private KeystoreModel GetNewKeystore(string filepath = "")
        {
            var newKeystore = new KeystoreModel
            {
                FilePath=filepath,
                Alias=string.Empty,
                StorePass=string.Empty,
                SHA1=string.Empty,
                SHA256=string.Empty
            };
            return newKeystore;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
