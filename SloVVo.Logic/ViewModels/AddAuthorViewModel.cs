using System;
using System.Linq;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class AddAuthorViewModel : ObservableObject
    {
        private UnitOfWork UnitOfWork;
        public ICommand AddAuthorCommand { get; set; }
        public string AuthorName { get; set; }

        public AddAuthorViewModel()
        {
            AddAuthorCommand = new RelayCommandEmpty(AddAuthor);
        }

        private void AddAuthor()
        {
            AddAuthorItem();

            EventAggregator.UpdateAuthorCollection();

            ViewEventHandler.RaiseShowAddBookView();
        }

        private void AddAuthorItem()
        {
            var recordExists = UnitOfWork.AuthorRepository.GetAll()
                .Any(x => x.AuthorName.ToLower() == AuthorName.ToLower());
            if (!recordExists)
            {
                UnitOfWork.AuthorRepository.Add(new Author()
                {
                    AuthorName = AuthorName
                });

                UnitOfWork.SaveChanges();
            }
        }
    }
}
