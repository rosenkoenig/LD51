using UnityEngine;
using System;
using System.Collections;

//Original version of the ConditionalHideAttribute created by Brecht Lecluyse (www.brechtos.com)
//Modified by: Giovagnoli Lorris
//[ConditionalHide("test", hideInInspector = true)]

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
		AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    /// <summary>
    /// Only used in Enum or String variable
    /// </summary>
    public string wantedValue;
	public string[] conditionalSourceFields;
	public bool hideInInspector = false;
	public bool inverse = false;

	public ConditionalHideAttribute(string sourceFieldsAttribute)
	{
		setupArray(sourceFieldsAttribute);
		this.hideInInspector = false;
		this.inverse = false;
	}

	public ConditionalHideAttribute(string sourceFieldsAttribute, bool hideInInspector)
	{
		setupArray(sourceFieldsAttribute);
		this.hideInInspector = hideInInspector;
		this.inverse = false;
	}

	public ConditionalHideAttribute(string sourceFieldsAttribute, bool hideInInspector, bool inverse)
	{
		setupArray(sourceFieldsAttribute);
		this.hideInInspector = hideInInspector;
		this.inverse = inverse;
	}

	void setupArray(string sourceFields)
	{
		conditionalSourceFields = sourceFields.Split(' ');
	}
}



