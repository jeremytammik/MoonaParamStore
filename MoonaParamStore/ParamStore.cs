using System.Collections.Generic;

namespace Moona.ParamStore
{
  /// <summary>
  /// Store things and keep track of dirty objects
  /// </summary>
  class ParamStore
  {
    Dictionary<MKey, MValue> _dict = new Dictionary<MKey, MValue>();
    List<MValue> _dirty = new List<MValue>();

    /// <summary>
    ///  return value or null
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
        v = new MValue( d );
        _dict.Add( k, v );
      }
      _dirty.Add( v );
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