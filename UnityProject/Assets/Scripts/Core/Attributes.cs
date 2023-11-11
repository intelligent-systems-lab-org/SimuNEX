using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Marks the variable as a parameter to the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ParameterAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// Marks the variable as an input to the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class InputAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// Marks the property so that <see cref="Fault"/> objects can be applied.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class Faultable : Attribute
    {
    }
}
