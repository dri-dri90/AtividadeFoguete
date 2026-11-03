using UnityEngine;
public class BossMed_1 : BossBaseBehaviour {
    private float t;
    public override void OnStateUpdate(Animator a, AnimatorStateInfo s, int l) {
        if (bossHealth.IsInvulnerable) return;
        t += Time.deltaTime;
        if (t >= 0.4) {
            t = 0;
            Vector2[] dirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
            foreach (var d in dirs) { Bullet b = bossBulletPool.Get(); b.transform.position = bossTransform.position; b.Setup(d, 4.5f, 1, false); }
        }
    }
}