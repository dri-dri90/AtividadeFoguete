using UnityEngine;
using UnityEngine.Pool;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float bulletSpeed = 14f;

    private IObjectPool<Bullet> bulletPool;
    private float nextFireTime;

    private void Awake()
    {
        if (Time.timeScale == 0) Time.timeScale = 1f;

        // Implementação oficial do ObjectPool da Unity (Requisito da Atividade)
        bulletPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: true,
            defaultCapacity: 20,
            maxSize: 50
        );
    }

    private Bullet CreateBullet()
    {
        if (bulletPrefab == null) return null;

        Bullet b = Instantiate(bulletPrefab);
        b.SetPool(bulletPool);
        return b;
    }

    private void OnGetBullet(Bullet b)
    {
        if (b == null) return;
        
        // Posição de nascimento: Posição do FirePoint no mundo
        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        b.transform.position = spawnPos;
        b.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet b)
    {
        if (b == null) return;
        b.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet b)
    {
        if (b != null) Destroy(b.gameObject);
    }

    private void Update()
    {
        // Requisito da atividade: atira ao premir ou ao manter pressionado com intervalo
        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1")) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("ARRASPE O BULLETOBJ PARA O CAMPO BULLET PREFAB NO INSPECTOR!");
            return;
        }

        Bullet bullet = bulletPool.Get();
        if (bullet != null)
        {
            bullet.Setup(Vector2.up, bulletSpeed, 10, true);
        }
    }
}