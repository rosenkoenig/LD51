using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AnimatorParameterAttribute))]
public class AnimatorParameterAttributeProperty : AnimatorBaseAttributePropertyDrawer
{
	protected override void DrawAnimatorProperty(Rect rect, SerializedProperty property, Animator animator)
	{
		AnimatorParameterAttribute animatorParameterAttribute = (AnimatorParameterAttribute)attribute;

		string[] parameterNames = new string[0];
		for (int i = 0; i < animator.parameterCount; i++)
		{
			if (animator.parameters[i].type == animatorParameterAttribute.type || animatorParameterAttribute.anyType)
				ArrayUtility.Add(ref parameterNames, animator.parameters[i].name);
		}

		if (parameterNames.Length > 0)
		{
			int selectedIndex = ArrayUtility.IndexOf(parameterNames, property.stringValue);
			if (selectedIndex == -1)
				selectedIndex = 0;

			selectedIndex = EditorGUI.Popup(rect, selectedIndex, parameterNames);
			property.stringValue = parameterNames[selectedIndex];
		}
	}
}
