using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int currentLives = 900;
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private GameObject[] lifeIcons;

    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();

        // Garante que o painel de derrota começa escondido
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(false);
        }

        UpdateUI();
    }

    public void TakeDamage()
    {
        currentLives--;
        UpdateUI();

        // Morre assim que as vidas chegam a 0 (3º tiro)
        if (currentLives <= 0)
        {
            Die();
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (lifeIcons[i] != null)
            {
                lifeIcons[i].SetActive(i < currentLives);
            }
        }
    }

    private void Die()
    {
        // 1. Ativa a tela de derrota primeiro
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }

        // 2. Pausa o jogo
        Time.timeScale = 0f;

        // 3. Esconde o Sprite e desativa a colisão da nave
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (playerCollider != null) playerCollider.enabled = false;

        // 4. Desativa o GameObject do Player
        gameObject.SetActive(false);
    }
}