using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioMixerSnapshot fpsSnapshot;
    public AudioMixerSnapshot topSnapshot;
    public float fpsToTopTransitionDuration = 0.2f;
    public float topToFpsTransitionDuration = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.Instance.onGameStarts += OnLevelStarts;
    }

    void OnLevelStarts ()
    {
        if (GameMaster.Instance.gameLevel.playerModeHandler.onModeChanged != null)
        {
            GameMaster.Instance.gameLevel.playerModeHandler.onModeChanged -= OnModeChanged;
        }
        GameMaster.Instance.gameLevel.playerModeHandler.onModeChanged += OnModeChanged;
    }

    void OnModeChanged(PlayerMode newMode)
    {
        Debug.Log("music change");

        if(newMode == PlayerMode._FPS)
        {
            fpsSnapshot.TransitionTo(topToFpsTransitionDuration);
        }
        else
        {
            topSnapshot.TransitionTo(fpsToTopTransitionDuration);
        }
    }
}
