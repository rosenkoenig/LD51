using UnityEditor;
using UnityEngine;

public abstract class AnimatorBaseAttributePropertyDrawer : ComponentAttributePropertyDrawer<Animator>
{
	protected override void DrawProperty(Rect rect, SerializedProperty property, Animator animator)
	{
		if (animator.layerCount == 0)
		{
			if (GUI.Button(rect, "Force animator update"))
			{
				GameObject current = Selection.activeGameObject;
				Selection.activeGameObject = animator.gameObject;
				EditorApplication.delayCall += () =>
				{
					Selection.activeGameObject = current;
				};
			}
		}
		else
		{
			DrawAnimatorProperty(rect, property, animator);
		}
	}

	protected abstract void DrawAnimatorProperty(Rect rect, SerializedProperty property, Animator animator);
}
