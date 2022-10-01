using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
  NONE,
  LAUNCHED,
  LOADED,
  STARTED,
  ENDED
}

public abstract class AbstractGameMaster : MonoBehaviour
{
  protected static AbstractGameMaster instance = null;
  public AbstractLevelMaster abstractLevelMaster = null;
  public AbstractUIMaster uiMaster = null;


  // Use this for initialization
  protected virtual void Awake()
  {
    instance = this;
  }

  protected virtual void Start()
  {
    Debug.Log("GameMaster :: Start");
    SetupUiMaster();
    InitLevelMaster();
    RegisterToEventToSpawnCharacter();
    StartGameStateManagement();
  }

  void StartWaitLoadingComplete()
  {
    Debug.Log("GameMaster - Loading starts");
    StartCoroutine(waitLoadingComplete());
  }


  IEnumerator waitLoadingComplete()
  {
    while (abstractLevelMaster.currentLevelIsLoaded == false)
    {
      yield return null;
    }

    SetGameState(GameState.LOADED);
    Debug.Log("GameMaster - Loading complete");
  }

  #region State Management
  public System.Action<GameState, GameState> OnGameStateHasChangedComplete = null;
  public System.Action<GameState> OnGameStateHasChanged = null;
  public System.Action onGameIsLaunched = null;
  public System.Action onGameIsLoaded = null;
  public System.Action onGameStarts = null;
  public System.Action onGameEnds = null;

  [ReadOnly]
  public GameState gameState = GameState.NONE;

  protected virtual void StartGameStateManagement()
  {
    onGameIsLaunched += StartWaitLoadingComplete;
    SetGameState(GameState.LAUNCHED);
  }

  public void SetGameState(GameState newState)
  {
    if (gameState != newState)
    {
      ChangeGameState(newState);
    }
  }

  void ChangeGameState(GameState newState)
  {
    GameState oldState = gameState;
    gameState = newState;
    if (OnGameStateHasChanged != null) OnGameStateHasChanged(newState);
    if (OnGameStateHasChangedComplete != null) OnGameStateHasChangedComplete(oldState, newState);

    switch (gameState)
    {
      case GameState.LAUNCHED:
        OnGameIsLaunched();
        break;
      case GameState.LOADED:
        OnGameIsLoaded();
        break;
      case GameState.STARTED:
        OnGameStarts();
        break;
      case GameState.ENDED:
        OnGameEnds();
        break;
    }
  }

  protected virtual void OnGameIsLaunched()
  {
    if (onGameIsLaunched != null) onGameIsLaunched();
  }

  protected virtual void OnGameIsLoaded()
  {
    if (onGameIsLoaded != null) onGameIsLoaded();
  }

  protected virtual void OnGameStarts()
  {
    if (onGameStarts != null) onGameStarts();
  }

  protected virtual void OnGameEnds()
  {
    if (onGameEnds != null) onGameEnds();
  }


  public virtual void RestartGame()
  {
    DestroyCharacter();
    DestroyCurrentLevel();
    uiMaster.CleanAll();
    SetGameState(GameState.LAUNCHED);
  }

  public virtual void StartGame()
  {
    SetGameState(GameState.STARTED);
  }

  public virtual void EndGame(string outcomeName)
  {
    Debug.Log("end");
    SetGameState(GameState.ENDED);
  }

  #endregion

  #region Level Management
  protected virtual void InitLevelMaster()
  {
    if (abstractLevelMaster == null) return;

    abstractLevelMaster.Init();

    onGameIsLaunched += abstractLevelMaster.OnGameIsLaunched;
    onGameStarts += abstractLevelMaster.OnGameIsStarted;
    onGameEnds += abstractLevelMaster.OnGameEnds;

  }

  protected virtual void DestroyCurrentLevel()
  {
    if (abstractLevelMaster.currentLevel)
      abstractLevelMaster.StopCurrentLevel();
    else
      Debug.LogWarning("[Game Master] No current level to destroy, is it normal?", this);
  }
  #endregion

  #region Character Spawning
  [Header("Character Spawning Options")]
  [SerializeField]
  GameObject characterPrefab = null;

  protected GameObject charInstance;

  [HideInInspector]
  public AbstractCharacter curAbstractCharacter;

  [SerializeField]
  GameState gameStateToSpawnCharacter = GameState.LOADED;

  void RegisterToEventToSpawnCharacter()
  {

    if (characterPrefab == null) return;

    switch (gameStateToSpawnCharacter)
    {
      case GameState.LAUNCHED:
        onGameIsLaunched += InstantiateCharacter;
        break;
      case GameState.LOADED:
        onGameIsLoaded += InstantiateCharacter;
        break;
      case GameState.STARTED:
        onGameStarts += InstantiateCharacter;
        break;
    }
  }

  protected virtual void InstantiateCharacter()
  {

    Transform spawnPoint = abstractLevelMaster.currentLevel.startSpawnPoint;

    if (spawnPoint != null)
      charInstance = GameObject.Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
    else
      charInstance = GameObject.Instantiate(characterPrefab);

    charInstance.SetActive(true);

    curAbstractCharacter = charInstance.GetComponent<AbstractCharacter>();
    curAbstractCharacter.OnSpawn(this);
  }

  public virtual void DestroyCharacter()
  {
    curAbstractCharacter.DestroyCharacter();
    charInstance = null;
  }
  #endregion

  void SetupUiMaster()
  {
    uiMaster.Setup(this);
  }
}
