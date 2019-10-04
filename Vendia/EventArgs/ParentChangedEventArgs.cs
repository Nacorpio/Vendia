using Vendia.API;

namespace Vendia.EventArgs
{

  public class ParentChangedEventArgs : System.EventArgs
  {
    public ParentChangedEventArgs(INode node, INode @new)
    {
      Node = node;
      New  = @new;
    }

    public INode Node { get; }
    public INode New { get; }
  }

}