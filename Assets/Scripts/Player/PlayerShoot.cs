using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting")]
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 0.2f;
    public int maxAmmo = 30;
    public AudioSource gunSound;

    [Header("References")]
    public Camera fpsCam;

    private float nextFireTime = 0f;
    public int currentAmmo;

    void Start() => currentAmmo = maxAmmo;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && currentAmmo > 0)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }

    void Shoot()
    {
        if (gunSound != null) gunSound.Play();
        currentAmmo--;
        Debug.Log($"Ammo: {currentAmmo}/{maxAmmo}");
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            // existing reactive target check
            IReactiveTarget target = hit.collider.GetComponentInParent<IReactiveTarget>();
            if (target != null)
            {
                target.ReactToHit();
                Debug.Log($"Hit {hit.collider.name}");
            }

            // ADD THIS - check for EnemyHealth
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        Debug.Log("Reloaded!");
    }
}