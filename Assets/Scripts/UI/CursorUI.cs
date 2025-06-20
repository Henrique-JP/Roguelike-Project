using UnityEngine;

public class CursorUI : MonoBehaviour
{
    public RectTransform crosshairUI;

    void Start()
    {
        // Esconde o cursor padrão
        Cursor.visible = false;
    }

    void Update()
    {
        // Atualiza a posição da mira na tela
        crosshairUI.position = Input.mousePosition;
    }
}
