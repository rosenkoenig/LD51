using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectLevelLoader : AbstractLevelLoaderOf<AbstractLevel>
{
    //List<GameObject> levels = default;

    void Awake ()
    {
        foreach(AbstractLevel lvl in levels)
        {
            lvl.gameObject.SetActive(false);
        }
    }

    protected override void DoLoadLevel(int levelIdx)
    {
        // levels[levelIdx]
        GameObject go = Instantiate(levels[levelIdx].gameObject);
        AbstractLevel level = go.GetComponent<AbstractLevel>();
        loadedLevels[levelIdx] = level;
    }

    protected override void DoUnloadLevel(int levelIdx)
    {
        if(loadedLevels[levelIdx] != null)
        {
            Destroy(loadedLevels[levelIdx].gameObject);
        }

        loadedLevels[levelIdx] = null;
    }

    protected override bool LevelIsLoaded(int levelIdx)
    {
        return loadedLevels[levelIdx] != null;
    }
}
