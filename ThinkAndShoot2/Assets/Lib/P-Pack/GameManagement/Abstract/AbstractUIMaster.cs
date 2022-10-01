using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractUIMaster : MonoBehaviour
{

  [SerializeField]
  protected Transform uiToBeCleanedParent = null;

  [SerializeField]
  protected Transform uiNotCleanedParent = null;

  [SerializeField]
  protected Camera uiCamera = null;

  protected RectTransform myCanvasRect = null;

  protected AbstractGameMaster gm = null;

  protected bool hasToUpdate = false;

  // Update is called once per frame
  void Update()
  {
    if (hasToUpdate)
      UpdateUIMaster();
  }

  protected virtual void UpdateUIMaster()
  {

  }

  public void Setup(AbstractGameMaster _gm)
  {
    gm = _gm;
    gm.OnGameStateHasChangedComplete += OnGameStateChanges;
  }

  protected virtual void OnGameStateChanges(GameState oldState, GameState newState)
  {
    switch (newState)
    {
      case GameState.NONE:
        break;
      case GameState.LAUNCHED:
        OnGameLaunched();
        break;
      case GameState.LOADED:
        OnGameLoaded();
        break;
      case GameState.STARTED:
        OnGameStart();
        break;
      case GameState.ENDED:
        OnGameEnds();
        break;
      default:
        break;
    }
  }

  protected virtual void OnGameLaunched()
  {
    myCanvasRect = GetComponent<RectTransform>();
  }

  protected virtual void OnGameLoaded()
  {

  }

  protected virtual void OnGameStart()
  {

  }

  protected virtual void OnGameEnds()
  {

  }

  public Popup AddPopup(Popup popup)
  {
    return AddPopup(popup, false);
  }

  public virtual Popup AddPopup(Popup popup, bool ignoresCleaning)
  {
    GameObject instance = GameObject.Instantiate(popup.gameObject);
    instance.transform.SetParent(ignoresCleaning ? uiNotCleanedParent : uiToBeCleanedParent, false);

    Popup popupInstance = instance.GetComponent<Popup>();

    return popupInstance;
  }

  public virtual void CleanAll()
  {
    for (int i = 0; i < uiToBeCleanedParent.childCount; i++)
    {
      Destroy(uiToBeCleanedParent.GetChild(i).gameObject);
    }
  }

  public virtual void RestartGame()
  {
    GameMaster.Instance.RestartGame();
  }

}
