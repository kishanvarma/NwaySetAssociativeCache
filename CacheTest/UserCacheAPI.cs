using System;
using CacheLibrary;

namespace CacheTest
{
    /*
     * Client implementation of the library
     */

    public class UserCacheAPI : NWaySetCache
    {
        /*
         * Client Constructor to Initialize a N way Set Cache
         * nSet - Number of sets
         * nEntry - Number of Entries
         * replacementAlgo - Algorithm to be used for replacement
         */
        public UserCacheAPI(int nSet, int nEntry, String replacementAlgo)
            : base(nSet, nEntry, replacementAlgo)
        {

        }
        /*
         * The User can override the hash Algorithm
         */
        public override int getHash(Object key)
        {
            return Math.Abs(key.GetHashCode());
        }
        /*
         * Alternative replacement Algorithm to be used by the client
         */

        public override int customReplacementAlgo(int startIndex, int endIndex)
        {
            return endIndex;
        }

    }
}
