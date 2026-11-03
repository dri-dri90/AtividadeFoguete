using UnityEngine;
using UnityEngine.Pool;

public class BossPoolManager : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint; // Opcional: cria um filho no Boss para ser o bico/ponto de tiro
    
    // Alterado para ObjectPool<Bullet> para ser compatível com o BossBaseBehaviour
    public ObjectPool<Bullet> BulletPool { get; private set; }

    private void Awake()
    {
        BulletPool = new ObjectPool<Bullet>(
            createFunc: () => {
                if (bulletPrefab == null) return null;
                Bullet b = Instantiate(bulletPrefab);
                b.SetPool(BulletPool);
                return b;
            },
            actionOnGet: b => {
                if (b != null)
                {
                    // Põe a bala na posição do FirePoint ou do próprio Boss
                    Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
                    b.transform.position = spawnPos;
                    b.gameObject.SetActive(true);
                }
            },
            actionOnRelease: b => {
                if (b != null) b.gameObject.SetActive(false);
            },
            actionOnDestroy: b => {
                if (b != null) Destroy(b.gameObject);
            },
            defaultCapacity: 50,
            maxSize: 200
        );
    }

    // Método facilitador para os StateMachineBehaviours chamarem:
    public void ShootBullet(Vector2 direction, float speed, int damage)
    {
        if (bulletPrefab == null || BulletPool == null) return;

        Bullet b = BulletPool.Get();
        if (b != null)
        {
            // Direção Vector2.down para o tiro descer em direção ao Player, e fromPlayer = false
            b.Setup(direction, speed, damage, false);
        }
    }
}