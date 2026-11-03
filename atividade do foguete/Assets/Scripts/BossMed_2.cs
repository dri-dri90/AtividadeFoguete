using UnityEngine;
public class BossMed_2 : BossBaseBehaviour {
    private float t;
    public override void OnStateUpdate(Animator a, AnimatorStateInfo s, int l) {
        if (bossHealth.IsInvulnerable) return;
        bossTransform.position += new Vector3(Mathf.Cos(Time.time * 5f) * 3f, 0, 0) * Time.deltaTime;
        t += Time.deltaTime;
        if (t >= 0.3) { t = 0; Bullet b = bossBulletPool.Get(); b.transform.position = bossTransform.position; b.Setup(Vector2.down, 4.5f, 1, false); }
    }
}