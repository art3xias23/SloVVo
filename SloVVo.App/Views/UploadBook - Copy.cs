using System.Windows.Controls;
using SloVVo.Logic.ViewModels;

namespace SloVVo.App.Views
{
    /// <summary>
    /// Interaction logic for UploadBook.xaml
    /// </summary>
    public partial class BorrowView : Page
    {
        public BorrowView(BorrowViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
