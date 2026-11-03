using UnityEngine;
public class BossLow_1 : BossBaseBehaviour {
    private float t, angle;
    public override void OnStateUpdate(Animator a, AnimatorStateInfo s, int l) {
        if (bossHealth.IsInvulnerable) return;
        t += Time.deltaTime;
        if (t >= 0.08) {
            t = 0; angle += 25f;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.down;
            Bullet b = bossBulletPool.Get(); b.transform.position = bossTransform.position; b.Setup(dir, 4f, 1, false);
        }
    }
}