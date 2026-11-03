using UnityEngine;

public class BossHigh_2 : BossBaseBehaviour {
    private float t;

    public override void OnStateUpdate(Animator a, AnimatorStateInfo s, int l) {
        if (bossHealth.IsInvulnerable) return;

        t += Time.deltaTime;
        if (t >= 1.2f) {
            t = 0;

            // Dispara apenas 1 bala centralizada
            Bullet b = bossBulletPool.Get();
            b.transform.position = bossTransform.position;
            b.Setup(Vector2.down, 7f, 1, false);
        }
    }
}