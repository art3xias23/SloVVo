﻿using System;
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
        private readonly IUnitOfWork _uow;
        public ICommand AddAuthorCommand { get; set; }
        public string AuthorName { get; set; }

        public AddAuthorViewModel(IUnitOfWork uow)
        {
            _uow = uow;
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
