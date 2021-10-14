using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SloVVo.Data.Models;
using SloVVo.Logic.ViewModels;

namespace SloVVo.Logic.ObservableModels
{
    public class ObservableBook : ObservableObject
    {
        private int _locationId;
        public int LocationId
        {
            get { return _locationId; }
            set
            {
                _locationId = value; OnPropertyChanged(nameof(LocationId));
            }
        }

        private int _biblioId;

        public int BiblioId
        {
            get => _biblioId;
            set
            {
                _biblioId = value;
                OnPropertyChanged(nameof(BiblioId));
            }
        }

        private int _shelfId { get; set; }

        public int ShelfId
        {
            get => _shelfId;
            set
            {
                _shelfId = value;
                OnPropertyChanged(nameof(ShelfId));
            }
        }

        private int _bookId;

        public int BookId
        {
            get => _bookId;
            set { _bookId = value; OnPropertyChanged(nameof(BookId)); }
        }

        private string _bookName;
        public string BookName
        {
            get => _bookName;
            set { _bookName = value; OnPropertyChanged(nameof(BookName)); }
        }

        private string _authorName;

        public string AuthorName
        {
            get => _authorName;
            set { _authorName = value; OnPropertyChanged(nameof(AuthorName)); }
        }

        private bool _isTaken;
        public bool IsTaken
        {
            get => _isTaken;
            set
            {
                _isTaken = value;
                OnPropertyChanged(nameof(IsTaken));
            }
        }

        public virtual Location Location { get; set; }
        public virtual ICollection<UserBooks> UserBooks { get; set; }
    }
}
