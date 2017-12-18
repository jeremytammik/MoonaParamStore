using System.Collections.Generic;

namespace MoonaParamStore
{
  class ParamStore
  {

    Dictionary<MKey, MValue> _dict;

    public void Set( int hash, object d )
    {
      MKey k = new MKey( hash );
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
  }
}
