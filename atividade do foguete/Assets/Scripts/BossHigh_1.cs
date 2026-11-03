using UnityEngine;

public class BossHigh_1 : BossBaseBehaviour {
    private float t;

    public override void OnStateUpdate(Animator a, AnimatorStateInfo s, int l) {
        if (bossHealth.IsInvulnerable) return;

        t += Time.deltaTime;

        // Aumente este valor se quiser que atire ainda mais devagar (ex: 1.5f)
        if (t >= 1.5f) { 
            t = 0;

            // Dispara apenas UMA bala no centro
            Bullet b = bossBulletPool.Get();
            b.transform.position = bossTransform.position;
            b.Setup(Vector2.down, 6f, 1, false);
        }
    }
}