using System;
using Xunit;

namespace CacheTest
{
    public class UnitTestUserAlgo
    {
        [Fact]
        public void Test()
        {
            UserCacheAPI cache = new UserCacheAPI(8, 2, "CUSTOM");
            cache.put(1, "A");
            System.Threading.Thread.Sleep(50);
            cache.put(2, "B");
            System.Threading.Thread.Sleep(50);
            cache.put(3, "C");
            System.Threading.Thread.Sleep(50);
            cache.put(4, "D");
            System.Threading.Thread.Sleep(50);
            cache.put(5, "E");
            Assert.Equal(cache.get(1), "A");
            Assert.Equal(cache.get(2), "B");
            Assert.Equal(cache.get(3), "C");
            Assert.Equal(cache.get(4), "D");
            Assert.Equal(cache.get(5), "E");
        }
    }
}
