using SiteManager.Core.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SiteManager.Core.Command;
using System.Windows;
using System;
using SiteManager.Repository;

namespace SiteManager.Core
{
    public class SiteInformationViewModel : ViewModelBase
    {
        private readonly RepositoryManager _repositoryManager;
        private int count;
        public SiteInformationViewModel()
        {
            _repositoryManager = new RepositoryManager(new SqliteContext());
            _sites = new ObservableCollection<SiteModel>(_repositoryManager.GetSites());
            count = _sites.Count;
            AddSite = new RelayCommand(AddSiteCommand);
            SiteToAdd = new SiteModel();
            DeleteSite = new RelayCommand(DeleteSiteCommand);
            SiteErrorVisibility = Visibility.Hidden;
            AddressErrorVisibility = Visibility.Hidden;
        }

        public RelayCommand AddSite { get; set; }
        public RelayCommand DeleteSite { get; set; }

        private ObservableCollection<SiteModel> _sites;

        public ObservableCollection<SiteModel> Sites
        {
            get { return _sites; }
            set
            {
                _sites = value;
                OnPropertyChanged(nameof(Sites));
            }

        }

        private SiteModel _siteToAdd;

        public SiteModel SiteToAdd
        {
            get { return _siteToAdd; }
            set
            {
                _siteToAdd = value;
                OnPropertyChanged(nameof(SiteToAdd));
            }
        }

        private Visibility _siteErrorVisibility;

        public Visibility SiteErrorVisibility
        {
            get { return _siteErrorVisibility; }
            set
            {
                _siteErrorVisibility = value;
                OnPropertyChanged(nameof(SiteErrorVisibility));
            }
        }

        private Visibility _addressErrorVisibility;

        public Visibility AddressErrorVisibility
        {
            get { return _addressErrorVisibility; }
            set {
                _addressErrorVisibility = value;
                OnPropertyChanged(nameof(AddressErrorVisibility));
            }
        }

        private void AddSiteCommand(object siteToAdd)
        {
            var site = siteToAdd as SiteModel;
            site.SiteId = count = count + 1;
            site.CreatedDate = DateTime.Now;
            if (string.IsNullOrWhiteSpace(site.SiteName))
            {
                SiteErrorVisibility = Visibility.Visible;
                return;
            }
            if (string.IsNullOrWhiteSpace(site.Address))
            {
                AddressErrorVisibility = Visibility.Visible;
                return;
            }

            _repositoryManager.AddSite(site);
            _sites.Add(site);
            SiteToAdd = new SiteModel();
        }

        private void DeleteSiteCommand(object siteToDelete)
        {
            var site = siteToDelete as SiteModel;

            if (OnMessageBoxEvent(""))
            {
                _sites.Remove(site);
                _repositoryManager.DeleteSite(site);
            }
        }
    }
}
