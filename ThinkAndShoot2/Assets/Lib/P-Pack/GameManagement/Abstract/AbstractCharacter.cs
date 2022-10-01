using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCharacter : MonoBehaviour
{
  public virtual void OnSpawn (AbstractGameMaster gm)
  { }

  public virtual void DestroyCharacter ()
  {
    Destroy(gameObject);
  }
}
