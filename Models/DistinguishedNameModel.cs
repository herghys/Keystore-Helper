using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keystore_Extractor.Models
{
    [Serializable]
    public class DistinguishedNameModel : INotifyPropertyChanged
    {
        private string _name;
        private string _organizationalUnit;
        private string _organization;
        private string _city;
        private string _state;
        private string _country;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string OrganizationalUnit
        {
            get => _organizationalUnit;
            set
            {
                _organizationalUnit = value;
                OnPropertyChanged(nameof(OrganizationalUnit));
            }
        }
        public string Organization
        {
            get => _organization;
            set
            {
                _organization = value;
                OnPropertyChanged(nameof(Organization));
            }
        }
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public string State 
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }
        public string Country 
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
