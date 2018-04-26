using System;
using Xunit;


namespace CacheTest
{
    public class UnitTestMRU
    {
        [Fact]
        public void Test()
        {
            UserCacheAPI cache = new UserCacheAPI(8, 2, "MRU");
            cache.put(16, "Harden");
            System.Threading.Thread.Sleep(50);
            cache.put(32, "James");
            System.Threading.Thread.Sleep(50);
            cache.put(48, "Lebron");
            Assert.Equal(cache.get(16), "Harden");
            Assert.Equal(cache.get(32), null);
            Assert.Equal(cache.get(48), "Lebron");
        }
    }
}
