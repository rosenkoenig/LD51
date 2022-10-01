using UnityEngine;
using UnityEditor;

//Original version of the ConditionalHideAttribute created by Brecht Lecluyse (www.brechtos.com)
//Modified by: - Giovagnoli Lorris

[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    ConditionalHideAttribute condHAtt;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		condHAtt = (ConditionalHideAttribute)attribute;
		bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

		bool wasEnabled = GUI.enabled;
		GUI.enabled = enabled;
		if (!condHAtt.hideInInspector || enabled)
		{
			EditorGUI.PropertyField(position, property, label, true);
		}

		GUI.enabled = wasEnabled;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		condHAtt = (ConditionalHideAttribute)attribute;
		bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

		if (!condHAtt.hideInInspector || enabled)
		{
			return EditorGUI.GetPropertyHeight(property, label);
		}
		else
		{
			//The property is not being drawn
			//We want to undo the spacing added before and after the property
			return -EditorGUIUtility.standardVerticalSpacing;
		}
	}

	private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
	{
		bool enabled = true;

		for (int i = 0; i < condHAtt.conditionalSourceFields.Length; i++)
		{
			enabled = enabled && GetResult(property, condHAtt.conditionalSourceFields[i]);
		}

		return (condHAtt.inverse) ? !enabled : enabled;
	}

	bool GetResult(SerializedProperty property, string sourceField)
	{
		if (string.IsNullOrEmpty(sourceField)) return true;
		SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(sourceField);
		if (sourcePropertyValue != null)
		{
			return GetEnabledByType(sourcePropertyValue);
		}
		else
		{
			string path = property.propertyPath;
			string[] parts = path.Split('.');
			path = path.Remove(path.IndexOf(parts[parts.Length - 1], 1));
			sourcePropertyValue = property.serializedObject.FindProperty(path + sourceField);
			if (sourcePropertyValue != null)
			{
				return GetEnabledByType(sourcePropertyValue);
			}
			else
			{
				Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + sourceField);
			}
		}

		return true;
	}

	bool GetEnabledByType(SerializedProperty property)
	{
		switch (property.propertyType)
		{
			case SerializedPropertyType.Boolean:
				return property.boolValue;

			case SerializedPropertyType.ObjectReference:
				return property.objectReferenceValue != null;
            case SerializedPropertyType.Integer:
                return property.intValue != 0;
            case SerializedPropertyType.Float:
                return property.floatValue != 0;
            case SerializedPropertyType.Enum:
                return property.enumDisplayNames[property.enumValueIndex].Trim().Replace(" ", "") == condHAtt.wantedValue;
            case SerializedPropertyType.String:
                return property.stringValue == condHAtt.wantedValue;
        }

		return true;
	}
}
