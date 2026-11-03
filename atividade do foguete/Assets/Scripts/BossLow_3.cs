using UnityEngine;
public class BossLow_3 : BossBaseBehaviour {
    private float t;
    public override void OnStateUpdate(Animator a, AnimatorStateInfo s, int l) {
        if (bossHealth.IsInvulnerable) return;
        t += Time.deltaTime;
        if (t >= 0.75) {
            t = 0;
            for (int i = 0; i < 12; i++) {
                Vector2 dir = Quaternion.Euler(0, 0, i * 30f) * Vector2.up;
                Bullet b = bossBulletPool.Get(); b.transform.position = bossTransform.position; b.Setup(dir, 4f, 1, false);
            }
        }
    }
}
