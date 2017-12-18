namespace MoonaParamStore
{
  /// <summary>
  /// An object to be stored and evaluated
  /// </summary>
  internal class MValue
  {
    public bool IsDirty = true;
    public object Data;

    public MValue( object d )
    {
      Data = d;
    }
  }
}