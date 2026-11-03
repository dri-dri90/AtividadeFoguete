using UnityEngine;

public class BossHigh_3 : BossBaseBehaviour {
    private float t;
    private Vector3 startPosition;
    private bool initialized;

    public override void OnStateUpdate(Animator a, AnimatorStateInfo s, int l) {
        if (bossHealth.IsInvulnerable) return;

        // Guarda a posição inicial do Boss no primeiro frame
        if (!initialized) {
            startPosition = bossTransform.position;
            initialized = true;
        }

        // --- MOVIMENTO SUAVE (Vai e vem a partir da posição inicial) ---
        float offset = Mathf.Sin(Time.time * 2f) * 3f;
        bossTransform.position = startPosition + new Vector3(offset, 0, 0);

        // --- LÓGICA DE TIRO (1 bala por vez) ---
        t += Time.deltaTime;
        if (t >= 1.2f) {
            t = 0;

            Bullet b = bossBulletPool.Get();
            b.transform.position = bossTransform.position;
            b.Setup(Vector2.down, 5f, 1, false);
        }
    }
}