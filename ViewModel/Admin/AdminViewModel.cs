using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PropertyChanged;
using ConsoleDBTest.ViewModels;
using ConsoleDBTest.Dealer;
using Database4.Data;
using Database4.View;
using System.Windows;
using Database4.View.Login;
using Xceed.Wpf.Toolkit;
using ConsoleDBTest.Utils.StringUtils;
using ConsoleDBTest.Models;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AdminViewModel : ViewModel {
        public AdminViewModel(bool isAdmin) {
            this.IsAdmin = isAdmin;
            this.Update();
        }

        #region COUNTRIES

        public List<CountryViewModel> Countries { get; set; }
        public CountryViewModel SelectedCountry { get; set; } = null;

        public List<CountryViewModel> GetCountries() =>
            new CountryDealer().Select(GlobalAppDataContext.Instance).ToList().Select(c => new CountryViewModel(c)).ToList();

        public void UpdateCountries() => this.Countries = this.GetCountries();

        public void AddToCountries() {
            var temp = new AddCountry();
            temp.DataContext = new AddCountryViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditCountries() {
            var temp = new AddCountry();
            temp.DataContext = new AddCountryViewModel(temp, this.SelectedCountry.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteCountries() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить эту страну?", "Удаление страны", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new CountryDealer().Delete(GlobalAppDataContext.Instance, this.SelectedCountry.Id);
                this.Update();
            }
        }

        public ICommand AddToCountriesCommand  => new RelayCommand(this.AddToCountries);
        public ICommand EditCountriesCommand   => new RelayCommand(this.EditCountries, _ => this.SelectedCountry != null);
        public ICommand DeleteCountriesCommand => new RelayCommand(this.DeleteCountries, _ => this.SelectedCountry != null);

        #endregion

        #region CITIES

        public List<CityViewModel> Cities { get; set; }
        public CityViewModel SelectedCity { get; set; } = null;

        public List<CityViewModel> GetCities() =>
            new CityDealer().Select(GlobalAppDataContext.Instance).Select(city => new CityViewModel(city, GlobalAppDataContext.Instance)).ToList();

        public void UpdateCities() => this.Cities = this.GetCities();

        public void AddToCities() {
            var temp = new AddCity();
            temp.DataContext = new AddCityViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditCities() {
            var temp = new AddCity();
            temp.DataContext = new AddCityViewModel(temp, this.SelectedCity.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteCities() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить этот город?", "Удаление города", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new CityDealer().Delete(GlobalAppDataContext.Instance, this.SelectedCity.Id);
                this.Update();
            }
        }

        public ICommand AddToCitiesCommand  => new RelayCommand(this.AddToCities);
        public ICommand EditCitiesCommand   => new RelayCommand(this.EditCities, _ => this.SelectedCity != null);
        public ICommand DeleteCitiesCommand => new RelayCommand(this.DeleteCities, _ => this.SelectedCity != null);

        #endregion

        #region SPECIALTIES

        public List<SpecialtyViewModel> Specialties       { get; set; }
        public SpecialtyViewModel       SelectedSpecialty { get; set; } = null;

        public List<SpecialtyViewModel> GetSpecialties() =>
            new SpecialtyDealer().Select(GlobalAppDataContext.Instance)
                            .Select(spec => new SpecialtyViewModel(spec))
                            .ToList();

        public void UpdateSpecialties() => this.Specialties = this.GetSpecialties();

        public void AddToSpecialties() {
            var temp = new AddSpecialty();
            temp.DataContext = new AddSpecialtyViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditSpecialties() {
            var temp = new AddSpecialty();
            temp.DataContext = new AddSpecialtyViewModel(temp, this.SelectedSpecialty.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteSpecialties() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить эту специальность?", "Удаление специальности",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new SpecialtyDealer().Delete(GlobalAppDataContext.Instance, this.SelectedSpecialty.Id);
                this.Update();
            }
        }

        public ICommand AddToSpecialtiesCommand  => new RelayCommand(this.AddToSpecialties);
        public ICommand EditSpecialtiesCommand   => new RelayCommand(this.EditSpecialties,   _ => this.SelectedSpecialty != null);
        public ICommand DeleteSpecialtiesCommand => new RelayCommand(this.DeleteSpecialties, _ => this.SelectedSpecialty != null);

        #endregion

        #region CATHEDRAS

        public List<CathedraViewModel> Cathedras  { get; set; }
        public CathedraViewModel SelectedCathedra { get; set; } = null;

        public List<CathedraViewModel> GetCathedras() =>
            new CathedraDealer().Select(GlobalAppDataContext.Instance)
                            .Select(cathedra => new CathedraViewModel(cathedra))
                            .ToList();

        public void UpdateCathedras() => this.Cathedras = this.GetCathedras();

        public void AddToCathedras() {
            var temp = new AddCathedra();
            temp.DataContext = new AddCathedraViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditCathedras() {
            var temp = new AddCathedra();
            temp.DataContext = new AddCathedraViewModel(temp, this.SelectedCathedra.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteCathedras() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить эту кафедру?", "Удаление кафедры",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new CathedraDealer().Delete(GlobalAppDataContext.Instance, this.SelectedCathedra.Id);
                this.Update();
            }
        }

        public ICommand AddToCathedrasCommand  => new RelayCommand(this.AddToCathedras);
        public ICommand EditCathedrasCommand   => new RelayCommand(this.EditCathedras, _ => this.SelectedCathedra != null);
        public ICommand DeleteCathedrasCommand => new RelayCommand(this.DeleteCathedras, _ => this.SelectedCathedra != null);

        #endregion

        #region FACULTIES

        public List<FacultyViewModel> Faculties { get; set; }
        public FacultyViewModel SelectedFaculty { get; set; } = null;

        public List<FacultyViewModel> GetFaculties() =>
            new FacultyDealer().Select(GlobalAppDataContext.Instance)
                            .Select(faculty => new FacultyViewModel(faculty))
                            .ToList();

        public void UpdateFaculties() => this.Faculties = this.GetFaculties();

        public void AddToFaculties() {
            var temp = new AddFaculty();
            temp.DataContext = new AddFacultiesViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditFaculties() {
            var temp = new AddFaculty();
            temp.DataContext = new AddFacultiesViewModel(temp, this.SelectedFaculty.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteFaculties() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить этот факультет?", "Удаление факультета",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new FacultyDealer().Delete(GlobalAppDataContext.Instance, this.SelectedFaculty.Id);
                this.Update();
            }
        }

        public ICommand AddToFacultiesCommand  => new RelayCommand(this.AddToFaculties);
        public ICommand EditFacultiesCommand   => new RelayCommand(this.EditFaculties, _ => this.SelectedFaculty != null);
        public ICommand DeleteFacultiesCommand => new RelayCommand(this.DeleteFaculties, _ => this.SelectedFaculty != null);

        #endregion

        #region GENRES

        public List<GenreViewModel> Genres  { get; set; }
        public GenreViewModel SelectedGenre { get; set; } = null;

        public List<GenreViewModel> GetGenres() =>
            new GenreDealer().Select(GlobalAppDataContext.Instance)
                            .Select(genre => new GenreViewModel(genre))
                            .ToList();

        public void UpdateGenres() => this.Genres = this.GetGenres();

        public void AddToGenres() {
            var temp = new AddGenre();
            temp.DataContext = new AddGenresViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditGenres() {
            var temp = new AddGenre();
            temp.DataContext = new AddGenresViewModel(temp, this.SelectedGenre.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteGenres() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить этот жанр?", "Удаление жанра",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new GenreDealer().Delete(GlobalAppDataContext.Instance, this.SelectedGenre.Id);
                this.Update();
            }
        }

        public ICommand AddToGenresCommand  => new RelayCommand(this.AddToGenres);
        public ICommand EditGenresCommand   => new RelayCommand(this.EditGenres, _ => this.SelectedGenre != null);
        public ICommand DeleteGenresCommand => new RelayCommand(this.DeleteGenres, _ => this.SelectedGenre != null);

        #endregion

        #region AUTHORS

        public List<AuthorViewModel> Authors  { get; set; }
        public AuthorViewModel SelectedAuthor { get; set; } = null;

        public List<AuthorViewModel> GetAuthors() =>
            new AuthorDealer().Select(GlobalAppDataContext.Instance)
                            .Select(author => new AuthorViewModel(author))
                            .ToList();

        public void UpdateAuthors() => this.Authors = this.GetAuthors();

        public void AddToAuthors() {
            var temp = new AddAuthor();
            temp.DataContext = new AddAuthorsViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditAuthors() {
            var temp = new AddAuthor();
            temp.DataContext = new AddAuthorsViewModel(temp, this.SelectedAuthor.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteAuthors() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить этот жанр?", "Удаление жанра",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new AuthorDealer().Delete(GlobalAppDataContext.Instance, this.SelectedAuthor.Id);
                this.Update();
            }
        }

        public ICommand AddToAuthorsCommand  => new RelayCommand(this.AddToAuthors);
        public ICommand EditAuthorsCommand   => new RelayCommand(this.EditAuthors, _ => this.SelectedAuthor != null);
        public ICommand DeleteAuthorsCommand => new RelayCommand(this.DeleteAuthors, _ => this.SelectedAuthor != null);

        #endregion

        #region DEGREES

        public List<DegreeViewModel> Degrees  { get; set; }
        public DegreeViewModel SelectedDegree { get; set; } = null;

        public List<DegreeViewModel> GetDegrees() =>
            new DegreeDealer().Select(GlobalAppDataContext.Instance)
                            .Select(degrees => new DegreeViewModel(degrees))
                            .ToList();

        public void UpdateDegrees() => this.Degrees = this.GetDegrees();

        public void AddToDegrees() {
            var temp = new AddDegree();
            temp.DataContext = new AddDegreesViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditDegrees() {
            var temp = new AddDegree();
            temp.DataContext = new AddDegreesViewModel(temp, this.SelectedDegree.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteDegrees() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить этот уровень?", "Удаление уровня",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new DegreeDealer().Delete(GlobalAppDataContext.Instance, this.SelectedDegree.Id);
                this.Update();
            }
        }

        public ICommand AddToDegreesCommand  => new RelayCommand(this.AddToDegrees);
        public ICommand EditDegreesCommand   => new RelayCommand(this.EditDegrees, _ => this.SelectedDegree != null);
        public ICommand DeleteDegreesCommand => new RelayCommand(this.DeleteDegrees, _ => this.SelectedDegree != null);

        #endregion

        #region WORKERS

        public List<WorkerViewModel> Workers  { get; set; }
        public WorkerViewModel SelectedWorker { get; set; } = null;

        public List<WorkerViewModel> GetWorkers() =>
            new WorkerDealer().Select(GlobalAppDataContext.Instance)
                            .Select(worker => new WorkerViewModel(worker))
                            .ToList();

        public void UpdateWorkers() => this.Workers = this.GetWorkers();

        public void AddToWorkers() {
            var temp = new AddWorker();
            temp.DataContext = new AddWorkersViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditWorkers() {
            var temp = new AddWorker();
            temp.DataContext = new AddWorkersViewModel(temp, this.SelectedWorker.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteWorkers() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить этого работника?", "Удаление работника",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new WorkerDealer().Delete(GlobalAppDataContext.Instance, this.SelectedWorker.Id);
                this.Update();
            }
        }

        public ICommand AddToWorkersCommand  => new RelayCommand(this.AddToWorkers);
        public ICommand EditWorkersCommand   => new RelayCommand(this.EditWorkers, _ => this.SelectedWorker != null);
        public ICommand DeleteWorkersCommand => new RelayCommand(this.DeleteWorkers, _ => this.SelectedWorker != null);

        #endregion

        #region PUBLISHERS

        public List<PublisherViewModel> Publishers  { get; set; }
        public PublisherViewModel SelectedPublisher { get; set; } = null;

        public List<PublisherViewModel> GetPublishers() =>
            new PublisherDealer().Select(GlobalAppDataContext.Instance)
                .Select(publishers => new PublisherViewModel(publishers, GlobalAppDataContext.Instance)).ToList();

        public void UpdatePublishers() => this.Publishers = this.GetPublishers();

        public void AddToPublishers() {
            var temp = new AddPublisher();
            temp.DataContext = new AddPublisherViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditPublishers() {
            var temp = new AddPublisher();
            temp.DataContext = new AddPublisherViewModel(temp, this.SelectedPublisher.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeletePublishers() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить это издательство?", "Удаление издательства", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new PublisherDealer().Delete(GlobalAppDataContext.Instance, this.SelectedPublisher.Id);
                this.Update();
            }
        }

        public ICommand AddToPublishersCommand  => new RelayCommand(this.AddToPublishers);
        public ICommand EditPublishersCommand   => new RelayCommand(this.EditPublishers, _ => this.SelectedPublisher != null);
        public ICommand DeletePublishersCommand => new RelayCommand(this.DeletePublishers, _ => this.SelectedPublisher != null);

        #endregion

        #region BOOKS

        public List<BookViewModel> Books  { get; set; }
        public BookViewModel SelectedBook { get; set; } = null;

        public List<BookViewModel> GetBooks() =>
            new BookDealer().Select(GlobalAppDataContext.Instance)
                .Select(book => new BookViewModel(book, GlobalAppDataContext.Instance)).ToList();

        public void UpdateBooks() => this.Books = this.GetBooks();

        public void AddToBooks() {
            var temp = new AddBook();
            temp.DataContext = new AddBookViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditBooks() {
            var temp = new AddBook();
            temp.DataContext = new AddBookViewModel(temp, this.SelectedBook.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteBooks() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить эту книгу?", "Удаление книги", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new BookDealer().Delete(GlobalAppDataContext.Instance, this.SelectedBook.Id);
                this.Update();
            }
        }

        public ICommand AddToBooksCommand  => new RelayCommand(this.AddToBooks);
        public ICommand EditBooksCommand   => new RelayCommand(this.EditBooks, _ => this.SelectedBook != null);
        public ICommand DeleteBooksCommand => new RelayCommand(this.DeleteBooks, _ => this.SelectedBook != null);

        #endregion

        #region FACULTIES_AND_SPECIALTIES

        public List<FacultyAndSpecialtyViewModel> FacultyAndSpecialties  { get; set; }
        public FacultyAndSpecialtyViewModel SelectedFacultyAndSpecialty  { get; set; } = null;

        public List<FacultyAndSpecialtyViewModel> GetFacultyAndSpecialties() =>
            new FacultyAndSpecialtyDealer().Select(GlobalAppDataContext.Instance)
                .Select(fas => new FacultyAndSpecialtyViewModel(fas, GlobalAppDataContext.Instance)).ToList();

        public void UpdateFacultyAndSpecialties() => this.FacultyAndSpecialties = this.GetFacultyAndSpecialties();

        public void AddToFacultyAndSpecialties() {
            var temp = new AddFacultyAndSpecialty();
            temp.DataContext = new AddFacultyAndSpecialtyViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditFacultyAndSpecialties() {
            var temp = new AddFacultyAndSpecialty();
            temp.DataContext = new AddFacultyAndSpecialtyViewModel(temp, this.SelectedFacultyAndSpecialty.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteFacultyAndSpecialties() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить эту связь?", "Удаление связи", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new FacultyAndSpecialtyDealer().Delete(GlobalAppDataContext.Instance, this.SelectedFacultyAndSpecialty.Id);
                this.Update();
            }
        }

        public ICommand AddToFacultyAndSpecialtiesCommand  => new RelayCommand(this.AddToFacultyAndSpecialties);
        public ICommand EditFacultyAndSpecialtiesCommand   => new RelayCommand(this.EditFacultyAndSpecialties, _ => this.SelectedFacultyAndSpecialty != null);
        public ICommand DeleteFacultyAndSpecialtiesCommand => new RelayCommand(this.DeleteFacultyAndSpecialties, _ => this.SelectedFacultyAndSpecialty != null);

        #endregion

        #region FACULTIES_AND_SPECIALTIES_AND_CATHEDRA

        public List<FacultyAndSpecialtyAndCathedraViewModel> FacultyAndSpecialtyAndCathedras  { get; set; }
        public FacultyAndSpecialtyAndCathedraViewModel SelectedFacultyAndSpecialtyAndCathedra { get; set; } = null;

        public List<FacultyAndSpecialtyAndCathedraViewModel> GetFacultyAndSpecialtyAndCathedras() =>
            new FacultyAndSpecialtyAndCathedraDealer().Select(GlobalAppDataContext.Instance)
                .Select(fasac => new FacultyAndSpecialtyAndCathedraViewModel(fasac, GlobalAppDataContext.Instance)).ToList();

        public void UpdateFacultyAndSpecialtyAndCathedras() => this.FacultyAndSpecialtyAndCathedras = this.GetFacultyAndSpecialtyAndCathedras();

        public void AddToFacultyAndSpecialtyAndCathedras() {
            var temp = new AddFacultyAndSpecialtyAndCathedra();
            temp.DataContext = new AddFacultyAndSpecialtyAndCathedraViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditFacultyAndSpecialtyAndCathedras() {
            var temp = new AddFacultyAndSpecialtyAndCathedra();
            temp.DataContext = new AddFacultyAndSpecialtyAndCathedraViewModel(temp, this.SelectedFacultyAndSpecialtyAndCathedra.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteFacultyAndSpecialtyAndCathedras() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить эту связь?", "Удаление связи", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new FacultyAndSpecialtyAndCathedraDealer().Delete(GlobalAppDataContext.Instance, this.SelectedFacultyAndSpecialtyAndCathedra.Id);
                this.Update();
            }
        }

        public ICommand AddToFacultyAndSpecialtyAndCathedrasCommand  => new RelayCommand(this.AddToFacultyAndSpecialtyAndCathedras);
        public ICommand EditFacultyAndSpecialtyAndCathedrasCommand   => new RelayCommand(this.EditFacultyAndSpecialtyAndCathedras, _ => this.SelectedFacultyAndSpecialtyAndCathedra != null);
        public ICommand DeleteFacultyAndSpecialtyAndCathedrasCommand => new RelayCommand(this.DeleteFacultyAndSpecialtyAndCathedras, _ => this.SelectedFacultyAndSpecialtyAndCathedra != null);

        #endregion

        #region GROUPS

        public List<GroupViewModel> Groups { get; set; }
        public GroupViewModel SelectedGroup { get; set; } = null;

        public List<GroupViewModel> GetGroups() =>
            new GroupDealer().Select(GlobalAppDataContext.Instance)
                .Select(book => new GroupViewModel(book, GlobalAppDataContext.Instance)).ToList();

        public void UpdateGroups() => this.Groups = this.GetGroups();

        public void AddToGroups() {
            var temp = new AddGroup();
            temp.DataContext = new AddGroupViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditGroups() {
            var temp = new AddGroup();
            temp.DataContext = new AddGroupViewModel(temp, this.SelectedGroup.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteGroups() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить эту группу?", "Удаление группы", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new GroupDealer().Delete(GlobalAppDataContext.Instance, this.SelectedGroup.Id);
                this.Update();
            }
        }

        public ICommand AddToGroupsCommand  => new RelayCommand(this.AddToGroups);
        public ICommand EditGroupsCommand   => new RelayCommand(this.EditGroups, _ => this.SelectedGroup != null);
        public ICommand DeleteGroupsCommand => new RelayCommand(this.DeleteGroups, _ => this.SelectedGroup != null);

        #endregion

        #region STUDENTS

        public List<StudentViewModel> Students  { get; set; }
        public StudentViewModel SelectedStudent { get; set; } = null;

        public List<StudentViewModel> GetStudents() =>
            new StudentDealer().Select(GlobalAppDataContext.Instance)
                .Select(student => new StudentViewModel(student, GlobalAppDataContext.Instance)).ToList();

        public void UpdateStudents() => this.Students = this.GetStudents();

        public void AddToStudents() {
            var temp = new AddStudent();
            temp.DataContext = new AddStudentViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditStudents() {
            var temp = new AddStudent();
            temp.DataContext = new AddStudentViewModel(temp, this.SelectedStudent.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteStudents() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить этого студента?", "Удаление студента", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new StudentDealer().Delete(GlobalAppDataContext.Instance, this.SelectedStudent.Id);
                this.Update();
            }
        }

        public ICommand AddToStudentsCommand  => new RelayCommand(this.AddToStudents);
        public ICommand EditStudentsCommand   => new RelayCommand(this.EditStudents, _ => this.SelectedStudent != null);
        public ICommand DeleteStudentsCommand => new RelayCommand(this.DeleteStudents, _ => this.SelectedStudent != null);

        #endregion

        #region TEACHERS

        public List<TeacherViewModel> Teachers  { get; set; }
        public TeacherViewModel SelectedTeacher { get; set; } = null;

        public List<TeacherViewModel> GetTeachers() =>
            new TeacherDealer().Select(GlobalAppDataContext.Instance)
                .Select(teacher => new TeacherViewModel(teacher, GlobalAppDataContext.Instance)).ToList();

        public void UpdateTeachers() => this.Teachers = this.GetTeachers();

        public void AddToTeachers() {
            var temp = new AddTeacher();
            temp.DataContext = new AddTeacherViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditTeachers() {
            var temp = new AddTeacher();
            temp.DataContext = new AddTeacherViewModel(temp, this.SelectedTeacher.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteTeachers() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить этого преподавателя?", "Удаление преподавателя", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new TeacherDealer().Delete(GlobalAppDataContext.Instance, this.SelectedTeacher.Id);
                this.Update();
            }
        }

        public ICommand AddToTeachersCommand  => new RelayCommand(this.AddToTeachers);
        public ICommand EditTeachersCommand   => new RelayCommand(this.EditTeachers, _ => this.SelectedTeacher != null);
        public ICommand DeleteTeachersCommand => new RelayCommand(this.DeleteTeachers, _ => this.SelectedTeacher != null);

        #endregion

        #region CLIENT_CARDS

        public List<ClientCardViewModel> ClientCards  { get; set; }
        public ClientCardViewModel SelectedClientCard { get; set; } = null;

        public List<ClientCardViewModel> GetClientCards() =>
            new ClientCardDealer().Select(GlobalAppDataContext.Instance)
                .Select(card => new ClientCardViewModel(card, GlobalAppDataContext.Instance)).ToList();

        public void UpdateClientCards() => this.ClientCards = this.GetClientCards();

        public void AddToClientCards() {
            var temp = new AddClientCard();

            Style messageBoxStyle = new System.Windows.Style();
            messageBoxStyle.Setters.Add(new Setter(Xceed.Wpf.Toolkit.MessageBox.YesButtonContentProperty, "Преподаватель"));
            messageBoxStyle.Setters.Add(new Setter(Xceed.Wpf.Toolkit.MessageBox.NoButtonContentProperty, "Студент"));

            var result = Xceed.Wpf.Toolkit.MessageBox.Show
                    ("Какого клиента вы хотите добавить?", "Добавление клиента", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes, messageBoxStyle);

            ViewModel dataContext = result switch {
                MessageBoxResult.Yes => new AddTeacherClientCardViewModel(temp),
                MessageBoxResult.No  => new AddStudentClientCardViewModel(temp),
                _                    => null
            };

            if (dataContext is null) {
                return;
            }

            temp.DataContext = dataContext;
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditClientCards() {
            var temp = new AddClientCard();

            temp.DataContext = this.SelectedClientCard.ClientTypeStr == ClientType.Teacher.ClientTypeToString()
                ? new AddTeacherClientCardViewModel(temp, this.SelectedClientCard.Id)
                : new AddStudentClientCardViewModel(temp, this.SelectedClientCard.Id);

            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteClientCards() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить эту карту?", "Удаление карты", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new ClientCardDealer().Delete(GlobalAppDataContext.Instance, this.SelectedClientCard.Id);
                this.Update();
            }
        }

        public void CloseClientCards() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите закрыть эту карту?", "Закрытие карты", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new ClientCardDealer().DisactiveCard(GlobalAppDataContext.Instance, this.SelectedClientCard.Id);
                this.Update();
            }
        }

        public ICommand AddToClientCardsCommand  => new RelayCommand(this.AddToClientCards);
        public ICommand EditClientCardsCommand   => new RelayCommand(this.EditClientCards, _ => this.SelectedClientCard != null);
        public ICommand DeleteClientCardsCommand => new RelayCommand(this.DeleteClientCards, _ => this.SelectedClientCard != null);
        public ICommand CloseClientCardsCommand  => new RelayCommand(this.CloseClientCards, _ => this.SelectedClientCard != null);

        #endregion

        #region LIBRARY_TRANSACTIONS

        public List<LibraryTransactionViewModel> LibraryTransactions  { get; set; }
        public LibraryTransactionViewModel SelectedLibraryTransaction { get; set; } = null;

        public List<LibraryTransactionViewModel> GetLibraryTransactions() =>
            new LibraryTransactionDealer().Select(GlobalAppDataContext.Instance)
                .Select(trans => new LibraryTransactionViewModel(trans, GlobalAppDataContext.Instance)).ToList();

        public void UpdateLibraryTransactions() => this.LibraryTransactions = this.GetLibraryTransactions();

        public void AddToLibraryTransactions() {
            var temp = new AddLibraryTransaction();
            temp.DataContext = new AddLibraryTransactionViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void EditLibraryTransactions() {
            var temp = new AddLibraryTransaction();
            temp.DataContext = new AddLibraryTransactionViewModel(temp, this.SelectedLibraryTransaction.Id);
            if (temp.ShowDialog() is true) {
                this.Update();
            }
        }

        public void DeleteLibraryTransactions() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите удалить эту сделку?", "Удаление сделки", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new LibraryTransactionDealer().Delete(GlobalAppDataContext.Instance, this.SelectedLibraryTransaction.Id);
                this.Update();
            }
        }

        public void CloseLibraryTransactions() {
            if (Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены, что хотите закрыть эту сделку?", "Закрытие сделки", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new LibraryTransactionDealer().DisactiveTransaction(GlobalAppDataContext.Instance, this.SelectedLibraryTransaction.Id);
                this.Update();
            }
        }

        public ICommand AddToLibraryTransactionsCommand  => new RelayCommand(this.AddToLibraryTransactions);
        public ICommand EditLibraryTransactionsCommand   => new RelayCommand(this.EditLibraryTransactions, _ => this.SelectedLibraryTransaction != null);
        public ICommand DeleteLibraryTransactionsCommand => new RelayCommand(this.DeleteLibraryTransactions, _ => this.SelectedLibraryTransaction != null);
        public ICommand CloseLibraryTransactionsCommand  => new RelayCommand(this.CloseLibraryTransactions, _ => this.SelectedLibraryTransaction != null);

        #endregion

        public void Update() {
            this.UpdateCountries();
            this.UpdateCities();
            this.UpdateCathedras();
            this.UpdateFaculties();
            this.UpdateGenres();
            this.UpdateAuthors();
            this.UpdateDegrees();
            this.UpdateWorkers();
            this.UpdatePublishers();
            this.UpdateBooks();
            this.UpdateFacultyAndSpecialties();
            this.UpdateFacultyAndSpecialtyAndCathedras();
            this.UpdateSpecialties();
            this.UpdateGroups();
            this.UpdateStudents();
            this.UpdateTeachers();
            this.UpdateClientCards();
            this.UpdateLibraryTransactions();
        }

        public void Exit(Window admin) {
            var result = Xceed.Wpf.Toolkit.MessageBox.Show("Вы уверены что хотите выйти?", "Выход", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes) {
                var window = new LoginWindow();
                admin.Hide(); 
                window.ShowDialog();
                admin.Close();
            }
        }

        public string UpdateButtonText => "Обновить";
        public string AddButtonText    => "Добавить";
        public string DeleteButtonText => "Удалить";
        public string EditButtonText   => "Изменить";
        public string CloseButtonText  => "Закрыть";
        public string ExitButtonText   => "Выйти";

        public bool IsAdmin { get; } = false;

        public string Title => "БД \"Библиотека университета\"";

        public string SelectedTab { get; set; }

        public ICommand ExitCommand => new RelayParameterCommand(o => this.Exit(o as Window));

        public ICommand AddCommand {
            get {
                try {
                    return this.SelectedTab switch {
                        nameof(AppDataContext.Authors) => this.AddToAuthorsCommand,
                        nameof(AppDataContext.Books) => this.AddToBooksCommand,
                        nameof(AppDataContext.Cathedras) => this.AddToCathedrasCommand,
                        nameof(AppDataContext.Cities) => this.AddToCitiesCommand,
                        nameof(AppDataContext.Countries) => this.AddToCountriesCommand,
                        nameof(AppDataContext.Degrees) => this.AddToDegreesCommand,
                        nameof(AppDataContext.Faculties) => this.AddToFacultiesCommand,
                        nameof(AppDataContext.FacultyAndSpecialties) => this.AddToFacultyAndSpecialtiesCommand,
                        nameof(AppDataContext.FacultyAndSpecialtyAndCathedras) => this.AddToFacultyAndSpecialtyAndCathedrasCommand,
                        nameof(AppDataContext.Genres) => this.AddToGenresCommand,
                        nameof(AppDataContext.Groups) => this.AddToGroupsCommand,
                        nameof(AppDataContext.Publishers) => this.AddToPublishersCommand,
                        nameof(AppDataContext.ClientCards) => this.AddToClientCardsCommand,
                        nameof(AppDataContext.LibraryTransactions) => this.AddToLibraryTransactionsCommand,
                        nameof(AppDataContext.Students) => this.AddToStudentsCommand,
                        nameof(AppDataContext.Specialties) => this.AddToSpecialtiesCommand,
                        nameof(AppDataContext.Teachers) => this.AddToTeachersCommand,
                        nameof(AppDataContext.Workers) => this.AddToWorkersCommand,
                        _ => new RelayCommand(() => { }, _ => false)
                    };
                }
                catch(Exception) {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Ошибка при добавлении. Некорректные данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); ;
                    return null;
                }
            }
        }

        public ICommand EditCommand { 
            get {
                try {
                    return this.SelectedTab switch {
                        nameof(AppDataContext.Authors) => this.EditAuthorsCommand,
                        nameof(AppDataContext.Books) => this.EditBooksCommand,
                        nameof(AppDataContext.Cathedras) => this.EditCathedrasCommand,
                        nameof(AppDataContext.Cities) => this.EditCitiesCommand,
                        nameof(AppDataContext.Countries) => this.EditCountriesCommand,
                        nameof(AppDataContext.Degrees) => this.EditDegreesCommand,
                        nameof(AppDataContext.Faculties) => this.EditFacultiesCommand,
                        nameof(AppDataContext.FacultyAndSpecialties) => this.EditFacultyAndSpecialtiesCommand,
                        nameof(AppDataContext.FacultyAndSpecialtyAndCathedras) => this.EditFacultyAndSpecialtyAndCathedrasCommand,
                        nameof(AppDataContext.Genres) => this.EditGenresCommand,
                        nameof(AppDataContext.Groups) => this.EditGroupsCommand,
                        nameof(AppDataContext.Publishers) => this.EditPublishersCommand,
                        nameof(AppDataContext.ClientCards) => this.EditClientCardsCommand,
                        nameof(AppDataContext.LibraryTransactions) => this.EditLibraryTransactionsCommand,
                        nameof(AppDataContext.Students) => this.EditStudentsCommand,
                        nameof(AppDataContext.Specialties) => this.EditSpecialtiesCommand,
                        nameof(AppDataContext.Teachers) => this.EditTeachersCommand,
                        nameof(AppDataContext.Workers) => this.EditWorkersCommand,
                        _ => new RelayCommand(() => { }, _ => false)
                    };
                }
                catch(Exception) {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Ошибка при редактировании. Некорректные данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); ;
                    return null;
                }
            }
        }

        public ICommand DeleteCommand {
            get {
                try {
                    return this.SelectedTab switch {
                        nameof(AppDataContext.Authors) => this.DeleteAuthorsCommand,
                        nameof(AppDataContext.Books) => this.DeleteBooksCommand,
                        nameof(AppDataContext.Cathedras) => this.DeleteCathedrasCommand,
                        nameof(AppDataContext.Cities) => this.DeleteCitiesCommand,
                        nameof(AppDataContext.Countries) => this.DeleteCountriesCommand,
                        nameof(AppDataContext.Degrees) => this.DeleteDegreesCommand,
                        nameof(AppDataContext.Faculties) => this.DeleteFacultiesCommand,
                        nameof(AppDataContext.FacultyAndSpecialties) => this.DeleteFacultyAndSpecialtiesCommand,
                        nameof(AppDataContext.FacultyAndSpecialtyAndCathedras) => this.DeleteFacultyAndSpecialtyAndCathedrasCommand,
                        nameof(AppDataContext.Genres) => this.DeleteGenresCommand,
                        nameof(AppDataContext.Groups) => this.DeleteGroupsCommand,
                        nameof(AppDataContext.Publishers) => this.DeletePublishersCommand,
                        nameof(AppDataContext.ClientCards) => this.DeleteClientCardsCommand,
                        nameof(AppDataContext.LibraryTransactions) => this.DeleteLibraryTransactionsCommand,
                        nameof(AppDataContext.Students) => this.DeleteStudentsCommand,
                        nameof(AppDataContext.Specialties) => this.DeleteSpecialtiesCommand,
                        nameof(AppDataContext.Teachers) => this.DeleteTeachersCommand,
                        nameof(AppDataContext.Workers) => this.DeleteWorkersCommand,
                        _ => new RelayCommand(() => { }, _ => false)
                    };
                }
                catch(Exception) {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Ошибка при удалении. Данные невозможно удалить или они на что-то ссылаются.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); ;
                    return null;
                }
            }
        }

        public ICommand UpdateCommand => new RelayCommand(this.Update, _ => this.SelectedTab != null);

        public ICommand CloseCommand { 
            get {
                try {
                    return this.SelectedTab switch {
                        nameof(AppDataContext.ClientCards) => this.CloseClientCardsCommand,
                        nameof(AppDataContext.LibraryTransactions) => this.CloseLibraryTransactionsCommand,
                        _ => new RelayCommand(() => { }, _ => false)
                    };
                }
                catch(Exception) {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Ошибка при закрытии. Некорректные данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); ;
                    return null;
                }
            }
        }

        public static string AuthorsTableName                               { get; set; } = nameof(AppDataContext.Authors);
        public static string BooksTableName                                 { get; set; } = nameof(AppDataContext.Books);
        public static string CathedrasTableName                             { get; set; } = nameof(AppDataContext.Cathedras);
        public static string CitiesTableName                                { get; set; } = nameof(AppDataContext.Cities);
        public static string ClientCardsTableName                           { get; set; } = nameof(AppDataContext.ClientCards);
        public static string CountriesTableName                             { get; set; } = nameof(AppDataContext.Countries);
        public static string DegreesTableName                               { get; set; } = nameof(AppDataContext.Degrees);
        public static string FacultiesTableName                             { get; set; } = nameof(AppDataContext.Faculties);
        public static string FacultiesAndSpecialtiesTableName               { get; set; } = nameof(AppDataContext.FacultyAndSpecialties);
        public static string FacultiesAndSpecialtiesAndCathedrasTableName   { get; set; } = nameof(AppDataContext.FacultyAndSpecialtyAndCathedras);
        public static string GenresTableName                                { get; set; } = nameof(AppDataContext.Genres);
        public static string GroupsTableName                                { get; set; } = nameof(AppDataContext.Groups);
        public static string LibraryTransactionsTableName                   { get; set; } = nameof(AppDataContext.LibraryTransactions);
        public static string PublishersTableName                            { get; set; } = nameof(AppDataContext.Publishers);
        public static string SpecialtiesTableName                           { get; set; } = nameof(AppDataContext.Specialties);
        public static string StudentsTableName                              { get; set; } = nameof(AppDataContext.Students);
        public static string TeachersTableName                              { get; set; } = nameof(AppDataContext.Teachers);
        public static string WorkersTableName                               { get; set; } = nameof(AppDataContext.Workers);
    }
}
