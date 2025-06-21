using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint; // ponto onde os tiros saem (um filho da arma)
    private WeaponFireMode fireMode;

    void Awake()
    {
        fireMode = GetComponent<WeaponFireMode>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = firePoint.position.z; // Garante que ambos estão no mesmo plano

            Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

            fireMode.Fire(firePoint, direction);
        }
    }
}