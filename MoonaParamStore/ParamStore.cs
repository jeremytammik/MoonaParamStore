using System.Collections.Generic;

namespace MoonaParamStore
{
  class ParamStore
  {
    Dictionary<MKey, MValue> _dict = new Dictionary<MKey, MValue>();

    /// <summary>
    ///  Add a new value or update existing entry
    /// </summary>
    private MValue Get( MKey k )
    {
      MValue v;

      return _dict.TryGetValue( k, out v )
        ? v
        : null;
    }

    /// <summary>
    ///  Add a new value or update existing entry
    /// </summary>
    private void Set( MKey k, object d )
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
    ///  Return a hashed value or null
    /// </summary>
    public MValue Get( int hash )
    {
      MKey k = new MKey( hash );
      return Get( k );
    }

    /// <summary>
    ///  Return an id value or null
    /// </summary>
    public MValue Get( uint id )
    {
      MKey k = new MKey( id );
      return Get( k );
    }

    /// <summary>
    ///  Add a new hashed value or update existing entry
    /// </summary>
    public void Set( int hash, object d )
    {
      MKey k = new MKey( hash );
      Set( k, d );
    }

    /// <summary>
    ///  Add a new id value or update existing entry
    /// </summary>
    public void Set( uint id, object d )
    {
      MKey k = new MKey( id );
      Set( k, d );
    }
  }
}
