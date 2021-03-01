using System;

namespace SloVVo.ViewModels.Event
{
    public class ViewEventHandler
    {
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
    }
}
