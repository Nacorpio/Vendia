using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Vendia.API;
using Vendia.EventArgs;

namespace Vendia
{

  public class Node : INode
  {
    /// <summary>
    /// Raised when a child has been added to the <see cref="Node"/>.
    /// </summary>
    public event EventHandler <ChildEventArgs> ChildAdded;

    /// <summary>
    /// Raised when a child has been removed from the <see cref="Node"/>.
    /// </summary>
    public event EventHandler <ChildEventArgs> ChildRemoved;

    /// <summary>
    /// Raised when the parent of the <see cref="Node"/> has changed.
    /// </summary>
    public event EventHandler <ParentChangedEventArgs> ParentChanged;

    private List <INode> _children;

    /// <summary>
    /// Initializes a new instance of the <see cref="Node" /> class.
    /// </summary>
    public Node()
    {
      Index = 0;
      Depth = 0;
    }

    /// <summary>
    /// Gets a <see cref="INode"/> at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the <see cref="INode"/> to get.</param>
    /// <returns></returns>
    public INode this[int index] => _children[index];

    /// <summary>
    /// Gets the total number of child nodes contained within the <see cref="INode"/>.
    /// </summary>
    public int ChildCount => _children?.Count ?? 0;

    /// <summary>
    /// Gets the depth at which the current <see cref="INode"/> is located.
    /// </summary>
    public uint? Depth { get; internal set; }

    /// <summary>
    /// Gets the zero-based index at which the current <see cref="INode"/> is located.
    /// </summary>
    public uint? Index { get; internal set; }

    /// <summary>
    /// Gets the parent of the <see cref="INode"/>.
    /// </summary>
    public INode Parent { get; internal set; }

    /// <summary>
    /// Gets the node next to the current <see cref="INode"/>.
    /// </summary>
    public INode Next => Index.HasValue ? Parent.GetSiblings().ElementAt((int) ( Index.Value + 1 )) : null;

    /// <summary>
    /// Gets the node previous to the current <see cref="INode"/>.
    /// </summary>
    public INode Previous => Index.HasValue ? Parent.GetSiblings().ElementAt((int) ( Index.Value - 1 )) : null;

    /// <summary>
    /// Gets the child nodes of the <see cref="INode"/>.
    /// </summary>
    public IReadOnlyList <INode> Children => _children ?? new List <INode>();

    /// <summary>
    /// Indicates whether the <see cref="INode"/> has any children.
    /// </summary>
    public bool HasChildren => _children != null && _children.Any();

    /// <summary>
    /// Indicates whether the current <see cref="INode"/> is the root node.
    /// </summary>
    public bool IsRoot => Parent == null && Depth == 0 && Index == 0;

    /// <summary>
    /// Adds the specified <paramref name="node"/> as a child to the current <see cref="INode"/>.
    /// </summary>
    /// <param name="node">The node to add as a child.</param>
    public void AddChild(INode node)
    {
      if (node == null)
      {
        return;
      }

      if (!( node is Node n ))
      {
        return;
      }

      if (_children == null)
      {
        _children = new List <INode>();
      }

      if (node.Parent != null)
      {
        n.Parent.RemoveChild(n);
        n.Parent = this;

        n.ParentChanged?.Invoke(this, new ParentChangedEventArgs(n, this));
      }

      n.Index = (uint) _children.Count;
      n.Depth = Depth + 1;

      _children.Add(node);
      ChildAdded?.Invoke(this, new ChildEventArgs(n));
    }

    /// <summary>
    /// Removes the specified <paramref name="node"/> from the current <see cref="INode"/>.
    /// </summary>
    /// <param name="node">The node to remove.</param>
    /// <returns><see langword="true"/> if the child was successfully removed; otherwise, <see langword="false"/>.</returns>
    public bool RemoveChild(INode node)
    {
      if (node == null || _children == null || !_children.Remove(node))
      {
        return false;
      }

      ChildRemoved?.Invoke(this, new ChildEventArgs(node));
      return true;
    }

    /// <summary>
    /// Determines whether the specified <paramref name="node"/> is contained within the current <see cref="INode"/>.
    /// </summary>
    /// <param name="node">The node to find.</param>
    /// <returns><see langword="true"/> if the child was found; otherwise, <see langword="false"/>.</returns>
    public bool HasChild(INode node)
    {
      return _children != null && _children.Any(x => x.Equals(node));
    }

    /// <summary>
    /// Returns the nodes in the tree which are at a lower depth than the current <see cref="INode"/>.
    /// </summary>
    /// <returns></returns>
    public IEnumerable <INode> GetAscendants()
    {
      var results = new List <INode>
      {
        Parent
      };

      foreach (var sibling in Parent.GetSiblings())
      {
        results.Add(sibling);
        results.AddRange(sibling.GetAscendants());
      }

      return results;
    }

    /// <summary>
    /// Returns the nodes in the tree which are at a higher depth than the current <see cref="INode"/>.
    /// </summary>
    /// <returns></returns>
    public IEnumerable <INode> GetDescendants()
    {
      var results = new List <INode>();

      foreach (var node in _children)
      {
        results.Add(node);
        results.AddRange(node.GetDescendants());
      }

      return results;
    }

    /// <summary>
    /// Returns the nodes in the tree which are at the same depth as the current <see cref="INode"/>.
    /// </summary>
    /// <returns></returns>
    public IEnumerable <INode> GetSiblings()
    {
      return Parent?.Children?.Except(new[] { this });
    }

    /// <summary>Returns an enumerator that iterates through the collection.</summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator <INode> GetEnumerator()
    {
      return _children.GetEnumerator();
    }

    /// <summary>Returns an enumerator that iterates through a collection.</summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public bool Equals(INode other)
    {
      return Depth.Equals(other?.Depth) && Index.Equals(other?.Index);
    }
  }

}