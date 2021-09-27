using System.Windows;
using System.Windows.Controls;
using SloVVo.Logic.ViewModels;

namespace SloVVo.App.Views
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class UserView : Page
    {
        public UserView(UserViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
