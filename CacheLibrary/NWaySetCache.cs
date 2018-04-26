using System;

namespace CacheLibrary
{   
    /*
     * Implementation of the N way Cache Associative Set
     */
    public class NWaySetCache : ICache
    {   
        /*
         *  replacementAlgo : Replacement Algorithm to be used
         *  nSet : Number of Entries in the Set
         *  nEntry : Number of Entries in each Set
         */
        public readonly CacheObject[] CacheMemory;
        public readonly String replacementAlgo;
        private readonly int nSet;
        private readonly int nEntry;

        /*
         * Constructor in the case where Number of Sets are Given.
         * Number of Entries = 1 (Default)
         * Deafult Replacement Algo = "LRU"
         */
        public NWaySetCache(int nSet){
            this.nSet = nSet;
            this.nEntry = 1;
            this.replacementAlgo = "LRU";
            CacheMemory = new CacheObject[this.nSet * this.nEntry];
            Clear();
         }
          
        /*
         * Constructor in the case where Number of Sets and Number of Entries are Given.
         * Total Number of Objects will be nSets * nEntries.
         */
        public NWaySetCache(int nSet,int nEntry)
        {
            this.nSet = nSet;
            this.nEntry = nEntry;
            this.replacementAlgo = "LRU";
            CacheMemory = new CacheObject[this.nSet * this.nEntry];
            Clear();
        }

        /*
         * Constructor in the case where Number of Sets, Numbner of Entries and the Replacement Algorithm is mentoned are Given.
         * 
         */
        public NWaySetCache(int nSet, int nEntry,String replacementAlgo)
        {
            this.nSet = nSet;
            this.nEntry = nEntry;
            this.replacementAlgo = replacementAlgo;
            CacheMemory = new CacheObject[this.nSet * this.nEntry];
            Clear();
        }

        /*
         * Method to get the size of the nwayset
         */
        public int Size()
        {
            return nSet * nEntry;
        }

        /*
         * Method to clear the whole set
         */
        public void Clear()
        {
            for (int i = 0; i < CacheMemory.Length; i++)
            {
                CacheMemory[i] = new CacheObject();
            }
        }

        /*
         * Method to get the Corresponding value of the given key
         */
        public  Object get(Object key){

            //null fields 
            Object data = null;
            int emptyIndex = 0;
            Boolean isSingleEntryEmpty = false;
            Boolean cacheUpdated = false;

            //Getting the start and the end indexes based on the ID
            int ID = getHash(key);
            int startIndex = getStartIndex(ID);
            int endIndex = getEndIndex(startIndex);



            for (int i = startIndex; i <= endIndex; i++)
            {
                // Making note of the Single Empty Entry in the Array. 
                if (CacheMemory[i].isEmpty && !isSingleEntryEmpty)
                {
                    emptyIndex = i;
                    isSingleEntryEmpty = true;
                    continue;
                }

                // HIT  : Retrive data & update timestamp.
                if (CacheMemory[i].ID == ID)
                {
                    data = CacheMemory[i].Data;
                    CacheMemory[i].TS = getCurrentTime();
                    cacheUpdated = true;
                    break;
                }
            }

            //Cases to be handled in MISS scnarios
            // MISS :  Retrieve Dummy Data from Main memory.
            CacheObject DummyCacheEntry = null;
            if (!cacheUpdated)
            {
                DummyCacheEntry = dummyCallMainMemory(key);
                data = DummyCacheEntry.Data;
                cacheUpdated = true;
            }

            // MISS :  If there is an empty location, then update cache.
            if (isSingleEntryEmpty)
            {
                cacheUpdated = true;
            }

            // MISS : There is no empty entry location, Use Replacement Algorithm used to evict
            // an entry and make space for new entry.
            if (!cacheUpdated)
            {
                int evictedIndex = getEvictedIndex(replacementAlgo, startIndex, endIndex);
                CacheMemory[evictedIndex] = DummyCacheEntry;
            }

            return data;
        }

