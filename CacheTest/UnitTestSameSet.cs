using System;
using Xunit;

namespace CacheTest
{
    public class UnitTestSameSet
    {
        [Fact]
        public void Test()
        {
            UserCacheAPI cache = new UserCacheAPI(4, 2, "LRU");
            cache.put(16, "James");
            cache.put(32, "Lebron");
            Assert.Equal(cache.CacheMemory[0].Data, "James");
            Assert.Equal(cache.CacheMemory[1].Data, "Lebron");
        }
    }
}
