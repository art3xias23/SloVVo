using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NLog;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class BooksViewModel : ObservableObject
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork _uow;
        public ICommand LoadBookCollectionCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditRowCommand { get; set; }

        private ObservableCollection<Book> _booksList;

        private Book _selectedItem;

        public Book SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }
        public ObservableCollection<Book> BooksList
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
            EventAggregator.BookUpdateTransmitted += BookUpdate;

            LoadBookCollectionCommand = new RelayCommandEmpty(LoadBooksCollection);
            SearchCommand = new RelayCommandEmpty(Search);
            EditRowCommand = new RelayCommandEmpty(EditRow);

        }

        private void EditRow()
        {
            if (SelectedItem != null)
                ViewEventHandler.RaiseShowBookEvent(SelectedItem);
        }

        private void Search()
        {
            BooksList = new ObservableCollection<Book>(GetBooksList());
        }

        private List<Book> GetBooksList()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                if (IsBookChecked)
                {
                    var books = _uow.BookRepository.GetAll();

                    return books.Where(x => x.BookName != null && x.BookName.ToLower().Contains(SearchText.ToLower())).ToList();
                }
                else if (IsAuthorChecked)
                {
                    var books = _uow.BookRepository.GetAll();
                    return books.Where(x => x.Author != null && x.Author.AuthorName.ToLower().Contains(SearchText.ToLower()))?.ToList();
                }
                else
                {
                    var books = _uow.BookRepository.GetAll();
                    return books.ToList();
                }
            }

            return _uow.BookRepository.GetAll().ToList();
        }

        private void BookUpdate()
        {
            LoadBooksCollection();
        }

        private void LoadBooksCollection()
        {
            _logger.Trace("Loading Books");
            BooksList = new ObservableCollection<Book>(_uow.BookRepository.GetAllQueryable().ToList());
            SetTotalItems("Брой Книги", BooksList.Count);
        }

        private void SetTotalItems(string text, int count)
        {
            TotalItemsText = text;
            TotalItemsCount = count;
        }
    }
}
