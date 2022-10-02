using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GamePlayer : AbstractCharacter
{
    public PlayerInputHandler playerInputHandler;
    public CameraShaker m_playerCameraShaker;
    public PlayerCharacterController playerCharacterController;
    public CharacterController characterController;
    protected Health m_Health;
    public Weapon weapon;
    GameMaster gameMaster;
    UIMaster uiMaster;
    public float startChromaticAberrationValue = 0.262f;
    public float onHitChromaticAberrationAdd = 0.6f;
    public float startVignetteValue = 0.262f;
    public float onHitVignetteAdd = 0.6f;
    public float chromaticFalloffSmooth = 0.3f;
    public float vignetteFalloffSmooth = 0.1f;
    public ShakeOptions onHitShake = new ShakeOptions();

    public Volume cameraVolume;
    ChromaticAberration chromaticAberration;
    Vignette vignette;

    public override void OnSpawn(AbstractGameMaster gm)
    {
        base.OnSpawn(gm);
        gameMaster = gm as GameMaster;
        uiMaster = gm.uiMaster as UIMaster;

        gameMaster.onGameStarts += OnLevelStarts;
        gameMaster.gameLevel.playerModeHandler.onModeChanged += OnModeChanged;

        ChromaticAberration tmp;
        if (cameraVolume.profile.TryGet<ChromaticAberration>(out tmp))
        {
            chromaticAberration = tmp;
        }

        Vignette tmpV;
        if (cameraVolume.profile.TryGet<Vignette>(out tmpV))
        {
            vignette = tmpV;
        }


        m_Health = GetComponent<Health>();
        m_Health.OnDamaged += OnDamaged;
        m_Health.OnDie += OnDie;
    }

    private void Update()
    {
        UpdateChromaticAberration();
    }

    void OnLevelStarts ()
    {
        Debug.Log("level starts");
        //SetEnabled(true);
        uiMaster.healthBar.UpdateHealth(Mathf.Max(0.05f, m_Health.GetRatio()));
    }

    void SetEnabled (bool _state)
    {
        if(_state)
        {
            playerInputHandler.Enable();
        }
        else
        {
            playerInputHandler.Disable();
        }

        playerInputHandler.enabled = _state;
        playerCharacterController.enabled = _state;
        characterController.enabled = _state;
        weapon.SetEnabled(_state);
    }

    void OnModeChanged (PlayerMode mode)
    {
        SetEnabled(mode == PlayerMode._FPS);
    }

    void OnDamaged (float damageTaken, GameObject damageSource)
    {
        Debug.Log("damaged!");
        chromaticAberration.intensity.value += onHitChromaticAberrationAdd;
        vignette.intensity.value += onHitVignetteAdd;

        LaunchShake();

        uiMaster.healthBar.UpdateHealth(Mathf.Max(0.05f, m_Health.GetRatio()));
    }

    void OnDie ()
    {
        m_Health.OnDamaged -= OnDamaged;
        m_Health.OnDie -= OnDie;
        SetEnabled(false);
        gameMaster.EndGame("death");

        uiMaster.healthBar.UpdateHealth(0f);
    }

    void UpdateChromaticAberration ()
    {
        //float value = Mathf.Lerp(chromaticAberration.intensity.value, startChromaticAberrationValue, chromaticFalloffSmooth);
        //chromaticAberration.intensity.value = value;
        float value = Mathf.Lerp(vignette.intensity.value, startVignetteValue, vignetteFalloffSmooth);
        vignette.intensity.value = value;
    }
    void LaunchShake()
    {
        m_playerCameraShaker.LaunchShake(onHitShake);
    }

    private void OnDestroy()
    {
        gameMaster.onGameStarts -= OnLevelStarts;
        gameMaster.gameLevel.playerModeHandler.onModeChanged -= OnModeChanged;
    }
}
