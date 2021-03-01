using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SloVVo.Models.Data;
using SloVVo.ViewModels.Command;

namespace SloVVo.ViewModels.ViewModels
{
    public class UploadBookViewModel
    {
        public ICommand AddContentCommand { get; set; }
        public ICommand AddAuthorCommand { get; set; }
        public Book Book { get; set; }
        public UploadBookViewModel()
        {
            Book = new Book();
            AddAuthorCommand = new RelayCommandEmpty(AddAuthor);
            AddContentCommand = new RelayCommandEmpty(AddContent);
        }

        private void AddContent()
        {
            Event.ViewEventHandler.RaiseShowAddContentView();
        }

        private void AddAuthor()
        {
            Event.ViewEventHandler.RaiseShowAddContentView();
        }
    }
}
