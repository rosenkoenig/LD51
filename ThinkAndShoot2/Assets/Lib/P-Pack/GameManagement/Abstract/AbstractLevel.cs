using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractLevel : MonoBehaviour
{

  public Transform startSpawnPoint = null;

  [ReadOnly] public bool running = false;
  [ReadOnly] public bool displayed = false;

  private void Awake()
  {
    HideLevel();
  }

  public virtual void DisplayLevel()
  {
    gameObject.SetActive(true);
    displayed = true;
  }

  public virtual void HideLevel ()
  {
    gameObject.SetActive(false);
    displayed = false;
  }

  public virtual void StartLevel ()
  {
    running = true;
  }

  public virtual void StopLevel ()
  {
    running = false;
  }

  public abstract string GetLevelName();
}
