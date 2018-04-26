using System;
using Xunit;

namespace CacheTest
{
    public class UnitTestRW
    {
        [Fact]
        public void Test()
        {
            UserCacheAPI cache = new UserCacheAPI(8, 2, "LRU");
            cache.put("Sachin", "Tendulkar");
            cache.put("Rahul", "Dravid");
            Assert.Equal(cache.get("Sachin"), "Tendulkar");
            Assert.Equal(cache.get("Rahul"), "Dravid");
        }
    }
}
