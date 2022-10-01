using UnityEditor;
using UnityEngine;

public abstract class ComponentAttributePropertyDrawer<T> : PropertyDrawer
	where T : Component
{
	private const float m_indentWidth = 15f;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		ComponentAttribute componentAttribute = (ComponentAttribute)attribute;
		T component = property.serializedObject.FindProperty(componentAttribute.componentPropertyName).objectReferenceValue as T;

		float labelWidth = EditorGUIUtility.labelWidth - EditorGUI.indentLevel * m_indentWidth;
		EditorGUI.LabelField(new Rect(position.x, position.y, labelWidth, EditorGUIUtility.singleLineHeight), label);

		Rect drawRect = new Rect(position.x + labelWidth, position.y, position.width - labelWidth, EditorGUIUtility.singleLineHeight);
		if (component == null)
		{
			string errorMessage = string.Format("<color=#FF0000>No {0} has been specified</color>", typeof(T).Name);
			EditorGUI.LabelField(drawRect, errorMessage, CustomEditorStyles.richTextLabel);
		}
		else
		{
			DrawProperty(drawRect, property, component);
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUIUtility.singleLineHeight;
	}

	protected abstract void DrawProperty(Rect rect, SerializedProperty property, T component);
}
