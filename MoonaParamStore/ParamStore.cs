using System.Collections.Generic;

namespace Moona.ParamStore
{
    /// <summary>
    /// Store things and keep track of dirty objects
    /// https://github.com/jeremytammik/MoonaParamStore
    /// </summary>
    class ParamStore
    {
        Dictionary<MKey, MValue> _dict = new Dictionary<MKey, MValue>();
        
        /// <summary>
        ///  return value or null
        /// </summary>
        public MValue Get(MKey k)
        {
            MValue v;

            return _dict.TryGetValue(k, out v)
              ? v
              : null;
        }

        /// <summary>
        ///  Add a new value or update existing entry
        /// </summary>
        public MValue Set(MKey k, MValue value)
        {
            MValue v;
            if (_dict.TryGetValue(k, out v))
            {
                _dict[k] = value;
            }
            else
            {
                _dict.Add(k, value);
            }

            value.IsDirty = true;
            return value;
        }

        /// <summary>
        /// Return dirty list
        /// </summary>
        public List<MValue> GetDirtyClearFlags()
        {
            List<MValue> dirty = new List<MValue>();

            foreach(MValue v in _dict.Values)
            {
                if(v.IsDirty)
                {
                    v.IsDirty = false;
                    dirty.Add(v);
                }
            }
            
            return dirty;
        }
    }
}