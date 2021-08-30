using System;
using System.Linq;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class AuthorViewModel : ObservableObject
    {
        private UnitOfWork _uow;
        public ICommand AddAuthorCommand { get; set; }
        public string AuthorName { get; set; }

        public AuthorViewModel()
        {
            _uow = new UnitOfWork();
            AddAuthorCommand = new RelayCommandEmpty(AddAuthor);
        }

        private void AddAuthor()
        {
            AddAuthorItem();

            EventAggregator.UpdateAuthorCollection();

            ViewEventHandler.RaiseCloseAddAuthorView();
        }

        private void AddAuthorItem()
        {
            var recordExists = _uow.AuthorRepository.GetAll()
                .Any(x => x.AuthorName.ToLower() == AuthorName.ToLower());
            if (!recordExists)
            {
                _uow.AuthorRepository.Add(new Author()
                {
                    AuthorName = AuthorName
                });

                _uow.SaveChanges();
            }
        }
    }
}