        /*
         * Method to insert a key value pair in the cache
         */
        public  void put(Object key, Object value){
            //Initializing fields
            int emptyIndex = 0;
            Boolean isSingleEntryEmpty = false;
            Boolean cacheUpdated = false;
            //Getting the start and the end indexes based on the ID
            int ID = getHash(key);
            int startIndex = getStartIndex(ID);
            int endIndex = getEndIndex(startIndex);

            CacheObject newCacheEntry = new CacheObject(false, value, ID, getCurrentTime());

            for (int i = startIndex; i <= endIndex; i++)
            {

                // Making note of the Single Empty Entry in the Array. 
                if (CacheMemory[i].isEmpty && !isSingleEntryEmpty)
                {
                    emptyIndex = i;
                    isSingleEntryEmpty = true;
                    continue;
                }

                // HIT  : Retrive data & updateCache to true.
                if (CacheMemory[i].ID == ID)
                {
                    CacheMemory[i] = newCacheEntry;
                    cacheUpdated = true;
                    break;
                }
                else
                {
                    continue;
                }
            }

            // MISS : Add the new entry to the empty location available.
            if (isSingleEntryEmpty)
            {
                CacheMemory[emptyIndex] = newCacheEntry;
                cacheUpdated = true;
            }

            // MISS:  If there is no empty location, Use Replacement Algorithm to evict
            // an entry and make space for the  new entry.
            if (!cacheUpdated)
            {
                int evictedIndex = getEvictedIndex(replacementAlgo, startIndex, endIndex);
                CacheMemory[evictedIndex] = newCacheEntry;
            }
        }

        /*
         * Method to get current time
         */
        private long getCurrentTime()
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return milliseconds;
        }

        /*
         * Method to main memory
         */
        private CacheObject dummyCallMainMemory(Object key)
        {
            CacheObject cacheEntry = new CacheObject(false, null, getHash(key), getCurrentTime());
            return cacheEntry;
        }

        /*
         * A virtual method to get the hash of the Key.
         * The user will be able to use any has algorithm for any user defined replacement algorithm
         */
        public virtual int getHash(Object key)
        {
            string input = key.ToString();
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            return BitConverter.ToInt32(hash, 0);
        }

        /*
         * Method to get the start Index of the nwayset
         */
        private int getStartIndex(int ID)
        {
            return (ID % nSet) * nEntry;
        }

        /*
         * Method to get the end Index of the nwayset
         */
        private int getEndIndex(int startIndex)
        {
            return startIndex + nEntry - 1;
        }

        /*
         * Method to get the entry based on the replacement algorithm selected
         */
        private int getEvictedIndex(String replacementAlgorithm, int startIndex, int endIndex)
        {
            if ("LRU".Equals(replacementAlgorithm))
            {
                return lruReplacementAlgo(startIndex, endIndex);
            }
            else if ("MRU".Equals(replacementAlgorithm))
            {
                return mruReplacementAlgo(startIndex, endIndex);
            }
            else if ("CUSTOM".Equals(replacementAlgorithm))
            {
                return customReplacementAlgo(startIndex, endIndex);
            }
            else
            {
                throw new NotSupportedException("Unsupported Replacement alogorithm usage.");
            }
        }

        /*
         * Implementation of LRU
         */
        private int lruReplacementAlgo(int startIndex, int endIndex)
        {
            int lruIndex = startIndex;
            long lruTimestamp = CacheMemory[startIndex].TS;
            for (int i = startIndex; i <= endIndex; i++)
            {
                long currentTimestamp = CacheMemory[i].TS;
                if (lruTimestamp > currentTimestamp)
                {
                    lruIndex = i;
                    lruTimestamp = currentTimestamp;
                }
            }
            return lruIndex;
        }

        /*
        * Implementation of MRU
        */
        private int mruReplacementAlgo(int startIndex, int endIndex)
        {
            int mruIndex = startIndex;
            long mruTimestamp = CacheMemory[startIndex].TS;
            for (int i = startIndex; i <= endIndex; i++)
            {
                long currentTimestamp = CacheMemory[i].TS;
                if (mruTimestamp < currentTimestamp)
                {
                    mruIndex = i;
                    mruTimestamp = currentTimestamp;
                }
            }
            return mruIndex;
        }

        /*
        * Implementation of User Defined Replacement Algorithm
        */
        public virtual int customReplacementAlgo(int startIndex, int endIndex)
        {
            return mruReplacementAlgo(startIndex, endIndex);
        }

    }
}
