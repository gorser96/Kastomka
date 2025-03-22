using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Префаб сферы (проекта)
    public Transform firePoint; // Точка, из которой будет вылетать сфера
    public float shootCooldown = 0.5f; // Время между выстрелами (в секундах)
    private float lastShootTime = 0f;
    void Update()
    {
        if (Time.time > lastShootTime + shootCooldown)
        {
            Shoot(); // Вызываем метод для выстрела
            lastShootTime = Time.time; // Обновляем время последнего выстрела
        }
    }
    public void Shoot()
    {
        // Создаем сферу в точке выстрела
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Дополнительные настройки для движения, если нужно
    }
}
