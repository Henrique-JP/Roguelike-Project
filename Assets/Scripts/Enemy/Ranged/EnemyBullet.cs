using UnityEngine;
public class EnemyBullet : MonoBehaviour
{
    public int damage = 1; // Quanto de dano essa bala causa

    // Tempo de vida da bala para que ela não fique para sempre no cenário
    public float lifeTime = 3f;

    void Start()
    {
        // Destrói a bala após 'lifeTime' segundos
        Destroy(gameObject, lifeTime);
    }

    // Chamado quando a bala colide com outro objeto
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto colidido tem a tag "Enemy"
        if (other.CompareTag("Player")) // Verifica se colidiu com o jogador
        {
            // Tenta obter o componente PlayerHealth do objeto colidido
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // Se o jogador tiver o script PlayerHealth, aplica o dano
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Chama o método TakeDamage no jogador
            }

            // Destrói a bala após colidir com o jogador
            Destroy(gameObject);
        }
        // Opcional: Destruir a bala se ela colidir com algo que não seja o inimigo (ex: paredes)
        else if (other.CompareTag("Wall") || other.CompareTag("Ground")) // Adicione as tags dos seus obstáculos
        {
             Destroy(gameObject);
        }
    }
}
