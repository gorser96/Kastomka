using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Префаб сферы (проекта)
    public GameObject projectilePrefab1; // Префаб сферы (проекта)
    public Transform firePoint; // Точка, из которой будет вылетать сфера
    public float shootCooldown = 0.5f; // Время между выстрелами (в секундах)
    private float lastShootTime = 0f;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        var mobObjects = GameObject.FindGameObjectsWithTag("Mob");
        if (mobObjects.Length > 0)
        {
            // Создаем сферу в точке выстрела
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            animator.SetBool("attack", true);
        }
        else
        {
            animator.SetBool("attack", false);
        }
    }
}
