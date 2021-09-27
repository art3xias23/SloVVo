using System;
using System.Windows.Forms;
using SloVVo.Data.EventArgs;
using SloVVo.Data.Models;

namespace SloVVo.Logic.Event
{
    public class ViewEventHandler
    {



        #region ShowAddContent

        public static event EventHandler ShowAddContentScreenEvent;

        public static void RaiseShowAddContentView()
        {
            OnRaiseShowAddContentView();
        }

        private static void OnRaiseShowAddContentView()
        {
            ShowAddContentScreenEvent?.Invoke(typeof(ViewEventHandler), EventArgs.Empty);
        }

        public static bool HasShowAddContentScreenEventListeners => ShowAddContentScreenEvent != null;
        #endregion


        #region ShowAddAuthor
        public static event EventHandler ShowAddAuthorScreenEvent;

        public static void RaiseShowAddAuthorView()
        {
            OnRaiseShowAddAuthorView();
        }

        private static void OnRaiseShowAddAuthorView()
        {
            ShowAddAuthorScreenEvent?.Invoke(typeof(ViewEventHandler), EventArgs.Empty);
        }

        public static bool HasShowAddAuthorScreenEventListeners => ShowAddAuthorScreenEvent != null;
        #endregion

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
