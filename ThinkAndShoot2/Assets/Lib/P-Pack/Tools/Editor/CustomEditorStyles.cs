using UnityEditor;
using UnityEngine;

public static class CustomEditorStyles
{
	private static GUIStyle s_richTextFoldoutStyle;
	private static GUIStyle s_richTextLabel;
	private static GUIStyle s_middleCenteredBackgroundLabel;
	private static GUIStyle s_wordWrapBackgroundLabel;

	public static GUIStyle richTextFoldoutStyle
	{
		get
		{
			if (s_richTextFoldoutStyle == null)
			{
				s_richTextFoldoutStyle = new GUIStyle(EditorStyles.foldout);
				s_richTextFoldoutStyle.richText = true;
			}
			return s_richTextFoldoutStyle;
		}
	}

	public static GUIStyle richTextLabel
	{
		get
		{
			if (s_richTextLabel == null)
			{
				s_richTextLabel = new GUIStyle(EditorStyles.label);
				s_richTextLabel.richText = true;
			}
			return s_richTextLabel;
		}
	}

	public static GUIStyle middleCenteredBackgroundLabel
	{
		get
		{
			if (s_middleCenteredBackgroundLabel == null)
			{
				s_middleCenteredBackgroundLabel = new GUIStyle(GUI.skin.label);
				s_middleCenteredBackgroundLabel.alignment = TextAnchor.MiddleCenter;
				s_middleCenteredBackgroundLabel.normal.background = EditorGUIUtility.whiteTexture;
			}
			return s_middleCenteredBackgroundLabel;
		}
	}

	public static GUIStyle wordWrapBackgroundLabel
	{
		get
		{
			if (s_wordWrapBackgroundLabel == null)
			{
				s_wordWrapBackgroundLabel = new GUIStyle(GUI.skin.label);
				s_wordWrapBackgroundLabel.wordWrap = true;
				s_wordWrapBackgroundLabel.alignment = TextAnchor.MiddleLeft;
				s_wordWrapBackgroundLabel.normal.background = EditorGUIUtility.whiteTexture;
			}
			return s_wordWrapBackgroundLabel;
		}
	}
}
