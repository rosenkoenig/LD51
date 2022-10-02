using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public PlayerInputHandler PlayerInputHandler;
    public Animator Animator;

    public Projectile projectile;

    public Transform shootHotspot;
    public Transform immobileHotspot;
    Vector3 currentHotspotPosition;
    Quaternion currentHotspotRotation;

    public AudioSource audioSource;
    public AudioClip shootSound;
    public GameObject shootVFX;

    
    public float recoilFactor = 0f;
    public float recoilAdd = 0.1f;
    public float recoilRecoverySpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.Instance.gameLevel.playerModeHandler.onModeChanged += OnModeChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInputHandler.GetFireInputDown())
        {
            Shoot();
        }

        UpdateShootHotspot();
    }

    void OnModeChanged(PlayerMode newMode)
    {
        transform.GetChild(0).gameObject.SetActive(newMode == PlayerMode._FPS);
    }

    void Shoot ()
    {
        Animator.Play("WEAPON_Shoot");
        CreateProjectile();
        audioSource.PlayOneShot(shootSound);
        recoilFactor += recoilAdd;
    }

    void CreateProjectile ()
    {
        GameObject projectileInst = Instantiate(projectile.gameObject, currentHotspotPosition, currentHotspotRotation) as GameObject;
        projectileInst.GetComponent<Projectile>().owner = PlayerInputHandler.gameObject;
        GameObject shootVFx = Instantiate(shootVFX, shootHotspot.position, shootHotspot.rotation) as GameObject;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(shootHotspot.position, shootHotspot.position + transform.forward, Color.black);
    }

    void UpdateShootHotspot ()
    {
        recoilFactor = Mathf.Clamp01(recoilFactor);

        currentHotspotPosition = Vector3.Lerp(immobileHotspot.position, shootHotspot.position, recoilFactor);
        currentHotspotRotation = Quaternion.Lerp(immobileHotspot.rotation, shootHotspot.rotation, recoilFactor);

        recoilFactor = Mathf.Lerp(recoilFactor, 0f, recoilRecoverySpeed * Time.deltaTime);
    }

    public void SetEnabled (bool _state)
    {
        enabled = _state;
        transform.GetChild(0).gameObject.SetActive(_state);
    }
}
