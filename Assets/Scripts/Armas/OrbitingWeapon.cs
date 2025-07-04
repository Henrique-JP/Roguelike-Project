using UnityEngine;

public class OrbitingWeapon : MonoBehaviour
{
    public Transform player;
    public float orbitRadius = 2f;

    void Update()
    {
        // Converte posição do mouse para mundo
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        // Direção do jogador até o mouse
        Vector2 direction = (mouseWorldPos - player.position).normalized;

        // Calcula posição da arma ao redor do jogador
        Vector2 orbitPosition = (Vector2)player.position + direction * orbitRadius;
        transform.position = orbitPosition;

        // Faz a arma olhar para o mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flip horizontal quando o mouse está à esquerda do jogador
        Vector3 scale = transform.localScale;
        scale.y = (mouseWorldPos.x < player.position.x) ? -1f : 1f; //a escala é a atual baseada na pistola, posteriormente pode ser necessário fazer de forma mais modular
        transform.localScale = scale;
    }
}