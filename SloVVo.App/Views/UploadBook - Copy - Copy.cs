using System.Windows.Controls;
using SloVVo.Logic.ViewModels;

namespace SloVVo.App.Views
{
    /// <summary>
    /// Interaction logic for UploadBook.xaml
    /// </summary>
    public partial class BookView : Page
    {
        public BookView(BookViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
