using UnityEngine;
using UnityEngine.Pool;

public abstract class BossBaseBehaviour : StateMachineBehaviour
{
    protected Transform bossTransform;
    protected BossHealth bossHealth;
    protected ObjectPool<Bullet> bossBulletPool;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossTransform = animator.transform;
        bossHealth = animator.GetComponent<BossHealth>();
        
        BossPoolManager poolMgr = animator.GetComponent<BossPoolManager>();
        if (poolMgr != null) bossBulletPool = poolMgr.BulletPool;
    }
}    