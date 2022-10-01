using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public abstract class ComponentAttribute : PropertyAttribute
{
	public ComponentAttribute(string componentPropertyName)
	{
		m_componentPropertyName = componentPropertyName;
	}

	private string m_componentPropertyName;
	public string componentPropertyName { get { return m_componentPropertyName; } }
}
