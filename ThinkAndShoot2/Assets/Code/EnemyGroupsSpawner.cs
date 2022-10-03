using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(GameLevel))]
public class EnemyGroupsSpawner : MonoBehaviour
{
    GameLevel gameLevel = null;
    public List<LevelChapter> levelChapters;
    int currentChapterIndex;

    // Start is called before the first frame update
    void Awake()
    {
        gameLevel = GetComponent<GameLevel>();
    }

    public void OnLevelStarts ()
    {
        currentChapterIndex = 0;
        StartChapter();
    }

    void StartChapter()
    {
        LevelChapter chapter = levelChapters[currentChapterIndex];
        chapter.StartChapter();
    }

    public void UpdateChapters ()
    {
        foreach(LevelChapter chapter in levelChapters)
        {
            if(chapter.state == ChapterState.STARTED)
            {
                chapter.UpdateChapter(Time.deltaTime, this);
                if(chapter.state == ChapterState.ENDED)
                {
                    GoToNextChapter();
                }
            }
            else if(chapter.state == ChapterState.ENDED && chapter.keepSpawningAfterEnd)
            {
                chapter.UpdateChapter(Time.deltaTime, this);
            }
        }
    }

    void GoToNextChapter ()
    {
        currentChapterIndex++;
        if(currentChapterIndex < levelChapters.Count)
        {
            StartChapter();
        }
    }

    public void SpawnEnemyGroup(EnemyGroup group)
    {
        GameObject inst = Instantiate(group.gameObject, gameLevel.transform);
        EnemyGroup groupInst = inst.GetComponent<EnemyGroup>();
        groupInst.OnSpawned();
    }
}
