using UnityEngine;

public class Bullet : MonoBehaviour
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
        if (other.CompareTag("Enemy"))
        {
            // Tenta obter o componente EnemyHealth do objeto colidido
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            // Se o inimigo tiver o script EnemyHealth, aplica o dano
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Chama o método TakeDamage no inimigo
            }

            // Destrói a bala após colidir com o inimigo
            Destroy(gameObject);
        }
        // Opcional: Destruir a bala se ela colidir com algo que não seja o inimigo (ex: paredes)
        else if (other.CompareTag("Wall") || other.CompareTag("Ground")) // Adicione as tags dos seus obstáculos
        {
             Destroy(gameObject);
        }
    }
}
