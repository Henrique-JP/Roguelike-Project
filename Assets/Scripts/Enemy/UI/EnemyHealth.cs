using UnityEngine;
using UnityEngine.UI;
using TMPro; // Para usar TextMeshPro

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    private float currentHealth;

    public GameObject damagePopupPrefab;
    public CanvasGroup healthBarCanvasGroup;
    public Slider healthBar;

    private WaveManager waveManager; // Adicione esta linha para a referência ao WaveManager

    void Awake()
    {
        currentHealth = maxHealth;
        if (healthBarCanvasGroup != null)
        {
            healthBarCanvasGroup.alpha = 0;
            healthBarCanvasGroup.interactable = false;
            healthBarCanvasGroup.blocksRaycasts = false;
        }
        UpdateHealthUI();

        // Encontra o WaveManager na cena (só precisa ser feito uma vez)
        waveManager = Object.FindFirstObjectByType<WaveManager>();
        if (waveManager == null)
        {
            Debug.LogWarning("WaveManager não encontrado na cena! O sistema de hordas pode não funcionar corretamente.");
        }
    }

    void Update()
    {
        if (healthBarCanvasGroup != null)
        {
            healthBarCanvasGroup.transform.rotation = Quaternion.identity; // Mantém a rotação fixa
            // Ou: healthBarCanvasGroup.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (healthBarCanvasGroup != null && healthBarCanvasGroup.alpha == 0)
        {
            healthBarCanvasGroup.alpha = 1;
        }

        currentHealth -= damageAmount;

        if (damagePopupPrefab != null && healthBarCanvasGroup != null) // Certifica-se que o CanvasGroup existe para ser o pai
        {
            // Posiciona o pop-up ligeiramente acima do inimigo
            GameObject popup = Instantiate(damagePopupPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, healthBarCanvasGroup.transform);
            DamagePopup damagePopup = popup.GetComponent<DamagePopup>();
            if (damagePopup != null)
            {
                damagePopup.Setup(damageAmount);
            }
        }

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " foi destruído!");

        // Notifica o WaveManager que um inimigo morreu
        if (waveManager != null)
        {
            waveManager.EnemyDied();
        }

        Destroy(gameObject); // Destrói o GameObject do inimigo
    }
}