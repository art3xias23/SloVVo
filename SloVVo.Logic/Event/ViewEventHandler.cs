using System;

namespace SloVVo.Logic.Event
{
    public class ViewEventHandler
    {
        #region ShowUploadScreen
        public static event EventHandler ShowUploadScreenEvent;

        public static void RaiseShowUploadView()
        {
            OnRaiseShowUploadView();
        }
        private static void OnRaiseShowUploadView()
        {
            ShowUploadScreenEvent?.Invoke(typeof(ViewEventHandler), EventArgs.Empty);
        }
        public static bool HasShowUploadScreenEventListeners => ShowUploadScreenEvent != null;
        #endregion

        #region CloseUploadScreen 


        public static event EventHandler CloseUploadScreenEvent;

        public static void RaiseCloseUploadView()
        {
            OnRaiseCloseUploadView();
        }
        private static void OnRaiseCloseUploadView()
        {
            CloseUploadScreenEvent?.Invoke(typeof(ViewEventHandler), EventArgs.Empty);
        }

        public static bool HasCloseUploadScreenEventListeners => CloseUploadScreenEvent != null;

        #endregion


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

        #region CloseAddContent
        public static event EventHandler CloseAddContentScreenEvent;

        public static void RaiseCloseAddContentView()
        {
            OnRaiseCloseAddContentView();
        }

        private static void OnRaiseCloseAddContentView()
        {
            CloseAddContentScreenEvent?.Invoke(typeof(ViewEventHandler), EventArgs.Empty);
        }

        public static bool HasCloseAddContentScreenEventListeners => CloseAddContentScreenEvent != null;
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

        #region CloseAddAuthorScreenEvent 


        public static event EventHandler CloseAddAuthorScreenEvent;

        public static void RaiseCloseAddAuthorView()
        {
            OnRaiseCloseAddAuthorView();
        }

        private static void OnRaiseCloseAddAuthorView()
        {
            CloseAddAuthorScreenEvent?.Invoke(typeof(ViewEventHandler), EventArgs.Empty);
        }

        public static bool HasCloseAddAuthorScreenEventListeners => CloseAddAuthorScreenEvent != null;

        #endregion

        #region AddUser 
        public static event EventHandler ShowAddUserEvent;

        public static void RaiseShowAddUserEvent()
        {
            OnRaiseShowAddUserEvent();
        }

        private static void OnRaiseShowAddUserEvent()
        {
            ShowAddUserEvent?.Invoke(typeof(ViewEventHandler), EventArgs.Empty);
        }
        public static bool HasAddUserScreenEventListeners => ShowAddUserEvent != null;

        #endregion

        #region CloseAddUser 
        public static event EventHandler CloseAddUserEvent;

        public static void RaiseCloseAddUserEvent()
        {
            OnRaiseCloseAddUserEvent();
        }

        private static void OnRaiseCloseAddUserEvent()
        {
            CloseAddUserEvent?.Invoke(typeof(ViewEventHandler), EventArgs.Empty);
        }
        public static bool HasCloseUserScreenEventListeners => CloseAddUserEvent != null;

        #endregion
    }
}
