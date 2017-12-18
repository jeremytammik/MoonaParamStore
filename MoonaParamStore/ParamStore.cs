using System.Collections.Generic;

namespace MoonaParamStore
{
  class ParamStore
  {
    Dictionary<MKey, MValue> _dict = new Dictionary<MKey, MValue>();
    List<MValue> _dirty = new List<MValue>();

    /// <summary>
    ///  Add a new value or update existing entry
    /// </summary>
    public MValue Get( MKey k )
    {
      MValue v;

      return _dict.TryGetValue( k, out v )
        ? v
        : null;
    }

    /// <summary>
    ///  Add a new value or update existing entry
    /// </summary>
    public void Set( MKey k, object d )
    {
      MValue v;

      if( _dict.TryGetValue( k, out v ) )
      {
        v.Data = d;
      }
      else
      {
        _dict.Add( k, new MValue( d ) );
      }
    }

    /// <summary>
    /// Return dirty list
    /// </summary>
    public List<MValue> GetDirty()
    {
      return _dirty;
    }

    /// <summary>
    /// Clear dirty list
    /// </summary>
    public void ClearDirty()
    {
      _dirty.Clear();
    }
  }
}