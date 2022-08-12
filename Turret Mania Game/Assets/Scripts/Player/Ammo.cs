using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int CurrAmmo;
    public int MaxAmmo;

    public float GunRecoilSpeed;
    public bool canPickUp = false;
    public bool GunExist = false;

    [Header("Mouse Rotation")]
    public Vector3 angle;
    private Transform mousePos;

    [Header("SFX")]
    public AudioClip GunLossSFX;
    public AudioClip ReloadSFX;

    [Header("Game Objects")]
    public GameObject GunProjectile;
    public GameObject Turret;
    private SpriteRenderer TurretSprite;

    [Header("References")]
    private AudioSource AS;
    private Rigidbody2D Rb2D;

    //[Header("Events")]
    public event Action ReloadAction;
    public event Action ThrowAction;

    // Start is called before the first frame update
    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        AS = GetComponent<AudioSource>();
        TurretSprite = Turret.GetComponent<SpriteRenderer>();
        CurrAmmo = MaxAmmo;
        mousePos = GameObject.FindGameObjectWithTag("Mouse").transform;
    }

    public void RecoilWeapon()
    {
        if (GunExist)
        {
            return;
        }
        GunExist = true;

        AS.PlayOneShot(GunLossSFX);
        angle = (this.transform.position - mousePos.position);

        GameObject GunFlung = Instantiate(GunProjectile, transform.position, Quaternion.identity);
        GunFlung.GetComponent<Rigidbody2D>().velocity = new Vector3(Rb2D.velocity.x, Rb2D.velocity.y, 0) + -Turret.transform.up * GunRecoilSpeed;
        GunFlung.transform.rotation = Turret.transform.rotation;

        TurretSprite.enabled = false;

        if (ThrowAction != null)
            ThrowAction();

        canPickUp = false;
        Invoke("PickUp", 0.5f);
    }

    void PickUp()
    {
        canPickUp = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canPickUp)
        {
            return;
        }

        if (collision.CompareTag("Weapon"))
        {
            ReloadAction();

            TurretSprite.enabled = true;
            AS.PlayOneShot(ReloadSFX);
            Destroy(collision.gameObject);
            CurrAmmo = MaxAmmo;
            GunExist = false;
        }
    }


}
