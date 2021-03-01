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

        public static bool HasEventListeners => ShowUploadScreenEvent != null;
    }
}
