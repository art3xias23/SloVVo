using System;
using System.Windows.Forms;
using SloVVo.Data.EventArgs;
using SloVVo.Data.Models;

namespace SloVVo.Logic.Event
{
    public class ViewEventHandler
    {
        public static Action ShowAddBookScreenAction;

        public static void RaiseShowAddBookView()
        {
            OnRaiseShowAddBookView();
        }

        private static void OnRaiseShowAddBookView()
        {
            ShowAddBookScreenAction?.Invoke();
        }



        public static event EventHandler ShowBooksEvent;

        public static void RaiseShowBooksEvent()
        {
            OnRaiseShowBooksEvent();
        }

        private static void OnRaiseShowBooksEvent()
        {
            ShowBooksEvent?.Invoke(typeof(ViewEventHandler), EventArgs.Empty);
        }


        public static event EventHandler<BookEventArgs> ShowBorrowEvent;

        public static void RaiseShowBorrowEvent(Book selectedItem)
        {
            OnRaiseShowBorrowsEvent(selectedItem);
        }

        private static void OnRaiseShowBorrowsEvent(Book selectedItem)
        {
            ShowBorrowEvent?.Invoke(typeof(ViewEventHandler), new BookEventArgs()
            {
                Book = selectedItem
            });
        }

        public static event EventHandler ShowUsersEvent;

        public static void RaiseShowUsersEvent()
        {
            OnRaiseShowUsersEvent();
        }

        private static void OnRaiseShowUsersEvent()
        {
            ShowUsersEvent?.Invoke(typeof(ViewEventHandler), EventArgs.Empty);
        }

        public static event EventHandler<BookEventArgs> ShowBookEvent;
        public static void RaiseShowBookEvent(Book selectedItem)
        {
            OnRaiseShowBookEvent(selectedItem);
        }

        private static void OnRaiseShowBookEvent(Book selectedItem)
        {
            ShowBookEvent?.Invoke(typeof(ViewEventHandler), new BookEventArgs(){Book = selectedItem});
        }

        public static event EventHandler<UserEventArgs> ShowUserEvent;

        public static void RaiseShowUserEvent(User selectedUser)
        {
            OnRaiseShowUserEvent(selectedUser);
        }

        private static void OnRaiseShowUserEvent(User selectedUser)
        {
            ShowUserEvent?.Invoke(typeof(ViewEventHandler), new UserEventArgs(){User = selectedUser});
        }
    }
}
