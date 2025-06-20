using UnityEngine;

public abstract class WeaponFireMode : MonoBehaviour
{
    public abstract void Fire(Transform firePoint, Vector2 direction);
}

