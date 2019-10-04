using Vendia.API;

namespace Vendia.EventArgs
{

  public class ChildEventArgs : System.EventArgs
  {
    public ChildEventArgs(INode node)
    {
      Node = node;
    }

    public INode Node { get; }
  }

}