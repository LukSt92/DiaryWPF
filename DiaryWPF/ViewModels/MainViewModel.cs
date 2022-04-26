using DiaryWPF.Commands;
using DiaryWPF.Models;
using DiaryWPF.Models.Domains;
using DiaryWPF.Models.Wrappers;
using DiaryWPF.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiaryWPF.ViewModels
{
  public  class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        public MainViewModel()
        {
            AddStudentsCommand = new RelayCommand(AddEditStudent);
            EditStudentsCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentsCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            SettingsCommand = new RelayCommand(Settings);
            LoadedWindowCommand = new AsyncRelayCommand(LoadedWindow);

            Task task = LoadedWindow(null);
        }



        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(_repository.GetStudents(SelectedGroupId));
        }


        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand AddStudentsCommand { get; set; }
        public ICommand EditStudentsCommand { get; set; }
        public ICommand DeleteStudentsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }


        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set { _selectedGroupId = value;
                OnPropertyChanged();
                }
        }

        private ObservableCollection<Group> _group;

        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }


        
        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);


            SelectedGroupId = 0;
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia", $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} { SelectedStudent.LastName}?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }


        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }
        private void Settings(object obj)
        {
            var SettingsWindow = new SettingsView(true);
            SettingsWindow.Closed += SettingsWindow_Closed;
            SettingsWindow.ShowDialog();
        }

        private async Task LoadedWindow(object arg)
        {
            if (!IsValidConnectionToDatabase())
            {
                var metroWindow = Application.Current.MainWindow
                    as MetroWindow;
                var dialog = await metroWindow.ShowMessageAsync
                    ("Błąd połączenia.", $"Nie można połączyć się z bazą danych. Czy chcesz zmienić ustawienia?", MessageDialogStyle.AffirmativeAndNegative);

                if (dialog == MessageDialogResult.Negative)
                    Application.Current.Shutdown();
                else
                {
                    var settingswindow = new SettingsView(false);
                    settingswindow.ShowDialog();
                }
            }
            else
            {
                RefreshDiary();
                InitGroups();
            }



        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }
        private void SettingsWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }
        private bool IsValidConnectionToDatabase()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }       
        }


    }
}