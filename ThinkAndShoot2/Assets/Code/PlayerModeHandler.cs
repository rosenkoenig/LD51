using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    _NONE,
    _FPS,
    _TOP
}

public class PlayerModeHandler : MonoBehaviour
{
    public PlayerMode currentMode = PlayerMode._NONE;

    public System.Action<PlayerMode> onModeChanged;
    
    public float timer = 0f;

    public MusicManager musicManager;

    public void UpdatePlayerModeTimer()
    {
        timer += Time.unscaledDeltaTime;
        if(timer >= 10f)
        {
            PlayerMode newMode = PlayerMode._FPS;
            if(currentMode == PlayerMode._FPS)
            {
                newMode = PlayerMode._TOP;
            }

            SetMode(newMode);
            timer = 0f;
        }
    }

    public void SetMode (PlayerMode newMode)
    {
        Debug.Log("Set Mode " + newMode);

        if(newMode == currentMode)
        {
            return;
        }

        currentMode = newMode;

        if (onModeChanged != null)
        {
            musicManager.OnModeChanged(currentMode);
            onModeChanged(currentMode);
        }

        if(currentMode == PlayerMode._TOP)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void OnDestroy()
    {
        onModeChanged = null;
    }
}
