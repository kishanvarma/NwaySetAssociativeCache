using System;
namespace CacheLibrary
{
    public class CacheObject
    {
        /*
         * Structure of a Single Cache Entry 
         ____________________________
        | IsEmpty  |  Data | ID | TS |
        |__________|_______|____|____|
        */

        /*
         * Check if the Entry is Valid
         */
        private Boolean _isEmpty;
        public Boolean isEmpty
        {
            get{
                return _isEmpty;
            }
            set{
                _isEmpty = value;
            }
        }

        /*
         * This field represents Actual Data
         */
        private Object _Data;
        public Object Data
        {
            get{
                return _Data;
            }
            set{
                _Data = value;
            }
        }

        /*
         * This field represents the ID which id unique , represents unique hash key
         */
        private int _ID;
        public int ID
        {
            get{
                return _ID;
            }
            set{
                _ID = value;
            }
        }

        /*
         * This field stores the Time Stamp to be used in Replacement Algorithms
         */
        private long _TS;
        public long TS{
            get{
                return _TS;
            }
            set{
                _TS = value;
            }
        }
        /*
         * Default Constructor used to fill the empty cache entry
         */
        public CacheObject()
        {   
            
            this.isEmpty    = true;
            this.Data       = "null";
            this.ID         = 0;
            this.TS         = 0;
        }

        /*
         * Constructor to fill all fields of the entry
         */
        public CacheObject(Boolean isEmpty, Object Data, int ID, long TS)
        {

            this.isEmpty    = isEmpty;
            this.Data       = Data;
            this.ID         = ID;
            this.TS         = TS;
        }

    }
}
