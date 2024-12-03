using System;
using UnityEngine;

/// <summary> 
/// <para>
/// Used for UnityEngine.<typeparamref name="Object"/> (recommended), 
/// <typeparamref name="ScriptableObject"/> or <typeparamref name="Component"/>.
/// </para>
/// <para>
/// Don't use for <typeparamref name="GameObject"/> field type with <typeparamref name="RequireInterface"/> attribute.
/// </para>
/// </summary>
public class RequireInterfaceAttribute : PropertyAttribute
{
    public RequireInterfaceAttribute (Type type) => Type = type;

    public Type Type { get; private set; }
}
