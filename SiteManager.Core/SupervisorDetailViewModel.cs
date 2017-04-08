using SiteManager.Core.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SiteManager.Core.Command;
using System.Windows;
using System;

namespace SiteManager.Core
{
    public class SupervisorDetailViewModel : ViewModelBase
    {
        public SupervisorDetailViewModel()
        {
            var supervisors = new List<Supervisor>() {
                new Supervisor { SupervisorId= 1, SupervisorName = "nik", MonthlySalary= 5000, DutyDescription = "" } };
            _supervisors = new ObservableCollection<Supervisor>(supervisors);
            NameVisibility = Visibility.Hidden;
            SalaryVisibility = Visibility.Hidden;
            Add = new RelayCommand(AddSupervisorCommand);
            Delete = new RelayCommand(DeleteSupervisorCommand);
            SupervisorToAdd = new Supervisor();
        }

        public RelayCommand Add { get; set; }
        public RelayCommand Delete { get; set; }

        private ObservableCollection<Supervisor> _supervisors;

        public ObservableCollection<Supervisor> Supervisors
        {
            get { return _supervisors; }
            set
            {
                _supervisors = value;
                OnPropertyChanged(nameof(Supervisors));
            }
        }

        private Visibility _nameVisibility;

        public Visibility NameVisibility
        {
            get { return _nameVisibility; }
            set { _nameVisibility = value; OnPropertyChanged(nameof(NameVisibility)); }
        }

        private Visibility _salaryVisibility;

        public Visibility SalaryVisibility
        {
            get { return _salaryVisibility; }
            set { _salaryVisibility = value; OnPropertyChanged(nameof(SalaryVisibility)); }
        }

        private Supervisor _supervisorToAdd;

        public Supervisor SupervisorToAdd
        {
            get { return _supervisorToAdd; }
            set { _supervisorToAdd = value; OnPropertyChanged(nameof(SupervisorToAdd)); }
        }


        private void AddSupervisorCommand(object model)
        {
            var supervisorModel = model as Supervisor;

            if (string.IsNullOrWhiteSpace(supervisorModel.SupervisorName))
            {
                NameVisibility = Visibility.Visible;
                return;
            }
            if (supervisorModel.MonthlySalary < 1)
            {
                SalaryVisibility = Visibility.Visible;
                return;
            }

            _supervisors.Add(supervisorModel);
            SupervisorToAdd = new Supervisor();
        }

        private void DeleteSupervisorCommand(object model)
        {
            var supervisorModel = model as Supervisor;
            if(OnMessageBoxEvent())
                _supervisors.Remove(supervisorModel);
        }
    }
}
