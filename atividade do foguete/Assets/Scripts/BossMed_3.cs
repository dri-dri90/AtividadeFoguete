using UnityEngine;
public class BossMed_3 : BossBaseBehaviour {
    private float t;
    public override void OnStateUpdate(Animator a, AnimatorStateInfo s, int l) {
        if (bossHealth.IsInvulnerable) return;
        t += Time.deltaTime;
        if (t >= 0.6) {
            t = 0;
            for (int i = -2; i <= 2; i++) {
                Vector2 dir = Quaternion.Euler(0, 0, i * 15f) * Vector2.down;
                Bullet b = bossBulletPool.Get(); b.transform.position = bossTransform.position; b.Setup(dir, 4f, 1, false);
            }
        }
    }
}