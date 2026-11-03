using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private Vector2 direction = Vector2.up;
    private float speed = 14f;
    private int damage = 10;
    private bool isPlayerBullet = true;

    private IObjectPool<Bullet> pool;

    public void SetPool(IObjectPool<Bullet> bulletPool)
    {
        pool = bulletPool;
    }

    public void Setup(Vector2 dir, float spd, int dmg, bool fromPlayer = true)
    {
        direction = dir.normalized;
        speed = spd;
        damage = dmg;
        isPlayerBullet = fromPlayer;
    }

    private void Update()
    {
        // Garante que a bala voa na direção correta no espaço do MUNDO
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Se a bala sair dos limites visíveis da tela, devolve à Pool
        if (Mathf.Abs(transform.position.y) > 12f || Mathf.Abs(transform.position.x) > 15f)
        {
            ReleaseToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignora colisões da bala do Player com o próprio Player
        if (isPlayerBullet && collision.CompareTag("Player")) return;

        if (isPlayerBullet)
        {
            if (collision.TryGetComponent<BossHealth>(out var boss))
            {
                boss.TakeDamage(damage);
                ReleaseToPool();
            }
        }
        else
        {
            if (collision.TryGetComponent<PlayerHealth>(out var player))
            {
                player.TakeDamage();
                ReleaseToPool();
            }
        }
    }

    private void ReleaseToPool()
    {
        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}