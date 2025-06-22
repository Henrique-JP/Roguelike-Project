using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;      // Velocidade de movimento do inimigo
    public float stopDistance = 4f;   // Distância que o inimigo para de se mover do jogador
    public float attackRange = 6f;    // Distância máxima para o inimigo começar a atirar
    public float shootInterval = 1.5f; // Tempo entre cada tiro
    public GameObject bulletPrefab;    // Prefab do projétil que o inimigo vai atirar
    public Transform firePoint;       // Ponto de onde o projétil será instanciado (geralmente uma child do inimigo)

    private float shootTimer; // Contador para controlar o tempo entre os tiros

    void Start()
    {
        // Se o jogador não foi atribuído no Inspector, tenta encontrá-lo pela tag "Player"
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
            else
            {
                Debug.LogWarning("Jogador não encontrado! Certifique-se de que seu GameObject do jogador tem a tag 'Player'.");
            }
        }
    }

    void Update()
    {
        // Sai do Update se o jogador não for encontrado
        if (player == null) return;

        // Calcula a distância atual entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Lógica de Movimento: Segue o jogador se estiver fora da stopDistance
        if (distanceToPlayer > stopDistance)
        {
            // Move diretamente em direção ao jogador na velocidade definida
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        // Lógica de Atirar: Atira se estiver dentro da attackRange
        if (distanceToPlayer <= attackRange)
        {
            // Opcional: Faz o inimigo (ou o firePoint) olhar para o jogador
            LookAtPlayer();

            // Incrementa o timer de tiro
            shootTimer += Time.deltaTime;

            // Se o timer atingiu o intervalo, atira e reseta o timer
            if (shootTimer >= shootInterval)
            {
                ShootAtPlayer();
                shootTimer = 0f; // Reseta o timer para o próximo tiro
            }
        }
        else
        {
            // Se o inimigo estiver fora do attackRange, reseta o timer de tiro.
            // Isso evita que ele atire imediatamente ao entrar no alcance.
            shootTimer = 0f;
        }
    }

    // Função para fazer o inimigo (ou o ponto de tiro) olhar para o jogador
    void LookAtPlayer()
    {
        // Calcula a direção do inimigo para o jogador
        Vector2 direction = player.position - transform.position;
        // Calcula o ângulo em graus
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Aplica a rotação no eixo Z (para 2D)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Função para instanciar e lançar o projétil
    void ShootAtPlayer()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("O Prefab da Bala ou o Fire Point não foram atribuídos no script EnemyRanged!");
            return;
        }

        // Instancia a bala na posição e rotação do firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Pega o Rigidbody2D da bala para aplicar velocidade
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Calcula a direção exata para o jogador no momento do tiro
            Vector2 directionToPlayer = (player.position - firePoint.position).normalized;
            // Aplica uma velocidade à bala nessa direção
            rb.linearVelocity = directionToPlayer * 5f; // Ajuste 10f para a velocidade da sua bala
        }
        else
        {
            Debug.LogWarning("O Prefab da Bala não possui um componente Rigidbody2D!");
        }
    }
}