using System;
using Xunit;

namespace CacheTest
{
    public class UnitTestUpdateValue
    {
        [Fact]
        public void Test()
        {
            UserCacheAPI cache = new UserCacheAPI(8, 2, "LRU");
            cache.put(1, "Kishan");
            cache.put(1, "Varma");
            Assert.Equal(cache.get(1), "Varma");

        }
    }
}
