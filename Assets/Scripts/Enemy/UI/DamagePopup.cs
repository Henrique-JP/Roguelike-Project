using UnityEngine;
using TMPro; // Importar para TextMeshPro

public class DamagePopup : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float moveSpeed = 1f; // Velocidade com que o texto sobe
    private float disappearSpeed = 1f; // Velocidade com que o texto desaparece
    private float lifeTime = 1f; // Tempo total que o texto fica na tela

    private Color textColor;
    private Vector3 moveDirection;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textColor = textMesh.color; // Pega a cor inicial do texto
        moveDirection = new Vector3(0, 1f, 0); // Direção para cima
    }

    public void Setup(float damageAmount)
    {
        textMesh.SetText(damageAmount.ToString("F0")); // Define o texto como o valor do dano (sem decimais)
        // Opcional: Ajustar o tamanho da fonte ou cor com base no dano
        // textMesh.fontSize = 20 + damageAmount * 0.5f;
    }

    void Update()
    {
        // Move o texto para cima
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Diminui a opacidade do texto ao longo do tempo
        lifeTime -= Time.deltaTime;
        textColor.a = lifeTime; // A opacidade é baseada no tempo restante
        textMesh.color = textColor;

        // Se a opacidade for muito baixa, destrói o objeto
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}