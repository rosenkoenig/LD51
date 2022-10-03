


#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEditor.Overlays;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[Overlay(typeof(SceneView), id: ID_OVERLAY, displayName: "Enemy Creator")]
public class EnemyTool : ToolbarOverlay
{
    private const string ID_OVERLAY = "enemy-overlay";

    public EnemyTool () : base(CreateHomerEnemyButton.ID, CreateShooterEnemyButton.ID, CreateDiggerEnemyButton.ID, CreateShielderEnemyButton.ID)
    {

    }
}

[EditorToolbarElement(id: ID, typeof(SceneView))]
public class CreateHomerEnemyButton : EditorToolbarButton
{
    public const string ID = "CreateHomerEnemyButton";

    public CreateHomerEnemyButton ()
    {
        text = "Create Homer";
        tooltip = "Create Homer";
        icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/homer.png");
        clicked += OnCreateEnemy;
    }

    private void OnCreateEnemy()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Enemy/Enemy_Homer.prefab");
        Selection.activeObject = PrefabUtility.InstantiatePrefab(prefab);
    }
}

[EditorToolbarElement(id: ID, typeof(SceneView))]
public class CreateShielderEnemyButton : EditorToolbarButton
{
    public const string ID = "CreateShielderEnemyButton";

    public CreateShielderEnemyButton()
    {
        text = "Create Shielder";
        tooltip = "Create Shielder";
        icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/shielder.png");
        clicked += OnCreateEnemy;
    }

    private void OnCreateEnemy()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Enemy/Enemy_Shielder.prefab");
        Selection.activeObject = PrefabUtility.InstantiatePrefab(prefab);
    }
}

[EditorToolbarElement(id: ID, typeof(SceneView))]
public class CreateShooterEnemyButton : EditorToolbarButton
{
    public const string ID = "CreateShooterEnemyButton";

    public CreateShooterEnemyButton()
    {
        text = "Create Shooter";
        tooltip = "Create Shooter";
        icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/shooter.png");
        clicked += OnCreateEnemy;
    }

    private void OnCreateEnemy()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Enemy/Enemy_Shooter.prefab");
        Selection.activeObject = PrefabUtility.InstantiatePrefab(prefab);
    }
}

[EditorToolbarElement(id: ID, typeof(SceneView))]
public class CreateDiggerEnemyButton : EditorToolbarButton
{
    public const string ID = "CreateDiggerEnemyButton";

    public CreateDiggerEnemyButton()
    {
        text = "Create Digger";
        tooltip = "Create Digger";
        icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/digger.png");
        clicked += OnCreateEnemy;
    }

    private void OnCreateEnemy()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Enemy/Enemy_Digger.prefab");
        Selection.activeObject = PrefabUtility.InstantiatePrefab(prefab);
    }
}

#endif
