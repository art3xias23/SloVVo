using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Caching;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class SearchWindowViewModel : ObservableObject
    {
        private UnitOfWork _uow;
        private ObservableCollection<Book> _booksList;
        public ObservableCollection<Book> BooksList
        {
            get => _booksList;
            set { _booksList = value; OnPropertyChanged(nameof(BooksList)); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        private bool _isBookChecked;

        public bool IsBookChecked
        {
            get { return _isBookChecked; }
            set { _isBookChecked = value; OnPropertyChanged(nameof(IsBookChecked)); }
        }

        private bool _isAuthorChecked;

        public bool IsAuthorChecked
        {
            get { return _isAuthorChecked; }
            set { _isAuthorChecked = value; OnPropertyChanged(nameof(IsAuthorChecked)); }
        }

        public ICommand LoadBookCollectionCommand { get; set; }
        public ICommand ShowUploadScreenCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        private int _totalBooks;
        public int TotalBooks
        {
            get { return _totalBooks; }
            set { _totalBooks = value; OnPropertyChanged(nameof(TotalBooks)); }
        }


        public SearchWindowViewModel()
        {
            EventAggregator.BookUpdateTransmitted += BookUpdate;
            LoadBookCollectionCommand = new RelayCommandEmpty(LoadBookCollection);
            ShowUploadScreenCommand = new RelayCommandEmpty(ShowUplaodScreen);
            SearchCommand = new RelayCommand(Search);
            BooksList = new ObservableCollection<Book>();

            _uow = new UnitOfWork();
        }

        private void Search(object obj)
        {
            BooksList=  new ObservableCollection<Book>(GetBooksList());
        }

        private List<Book> GetBooksList()
        {
            var booksList = new List<Book>();

            if (IsBookChecked)
            {
                return booksList = _uow.BookRepository.GetAll()
                    .Where(x => x.BookName.ToLower().Contains(SearchText.ToLower())).ToList();
            }
            else if (IsAuthorChecked)
            {
                return booksList = _uow.BookRepository.GetAll()
                    .Where(x => x.Author.AuthorName.ToLower().Contains(SearchText.ToLower())).ToList();
            }

            return booksList;
        }

        private void BookUpdate()
        {
            LoadBookCollection();
            SetTotalBookCount();
        }

        private void ShowUplaodScreen()
        {
            ViewEventHandler.RaiseShowUploadView();
        }

        private void LoadBookCollection()
        {
            //ObservableCollection<Book> books = new ObservableCollection<Book>(Cache.DefaultCache["AllBooks"] as List<Book>);
            //BooksList = books?.Count > 0
            //    ? books
            //    : new ObservableCollection<Book>(_uow.BookRepository.GetAllQueryable());

            BooksList = new ObservableCollection<Book>(_uow.BookRepository.GetAllQueryable());
            SetTotalBookCount();
        }

        private void SetTotalBookCount()
        {
            TotalBooks = BooksList.Count;
        }
    }
}
