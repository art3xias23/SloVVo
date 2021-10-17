using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using NLog;
using NLog.LayoutRenderers.Wrappers;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;
using SloVVo.Logic.ObservableModels;
using SloVVo.Logic.PdfLogic;
using Document = Mono.Cecil.Cil.Document;

namespace SloVVo.Logic.ViewModels
{
    public class BooksViewModel : ObservableObject
    {
        private const string _pdfDirectory = @"C:\PdfDocuments";
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork _uow;
        private readonly IPdf<ObservableBook> _ipdf;
        public ICommand LoadBookCollectionCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditRowCommand { get; set; }
        public ICommand PrintCommand { get; set; }

        private ObservableCollection<ObservableBook> _booksList;

        private ObservableBook _selectedItem;

        public ObservableBook SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }
        public ObservableCollection<ObservableBook> BooksList
        {
            get => _booksList;
            set { _booksList = value; OnPropertyChanged(nameof(BooksList)); }
        }


        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        private bool _isBookChecked;

        public bool IsBookChecked
        {
            get => _isBookChecked;
            set { _isBookChecked = value; OnPropertyChanged(nameof(IsBookChecked)); }
        }

        private bool _isAuthorChecked;

        public bool IsAuthorChecked
        {
            get => _isAuthorChecked;
            set { _isAuthorChecked = value; OnPropertyChanged(nameof(IsAuthorChecked)); }
        }

        private int _totalItemsCount;

        public int TotalItemsCount
        {
            get => _totalItemsCount;
            set
            {
                _totalItemsCount = value; OnPropertyChanged(nameof(TotalItemsCount));
            }
        }

        private string _totalItemsText;
        public string TotalItemsText
        {
            get => _totalItemsText;
            set
            {
                _totalItemsText = value; OnPropertyChanged(nameof(TotalItemsText));
            }
        }


        public BooksViewModel(IUnitOfWork uow)
        {
            _uow = uow;
            _ipdf = new Pdf<ObservableBook>();
            BooksList = new ObservableCollection<ObservableBook>();

            LoadBookCollectionCommand = new RelayCommandEmpty(LoadBooksCollection);
            SearchCommand = new RelayCommandEmpty(Search);
            EditRowCommand = new RelayCommandEmpty(EditRow);
            PrintCommand = new RelayCommandEmpty(GeneratePdf);

        }

        private void GeneratePdf()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            _ipdf.CreateDirectoryIfNotExist(_pdfDirectory);
            var fileCount = _ipdf.GetCountOfDirectoryItems(_pdfDirectory);
            var fileName = $@"{_pdfDirectory}\PdfDocument_{fileCount}_{DateTime.Now.ToString("yymmdd")}.pdf";
            _ipdf.ExportToPdf(BooksList.ToList(), fileName);
            _ipdf.OpenDocument(fileName);
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void EditRow()
        {
            if (SelectedItem != null)
                ViewEventHandler.RaiseShowBookEvent(MapToBook(SelectedItem));
        }

        private async void Search()
        {
            await SetBooksList();
        }

        public IEnumerable<ObservableBook> MapToObservables(IEnumerable<Book> books)
        {
            var returnEnumerable = new ObservableCollection<ObservableBook>();

            foreach (var book in books)
            {
                var observableBook = new ObservableBook();
                observableBook.LocationId = book.LocationId;
                observableBook.BiblioId = book.BiblioId;
                observableBook.BookId = book.BiblioId;
                observableBook.ShelfId = book.ShelfId;
                observableBook.AuthorName = book.AuthorName;
                observableBook.IsTaken = book.IsTaken;
                observableBook.Location = book.Location;
                returnEnumerable.Add(observableBook);
            }

            return returnEnumerable;
        }

        public ObservableBook MapToObservable(Book book)
        {
            var observableBook = new ObservableBook();
            observableBook.LocationId = book.LocationId;
            observableBook.BiblioId = book.BiblioId;
            observableBook.BookId = book.BiblioId;
            observableBook.ShelfId = book.ShelfId;
            observableBook.AuthorName = book.AuthorName;
            observableBook.IsTaken = book.IsTaken;
            observableBook.Location = book.Location;

            return observableBook;
        }

        public Book MapToBook(ObservableBook book)
        {
            var observableBook = new Book();
            observableBook.LocationId = book.LocationId;
            observableBook.BiblioId = book.BiblioId;
            observableBook.BookId = book.BiblioId;
            observableBook.ShelfId = book.ShelfId;
            observableBook.AuthorName = book.AuthorName;
            observableBook.IsTaken = book.IsTaken;
            observableBook.Location = book.Location;

            return observableBook;
        }

        public IEnumerable<Book> MapToBooks(IEnumerable<ObservableBook> books)
        {
            var returnEnumerable = new ObservableCollection<Book>();

            foreach (var book in books)
            {
                var observableBook = new Book();
                observableBook.LocationId = book.LocationId;
                observableBook.BiblioId = book.BiblioId;
                observableBook.BookId = book.BiblioId;
                observableBook.ShelfId = book.ShelfId;
                observableBook.AuthorName = book.AuthorName;
                observableBook.IsTaken = book.IsTaken;
                observableBook.Location = book.Location;
                returnEnumerable.Add(observableBook);
            }

            return returnEnumerable;
        }
        private async Task SetBooksList()
        {
            var books = await _uow.BookRepository.GetAllEnumerableAsync();
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                if (IsBookChecked)
                {
                     books = books.Where(x => x.BookName != null && x.BookName.ToLower().Contains(SearchText.ToLower())).ToList();
                }
                else if (IsAuthorChecked)
                {
                     books = books.Where(x => x.AuthorName != null && x.AuthorName.ToLower().Contains(SearchText.ToLower()))?.ToList();
                }
            }

            BooksList =  new ObservableCollection<ObservableBook>(MapToObservables(books));
        }

        public async Task BookUpdate()
        {
            _logger.Trace("Started update book collection");
            await SetBooksList();
            _logger.Trace("Finished update book collection");
        }

        private void LoadBooksCollection()
        {
            try
            {
                //BooksList = new ObservableCollection<Book>(_uow.BookRepository.GetAllQueryable());
                Task.Run(BookUpdate); 
                _logger.Trace("Finished Loading Books");
                SetTotalItems("Брой Книги", BooksList.Count);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        private void SetTotalItems(string text, int count)
        {
            TotalItemsText = text;
            TotalItemsCount = count;
        }
    }
}
