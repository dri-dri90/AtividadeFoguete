using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 700f;
    private float currentHealth;

    [Header("UI do Boss")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private GameObject victoryPanel;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool IsInvulnerable { get; private set; }

    private enum HealthPhase { High, Medium, Low }
    private HealthPhase currentPhase = HealthPhase.High;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
            if (fillImage != null) fillImage.color = Color.green;
        }
    }

    public void TakeDamage(int amount)
    {
        TakeDamage((float)amount);
    }

    public void TakeDamage(float amount)
    {
        if (IsInvulnerable) return;

        currentHealth -= amount;
        if (healthSlider != null) healthSlider.value = currentHealth;

        float pct = (currentHealth / maxHealth) * 100f;

        if (currentPhase == HealthPhase.High && pct <= 50f && pct > 20f)
        {
            currentPhase = HealthPhase.Medium;
            TriggerPhaseTransition("ToMediumHealth", Color.yellow);
        }
        else if (currentPhase == HealthPhase.Medium && pct <= 20f && pct > 0)
        {
            currentPhase = HealthPhase.Low;
            TriggerPhaseTransition("ToLowHealth", Color.red);
        }
        else if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void TriggerPhaseTransition(string triggerName, Color barColor)
    {
        if (fillImage != null) fillImage.color = barColor;
        if (animator != null) animator.SetTrigger(triggerName);
        StartCoroutine(InvulnerabilityRoutine());
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        IsInvulnerable = true;
        Color orig = spriteRenderer != null ? spriteRenderer.color : Color.white;
        float elapsed = 0f;

        while (elapsed < 2.0f)
        {
            if (spriteRenderer != null) spriteRenderer.color = new Color(orig.r, orig.g, orig.b, 0.2f);
            yield return new WaitForSeconds(0.1f);
            if (spriteRenderer != null) spriteRenderer.color = orig;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }

        if (spriteRenderer != null) spriteRenderer.color = orig;
        IsInvulnerable = false;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}