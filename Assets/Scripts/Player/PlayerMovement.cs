using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator; // Adicionado

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Adicionado
    }

    void Update()
    {
        // Captura entrada de movimento (WASD ou setas)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normaliza o vetor para evitar velocidade maior nas diagonais
        movement = movement.normalized;

        // Atualiza parâmetro de animação
        if (animator != null)
        {
            animator.SetFloat("Speed", movement.sqrMagnitude); // Adicionado
        }
    }

    void FixedUpdate()
    {
        // Aplica movimento no Rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}