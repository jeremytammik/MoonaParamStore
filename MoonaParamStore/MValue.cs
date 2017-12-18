namespace Moona
{
  /// <summary>
  /// An object to be stored and evaluated
  /// </summary>
  internal class MValue
  {
    public object Data;

    public MValue( object d )
    {
      Data = d;
    }
  }
}