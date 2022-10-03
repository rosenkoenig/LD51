#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public static class LDTools
{
    [MenuItem("GameObject/Create Enemy Group")]
    private static void CreateEnemyGroup ()
    {
        if (Selection.objects.Length <= 0) return;

        GameObject[] selected = Selection.gameObjects;

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Enemy/Groups/Templates/EGR_Template.prefab");
        GameObject newParent = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        newParent.name = "EGR_New";
        newParent.transform.parent = selected[0].transform.parent;

        Vector3 pos = Vector3.zero;

        foreach (GameObject gameObject in selected)
        {
            pos += gameObject.transform.position;
        }

        pos /= selected.Length;

        newParent.transform.position = pos;

        foreach (GameObject gameObject in selected)
        {
            gameObject.transform.parent = newParent.transform;
        }

        Selection.objects = null;

        //PrefabUtility.SaveAsPrefabAsset(newParent, "Assets/_Enemy/Groups/" + newParent.name + ".prefab");
    }
}
#endif
