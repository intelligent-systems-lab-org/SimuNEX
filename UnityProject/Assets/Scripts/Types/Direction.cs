using UnityEngine;

/// <summary>
/// Represents a direction.
/// </summary>
public enum Direction
{
    Up, Down, Left, Right, Forward, Backward
}

/// <summary>
/// Extension methods for <see cref="Direction"/>.
/// </summary>
public static class DirectionExtensions
{
    /// <summary>
    /// Converts a <see cref="Direction"/> into a <see cref="Vector3"/> representation.
    /// </summary>
    /// <param name="dir">The <see cref="Direction"/> to convert.</param>
    /// <returns>A Vector3 representing the specified direction.</returns>
    public static Vector3 ToVector(this Direction dir)
    {
        return dir switch
        {
            Direction.Left => Vector3.left,
            Direction.Right => Vector3.right,
            Direction.Forward => Vector3.forward,
            Direction.Backward => Vector3.back,
            Direction.Up => Vector3.up,
            Direction.Down => Vector3.down,
            _ => Vector3.zero
        };
    }
}