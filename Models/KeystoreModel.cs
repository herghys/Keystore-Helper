using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keystore_Extractor.Models
{
    public class KeystoreModel : INotifyPropertyChanged
    {
        public Guid Id { get; private set; }
        private string _filePath;
        private string _alias;
        private string _storePass;
        private string _sha1;
        private string _sha256;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath=value;
                OnPropertyChanged(nameof(FilePath));
            }
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

        public KeystoreModel SetNew()
        {
            FilePath=string.Empty; // Initialize it
            Id=Guid.NewGuid();
            return this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
