using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChapterEndTrigger
{
    TIME,
    ENDLESS
}

public enum ChapterState
{
    WAITING,
    STARTED,
    ENDED
}

public enum SpawnMode
{
    RANDOM,
    SEQUENCE
}

[System.Serializable]
public class LevelChapter
{
    public List<EnemyGroup> enemyGroups;
    int lastGroupSpawnedIdx = 0;
    
    [Header("Spawn Settings")]
    public SpawnMode spawnMode;
    public float spawnFrequency;
    float spawnTimer = 0;

    [Header("Chapter End Conditions")]
    public ChapterEndTrigger endTrigger = ChapterEndTrigger.TIME;

    [ConditionalHide("endTrigger", wantedValue = "TIME", hideInInspector = true)]
    public float duration = 0f;
    float chapterTimer = 0f;

    [ConditionalHide("endTrigger", wantedValue = "TIME", hideInInspector = true)]
    public bool keepSpawningAfterEnd = false;

    public ChapterState state { get; private set; }

    public void StartChapter()
    {
        lastGroupSpawnedIdx = -1;
        chapterTimer = 0f;
        spawnTimer = 0f;
        state = ChapterState.STARTED;
    }

    public void UpdateChapter(float _dt, EnemyGroupsSpawner _spawner)
    {
        switch (endTrigger)
        {
            case ChapterEndTrigger.TIME:
                chapterTimer += _dt;
                if(chapterTimer >= duration)
                {
                    state = ChapterState.ENDED;
                }
                break;
            case ChapterEndTrigger.ENDLESS:
                break;
            default:
                break;
        }

        if (state == ChapterState.STARTED)
        {
            if (spawnTimer >= spawnFrequency)
            {
                int groupToSpawnIdx = -1;
                spawnTimer = 0f;

                switch (spawnMode)
                {
                    case SpawnMode.RANDOM:
                        groupToSpawnIdx = Random.Range(0, enemyGroups.Count);
                        break;
                    case SpawnMode.SEQUENCE:
                        groupToSpawnIdx = lastGroupSpawnedIdx + 1;
                        break;
                    default:
                        break;
                }

                lastGroupSpawnedIdx = groupToSpawnIdx;
                _spawner.SpawnEnemyGroup(enemyGroups[groupToSpawnIdx]);
            }
            else
            {
                spawnTimer += _dt;
            }

        }
    }
}
