using System;
namespace CacheLibrary
{   
    /*
     * Interface for the  NwaySetAssociative Cache
     */
    public interface ICache
    {   
        /*
         * Method to get the size of the cache.
         * 
         */
        int Size();
        /*
         * Method to reset the cache.
         */
        void Clear();
        /*
         * Method to READ the object with a KEY key
         */
        Object get( Object key );
        /*
         * Method to WRITE data on the Cache
         */
        void put( Object key,  Object value );
    }
}
