using System;
using System.Collections.Generic;
using NaughtyBiker.InfoObjects.Interfaces;

namespace NaughtyBiker.InfoObjects {
    /**
    * Default implementation of IInfo
    * 
    * @author Julian Sangillo
    * @version 1.0
    *
    * @see InfoObject
    */
    public class Info : IInfo {

        private IDictionary<string, object> data;
        private StateChange stateChanged;

        private string id;

        /**
        * Default Constructor
        *
        * @param id A string that can be used as a unique identifier for this collection of info
        */
        public Info(string infoId) {
            this.Id = infoId;
            this.data = new Dictionary<string, object>();
            this.StateChanged = (id, key, value) => {};
        }

        /**
        * @param id A string that can be used as a unique identifier for this collection of info
        * @param data A hash table for storing the info added by other objects
        */
        public Info(string infoId, IDictionary<string, object> data) {
            this.Id = infoId;
            this.data = data;
            this.StateChanged = (id, key, value) => {};
        }

        /**
        * @param id A string that can be used as a unique identifier for this collection of info
        * @param stateChanged The callback function to be called when a value stored in this info object has been changed
        */
        public Info(string infoId, StateChange stateChanged) {
            this.Id = infoId;
            this.data = new Dictionary<string, object>();
            this.StateChanged = stateChanged;
        }

        /**
        * @param id A string that can be used as a unique identifier for this collection of info
        * @param data A hash table for storing the info added by other objects
        * @param stateChanged The callback function to be called when a value stored in this info object has been changed
        */
        public Info(string infoId, IDictionary<string, object> data, StateChange stateChanged) {
            this.Id = infoId;
            this.data = data;
            this.StateChanged = stateChanged;
        }

        /**
        * Default implementation of IInfo.Data
        *
        * @return The current hash table where all info is stored
        */
        public IDictionary<string, object> Data {
            get {
                return data;
            }
        }

        /**
        * Default implementation of IInfo.StateChanged
        *
        * @param stateChanged The callback function to be called when a value stored in this info object has been changed
        */
        public StateChange StateChanged {
            set {
                stateChanged = value;
            }
        }

        /**
        * Default implementation of IInfo.Id
        *
        * @param id (Set only) A string that can be used as a unique identifier for this collection of info
        *
        * @return The current info id
        */
        public string Id {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        /**
        * Default implementation of <a href="d3/dd1/interface_i_info.html#a7ade3a9fcbb1f791627e2ab87495d986">IInfo.this[string key]</a>
        *
        * @param key The key string that uniquely identifies a stored value
        * @param value (Set only) The value to assign to the given key. May be of any type
        *
        * @return The value identified by its key
        *
        * @throws ArgumentNullException Raised when the key parameter is null
        * @throws ArgumentException Raised when the key parameter is an empty string
        * @throws KeyNotFoundException (Get only) Raised when the key parameter doesn't exist in this info object
        */
        public object this[string key] {
            get {
                return Get(key);
            }
            set {
                Set(key, value);
            }
        }

        /**
        * Default implementation of IInfo.Get(string key)
        *
        * @param key The key string that uniquely identifies a stored value
        * 
        * @return The value of the key
        *
        * @throws ArgumentNullException Raised when the key parameter is null
        * @throws ArgumentException Raised when the key parameter is an empty string
        * @throws KeyNotFoundException Raised when the key parameter doesn't exist in this info object
        */
        public object Get(string key) {

            if(string.IsNullOrEmpty(key))
                if(key == null)
                    throw new ArgumentNullException("Cannot find value when key is 'null'");
                else
                    throw new ArgumentException("Cannot find value when key is an 'empty_string'");

            object value;

            if(!data.TryGetValue(key, out value))
                throw new KeyNotFoundException("Unable to return value of an unknown key!");

            return value;
        }

        /**
        * Default implementation of IInfo.Set(string key, object value)
        * 
        * @param key The key string that uniquely identifies a stored value
        * @param value The value to assign to the given key. May be of any type
        *
        * @throws ArgumentNullException Raised when the key parameter is null
        * @throws ArgumentException Raised when the key parameter is an empty string
        */
        public void Set(string key, object value) {
            
            if(string.IsNullOrEmpty(key))
                if(key == null)
                    throw new ArgumentNullException("Cannot set value of a 'null' key");
                else
                    throw new ArgumentException("Cannot set value of an 'empty_string' key");

            if(data.ContainsKey(key))
                data[key] = value;
            else
                data.Add(key, value);

            stateChanged(this.Id, key, value);

        }

        /**
        * Default implementation of IInfo.Exists(string key)
        *
        * @param key The key string that uniquely identifies a stored value
        *
        * @return True if info contains the key or false otherwise
        *
        * @throws ArgumentNullException Raised when the key parameter is null
        * @throws ArgumentException Raised when the key parameter is an empty string
        */
        public bool Exists(string key) {

            if(string.IsNullOrEmpty(key))
                if(key == null)
                    throw new ArgumentNullException("Cannot find a 'null' key");
                else
                    throw new ArgumentException("Cannot find an 'empty_string' key");
                    
            return data.ContainsKey(key);
        }

    }
}