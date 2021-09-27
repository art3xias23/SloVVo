using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloVVo.Logic.Event
{
    public static class EventAggregator
    {
        public static void UpdateAuthorCollection()
        {
            AuthorUpdateTransmitted?.Invoke();
        }

        public static Action AuthorUpdateTransmitted;

        public static void UpdateSectionCollection()
        {
            SectionUpdateTransmitted?.Invoke();
        }

        public static Action SectionUpdateTransmitted;

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
