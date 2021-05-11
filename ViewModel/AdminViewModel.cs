using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PropertyChanged;
using ConsoleDBTest.ViewModels;
using ConsoleDBTest.Dealer;
using Database4.Data;
using Database4.View;
using System.Windows;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AdminViewModel : ViewModel {
        public AdminViewModel() {
            this.UpdateAll();
        }

        public void UpdateAll() {
            this.UpdateCountries();
            this.UpdateCities();
            this.UpdateCathedras();
            this.UpdateFaculties();
            this.UpdateGenres();
            this.UpdateAuthors();
            this.UpdateDegrees();
            this.UpdateWorkers();
            this.UpdatePublishers();
            //DO NOT FORGET TO UPDATE THIS
        }

        #region COUNTRIES

        public List<CountryViewModel> Countries { get; set; }
        public CountryViewModel SelectedCountry { get; set; }

        public List<CountryViewModel> GetCountries() =>
            new CountryDealer().Select(GlobalAppDataContext.Instance).ToList().Select(c => new CountryViewModel(c)).ToList();

        public void UpdateCountries() => this.Countries = this.GetCountries();

        public void AddToCountries() {
            var temp = new AddCountry();
            temp.DataContext = new AddCountryViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void EditCountries() {
            var temp = new AddCountry();
            temp.DataContext = new AddCountryViewModel(temp, this.SelectedCountry.Id);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void DeleteCountries() {
            if (MessageBox.Show("Вы уверены, что хотите удалить эту страну?", "Удаление страны", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new CountryDealer().Delete(GlobalAppDataContext.Instance, this.SelectedCountry.Id);
                this.UpdateAll();
            }
        }

        public ICommand AddToCountriesCommand  => new RelayCommand(this.AddToCountries);
        public ICommand EditCountriesCommand   => new RelayCommand(this.EditCountries, _ => this.SelectedCountry != null);
        public ICommand DeleteCountriesCommand => new RelayCommand(this.DeleteCountries, _ => this.SelectedCountry != null);

        #endregion

        #region CITIES

        public List<CityViewModel> Cities { get; set; }
        public CityViewModel SelectedCity { get; set; }

        public List<CityViewModel> GetCities() =>
            new CityDealer().Select(GlobalAppDataContext.Instance).Select(city => new CityViewModel(city, GlobalAppDataContext.Instance)).ToList();

        public void UpdateCities() => this.Cities = this.GetCities();

        public void AddToCities() {
            var temp = new AddCity();
            temp.DataContext = new AddCityViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void EditCities() {
            var temp = new AddCity();
            temp.DataContext = new AddCityViewModel(temp, this.SelectedCity.Id);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void DeleteCities() {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот город?", "Удаление города", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new CityDealer().Delete(GlobalAppDataContext.Instance, this.SelectedCity.Id);
                this.UpdateAll();
            }
        }

        public ICommand AddToCitiesCommand  => new RelayCommand(this.AddToCities);
        public ICommand EditCitiesCommand   => new RelayCommand(this.EditCities, _ => this.SelectedCity != null);
        public ICommand DeleteCitiesCommand => new RelayCommand(this.DeleteCities, _ => this.SelectedCity != null);

        #endregion

        #region SPECIALTIES

        public List<SpecialtyViewModel> Specialties       { get; set; }
        public SpecialtyViewModel       SelectedSpecialty { get; set; }

        public List<SpecialtyViewModel> GetSpecialties() =>
            new SpecialtyDealer().Select(GlobalAppDataContext.Instance)
                            .Select(spec => new SpecialtyViewModel(spec))
                            .ToList();

        public void UpdateSpecialties() => this.Specialties = this.GetSpecialties();

        public void AddToSpecialties() {
            var temp = new AddSpecialty();
            temp.DataContext = new AddSpecialtyViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void EditSpecialties() {
            var temp = new AddSpecialty();
            temp.DataContext = new AddSpecialtyViewModel(temp, this.SelectedSpecialty.Id);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void DeleteSpecialties() {
            if (MessageBox.Show("Вы уверены, что хотите удалить эту специальность?", "Удаление специальности",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new CityDealer().Delete(GlobalAppDataContext.Instance, this.SelectedCity.Id);
                this.UpdateAll();
            }
        }

        public ICommand AddToSpecialtiesCommand  => new RelayCommand(this.AddToSpecialties);
        public ICommand EditSpecialtiesCommand   => new RelayCommand(this.EditSpecialties,   _ => this.SelectedSpecialty != null);
        public ICommand DeleteSpecialtiesCommand => new RelayCommand(this.DeleteSpecialties, _ => this.SelectedSpecialty != null);

        #endregion

        #region CATHEDRAS

        public List<CathedraViewModel> Cathedras  { get; set; }
        public CathedraViewModel SelectedCathedra { get; set; }

        public List<CathedraViewModel> GetCathedras() =>
            new CathedraDealer().Select(GlobalAppDataContext.Instance)
                            .Select(cathedra => new CathedraViewModel(cathedra))
                            .ToList();

        public void UpdateCathedras() => this.Cathedras = this.GetCathedras();

        public void AddToCathedras() {
            var temp = new AddCathedra();
            temp.DataContext = new AddCathedraViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void EditCathedras() {
            var temp = new AddCathedra();
            temp.DataContext = new AddCathedraViewModel(temp, this.SelectedCathedra.Id);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void DeleteCathedras() {
            if (MessageBox.Show("Вы уверены, что хотите удалить эту кафедру?", "Удаление кафедры",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new CathedraDealer().Delete(GlobalAppDataContext.Instance, this.SelectedCathedra.Id);
                this.UpdateAll();
            }
        }

        public ICommand AddToCathedrasCommand  => new RelayCommand(this.AddToCathedras);
        public ICommand EditCathedrasCommand   => new RelayCommand(this.EditCathedras, _ => this.SelectedCathedra != null);
        public ICommand DeleteCathedrasCommand => new RelayCommand(this.DeleteCathedras, _ => this.SelectedCathedra != null);

        #endregion

        #region FACULTIES

        public List<FacultyViewModel> Faculties { get; set; }
        public FacultyViewModel SelectedFaculty { get; set; }

        public List<FacultyViewModel> GetFaculties() =>
            new FacultyDealer().Select(GlobalAppDataContext.Instance)
                            .Select(faculty => new FacultyViewModel(faculty))
                            .ToList();

        public void UpdateFaculties() => this.Faculties = this.GetFaculties();

        public void AddToFaculties() {
            var temp = new AddFaculty();
            temp.DataContext = new AddFacultiesViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void EditFaculties() {
            var temp = new AddFaculty();
            temp.DataContext = new AddFacultiesViewModel(temp, this.SelectedFaculty.Id);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void DeleteFaculties() {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот факультет?", "Удаление факультета",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new FacultyDealer().Delete(GlobalAppDataContext.Instance, this.SelectedFaculty.Id);
                this.UpdateAll();
            }
        }

        public ICommand AddToFacultiesCommand  => new RelayCommand(this.AddToFaculties);
        public ICommand EditFacultiesCommand   => new RelayCommand(this.EditFaculties, _ => this.SelectedFaculty != null);
        public ICommand DeleteFacultiesCommand => new RelayCommand(this.DeleteFaculties, _ => this.SelectedFaculty != null);

        #endregion

        #region GENRES

        public List<GenreViewModel> Genres  { get; set; }
        public GenreViewModel SelectedGenre { get; set; }

        public List<GenreViewModel> GetGenres() =>
            new GenreDealer().Select(GlobalAppDataContext.Instance)
                            .Select(genre => new GenreViewModel(genre))
                            .ToList();

        public void UpdateGenres() => this.Genres = this.GetGenres();

        public void AddToGenres() {
            var temp = new AddGenre();
            temp.DataContext = new AddGenresViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void EditGenres() {
            var temp = new AddGenre();
            temp.DataContext = new AddGenresViewModel(temp, this.SelectedGenre.Id);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void DeleteGenres() {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот жанр?", "Удаление жанра",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new GenreDealer().Delete(GlobalAppDataContext.Instance, this.SelectedGenre.Id);
                this.UpdateAll();
            }
        }

        public ICommand AddToGenresCommand  => new RelayCommand(this.AddToGenres);
        public ICommand EditGenresCommand   => new RelayCommand(this.EditGenres, _ => this.SelectedGenre != null);
        public ICommand DeleteGenresCommand => new RelayCommand(this.DeleteGenres, _ => this.SelectedGenre != null);

        #endregion

        #region AUTHORS

        public List<AuthorViewModel> Authors  { get; set; }
        public AuthorViewModel SelectedAuthor { get; set; }

        public List<AuthorViewModel> GetAuthors() =>
            new AuthorDealer().Select(GlobalAppDataContext.Instance)
                            .Select(author => new AuthorViewModel(author))
                            .ToList();

        public void UpdateAuthors() => this.Authors = this.GetAuthors();

        public void AddToAuthors() {
            var temp = new AddAuthor();
            temp.DataContext = new AddAuthorsViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void EditAuthors() {
            var temp = new AddAuthor();
            temp.DataContext = new AddAuthorsViewModel(temp, this.SelectedAuthor.Id);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void DeleteAuthors() {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот жанр?", "Удаление жанра",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new AuthorDealer().Delete(GlobalAppDataContext.Instance, this.SelectedAuthor.Id);
                this.UpdateAll();
            }
        }

        public ICommand AddToAuthorsCommand  => new RelayCommand(this.AddToAuthors);
        public ICommand EditAuthorsCommand   => new RelayCommand(this.EditAuthors, _ => this.SelectedAuthor != null);
        public ICommand DeleteAuthorsCommand => new RelayCommand(this.DeleteAuthors, _ => this.SelectedAuthor != null);

        #endregion

        #region DEGREES

        public List<DegreeViewModel> Degrees  { get; set; }
        public DegreeViewModel SelectedDegree { get; set; }

        public List<DegreeViewModel> GetDegrees() =>
            new DegreeDealer().Select(GlobalAppDataContext.Instance)
                            .Select(degrees => new DegreeViewModel(degrees))
                            .ToList();

        public void UpdateDegrees() => this.Degrees = this.GetDegrees();

        public void AddToDegrees() {
            var temp = new AddDegree();
            temp.DataContext = new AddDegreesViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void EditDegrees() {
            var temp = new AddDegree();
            temp.DataContext = new AddDegreesViewModel(temp, this.SelectedDegree.Id);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void DeleteDegrees() {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот уровень?", "Удаление уровня",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new DegreeDealer().Delete(GlobalAppDataContext.Instance, this.SelectedDegree.Id);
                this.UpdateAll();
            }
        }

        public ICommand AddToDegreesCommand  => new RelayCommand(this.AddToDegrees);
        public ICommand EditDegreesCommand   => new RelayCommand(this.EditDegrees, _ => this.SelectedDegree != null);
        public ICommand DeleteDegreesCommand => new RelayCommand(this.DeleteDegrees, _ => this.SelectedDegree != null);

        #endregion

        #region WORKERS

        public List<WorkerViewModel> Workers  { get; set; }
        public WorkerViewModel SelectedWorker { get; set; }

        public List<WorkerViewModel> GetWorkers() =>
            new WorkerDealer().Select(GlobalAppDataContext.Instance)
                            .Select(worker => new WorkerViewModel(worker))
                            .ToList();

        public void UpdateWorkers() => this.Workers = this.GetWorkers();

        public void AddToWorkers() {
            var temp = new AddWorker();
            temp.DataContext = new AddWorkersViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void EditWorkers() {
            var temp = new AddWorker();
            temp.DataContext = new AddWorkersViewModel(temp, this.SelectedWorker.Id);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void DeleteWorkers() {
            if (MessageBox.Show("Вы уверены, что хотите удалить этого работника?", "Удаление работника",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new WorkerDealer().Delete(GlobalAppDataContext.Instance, this.SelectedWorker.Id);
                this.UpdateAll();
            }
        }

        public ICommand AddToWorkersCommand  => new RelayCommand(this.AddToWorkers);
        public ICommand EditWorkersCommand   => new RelayCommand(this.EditWorkers, _ => this.SelectedWorker != null);
        public ICommand DeleteWorkersCommand => new RelayCommand(this.DeleteWorkers, _ => this.SelectedWorker != null);

        #endregion

        #region PUBLISHERS

        public List<PublisherViewModel> Publishers  { get; set; }
        public PublisherViewModel SelectedPublisher { get; set; }

        public List<PublisherViewModel> GetPublishers() =>
            new PublisherDealer().Select(GlobalAppDataContext.Instance)
                .Select(publishers => new PublisherViewModel(publishers, GlobalAppDataContext.Instance)).ToList();

        public void UpdatePublishers() => this.Publishers = this.GetPublishers();

        public void AddToPublishers() {
            var temp = new AddPublisher();
            temp.DataContext = new AddPublisherViewModel(temp);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void EditPublishers() {
            var temp = new AddPublisher();
            temp.DataContext = new AddPublisherViewModel(temp, this.SelectedPublisher.Id);
            if (temp.ShowDialog() is true) {
                this.UpdateAll();
            }
        }

        public void DeletePublishers() {
            if (MessageBox.Show("Вы уверены, что хотите удалить это издательство?", "Удаление издательства", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) is MessageBoxResult.Yes) {
                new PublisherDealer().Delete(GlobalAppDataContext.Instance, this.SelectedPublisher.Id);
                this.UpdateAll();
            }
        }

        public ICommand AddToPublishersCommand  => new RelayCommand(this.AddToPublishers);
        public ICommand EditPublishersCommand   => new RelayCommand(this.EditPublishers, _ => this.SelectedPublisher != null);
        public ICommand DeletePublishersCommand => new RelayCommand(this.DeletePublishers, _ => this.SelectedPublisher != null);

        #endregion

        //public List<Book> Books                                                     { get; set; }
        //public List<ClientCard> ClientCards                                         { get; set; }
        //public List<FacultyAndSpecialty> FacultyAndSpecialties                      { get; set; }
        //public List<FacultyAndSpecialtyAndCathedra> FacultyAndSpecialtyAndCathedras { get; set; }
        //public List<Group> Groups                                                   { get; set; }
        //public List<LibraryTransaction> LibraryTransactions                         { get; set; }
        //public List<Student> Students                                               { get; set; }
        //public List<Teacher> Teachers                                               { get; set; }

        public string SelectedTab { get; set; } 

        public string UpdateButtonText => "Обновить";
        public string AddButtonText    => "Добавить";
        public string DeleteButtonText => "Удалить";
        public string EditButtonText   => "Редактировать";

        public string Title => "БД \"Библиотека университета\"";

        //public ICommand temp => this.SelectedTab switch {
        //    nameof(AppDataContext.Countries) => this.AddToCountriesCommand,
        //    _ => new RelayCommand(() => MessageBox.Show("FUCK!"))
        //};

        public ICommand AddCommand    => this.AddToPublishersCommand;
        public ICommand EditCommand   => this.EditPublishersCommand;
        public ICommand DeleteCommand => this.DeletePublishersCommand;
        public ICommand UpdateCommand => new RelayCommand(this.UpdateAll);
    }
}
