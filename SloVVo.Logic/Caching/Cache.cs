using System.Runtime.Caching;

namespace SloVVo.Logic.Caching
{
    public class Cache
    {
        public static ObjectCache DefaultCache = MemoryCache.Default;
    }
}
