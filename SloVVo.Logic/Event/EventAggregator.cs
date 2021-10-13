using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloVVo.Logic.Event
{
    public static class EventAggregator
    {
        public static void UpdateBookCollection()
        {
            BookUpdateTransmitted?.Invoke();
        }

        public static Action BookUpdateTransmitted;
        public static void UpdateUserCollection()
        {
            UserUpdateTransmitted?.Invoke();
        }

        public static Action UserUpdateTransmitted;
    }
}
