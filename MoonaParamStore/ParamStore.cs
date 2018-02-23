namespace Moona.RTPC
{
	using System.Collections.Generic;

	/// <summary>
	/// Store things and keep track of dirty objects
	/// https://github.com/jeremytammik/MoonaParamStore
	/// </summary>
	class ParamStore
    {
        // Value store
        private Dictionary<MKey, MValue> _dict;
        private List<MValue> _values;

        // Key Caching
        private static Dictionary<uint, MKey> _idKeyCache;
        private static Dictionary<int, MKey> _hashKeyCache;

        public ParamStore()
        {
            _dict = new Dictionary<MKey, MValue>();
            _values = new List<MValue>();

            if (_idKeyCache == null)
                _idKeyCache = new Dictionary<uint, MKey>();

            if (_hashKeyCache == null)
                _hashKeyCache = new Dictionary<int, MKey>();
        }

        /// <summary>
        /// retrieves cached MKey or generates it if needed
        /// </summary>
        public MKey GetKey(int hash)
        {
            MKey k;
            if (_hashKeyCache.TryGetValue(hash, out k) == false)
            {
                k = new MKey(hash);
                _hashKeyCache.Add(hash, k);
            }

            return k;
        }
        /// <summary>
        /// retrieves cached MKey or generates it if needed
        /// </summary>
        public MKey GetKey(uint id)
        {
            MKey k;
            if (_idKeyCache.TryGetValue(id, out k) == false)
            {
                k = new MKey(id);
                _idKeyCache.Add(id, k);
            }

            return k;
        }

        /// <summary>
        ///  return value or null
        /// </summary>
        public MValue GetValue(MKey k)
        {
            MValue v;

            return _dict.TryGetValue(k, out v)
              ? v
              : null;
        }

        /// <summary>
        ///  Add a new value or update existing entry
        ///  MValues are reference types 
        ///  you can also cache them and simply set their dirty flag
        /// </summary>
        public MValue SetValue(MKey k, MValue value)
        {
            MValue v;
            if (_dict.TryGetValue(k, out v))
            {
                _dict[k] = value;
            }
            else
            {
                _dict.Add(k, value);
				_values.Add(value);
            }

            value.IsDirty = true;
            return value;
        }

        /// <summary>
        /// Return list of values with their IsDirty flag set and reset the dirty flag
        /// </summary>
        public List<MValue> GetDirtyClearFlags()
        {
            List<MValue> dirty = null;

            for(int i = 0; i < _values.Count; i++)
            {
				MValue v = _values[i];

				if (v.IsDirty)
                {
                    v.IsDirty = false;

					if (dirty == null)
						dirty = new List<MValue>();

					dirty.Add(v);
                }
            }
            
            return dirty;
        }
    }
}