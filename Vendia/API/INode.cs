using System;
using System.Collections.Generic;

using Vendia.EventArgs;

namespace Vendia.API
{

  /// <summary>
  /// Represents a node inside of a tree hierarchy. 
  /// </summary>
  public interface INode : IEnumerable <INode>, IEquatable <INode>
  {
    /// <summary>
    /// Raised when a child has been added to the <see cref="INode"/>.
    /// </summary>
    event EventHandler <ChildEventArgs> ChildAdded;

    /// <summary>
    /// Raised when a child has been removed from the <see cref="INode"/>.
    /// </summary>
    event EventHandler <ChildEventArgs> ChildRemoved;

    /// <summary>
    /// Raised when the parent of the <see cref="INode"/> has changed.
    /// </summary>
    event EventHandler <ParentChangedEventArgs> ParentChanged;

    /// <summary>
    /// Gets a <see cref="INode"/> at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the <see cref="INode"/> to get.</param>
    /// <returns></returns>
    INode this[int index] { get; }

    /// <summary>
    /// Gets the total number of child nodes contained within the <see cref="INode"/>.
    /// </summary>
    int ChildCount { get; }

    /// <summary>
    /// Gets the depth at which the current <see cref="INode"/> is located.
    /// </summary>
    uint? Depth { get; }

    /// <summary>
    /// Gets the zero-based index at which the current <see cref="INode"/> is located.
    /// </summary>
    uint? Index { get; }

    /// <summary>
    /// Gets the parent of the <see cref="INode"/>.
    /// </summary>
    INode Parent { get; }

    /// <summary>
    /// Gets the node next to the current <see cref="INode"/>.
    /// </summary>
    INode Next { get; }

    /// <summary>
    /// Gets the node previous to the current <see cref="INode"/>.
    /// </summary>
    INode Previous { get; }

    /// <summary>
    /// Gets the child nodes of the <see cref="INode"/>.
    /// </summary>
    IReadOnlyList <INode> Children { get; }

    /// <summary>
    /// Indicates whether the <see cref="INode"/> has any children.
    /// </summary>
    bool HasChildren { get; }

    /// <summary>
    /// Indicates whether the current <see cref="INode"/> is the root node.
    /// </summary>
    bool IsRoot { get; }

    /// <summary>
    /// Adds the specified <paramref name="node"/> as a child to the current <see cref="INode"/>.
    /// </summary>
    /// <param name="node">The node to add as a child.</param>
    void AddChild(INode node);

    /// <summary>
    /// Removes the specified <paramref name="node"/> from the current <see cref="INode"/>.
    /// </summary>
    /// <param name="node">The node to remove.</param>
    /// <returns><see langword="true"/> if the child was successfully removed; otherwise, <see langword="false"/>.</returns>
    bool RemoveChild(INode node);

    /// <summary>
    /// Determines whether the specified <paramref name="node"/> is contained within the current <see cref="INode"/>.
    /// </summary>
    /// <param name="node">The node to find.</param>
    /// <returns><see langword="true"/> if the child was found; otherwise, <see langword="false"/>.</returns>
    bool HasChild(INode node);

    /// <summary>
    /// Returns the nodes in the tree which are at a lower depth than the current <see cref="INode"/>.
    /// </summary>
    /// <returns></returns>
    IEnumerable <INode> GetAscendants();

    /// <summary>
    /// Returns the nodes in the tree which are at a higher depth than the current <see cref="INode"/>.
    /// </summary>
    /// <returns></returns>
    IEnumerable <INode> GetDescendants();

    /// <summary>
    /// Returns the nodes in the tree which are at the same depth as the current <see cref="INode"/>.
    /// </summary>
    /// <returns></returns>
    IEnumerable <INode> GetSiblings();
  }

}