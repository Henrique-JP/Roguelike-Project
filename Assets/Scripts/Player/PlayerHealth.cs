using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    private HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar = Object.FindFirstObjectByType<HealthBar>(); // Ou referencie direto se preferir

        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        Debug.Log("Player morreu!");
        // Aqui você pode chamar animação, tela de morte, desativar controles etc.
        Destroy(gameObject); // Destrói o objeto do jogador
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }
}
