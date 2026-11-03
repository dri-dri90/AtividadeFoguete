using UnityEngine;
public class BossLow_2 : BossBaseBehaviour {
    private float t;
    public override void OnStateUpdate(Animator a, AnimatorStateInfo s, int l) {
        if (bossHealth.IsInvulnerable) return;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) {
            bossTransform.position = Vector3.Lerp(bossTransform.position, new Vector3(player.transform.position.x, bossTransform.position.y, 0), Time.deltaTime * 4f);
        }
        t += Time.deltaTime;
        if (t >= 0.2) { t = 0; Bullet b = bossBulletPool.Get(); b.transform.position = bossTransform.position; b.Setup(Vector2.down, 4f, 1, false); }
    }
}