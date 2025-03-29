using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private int collisionCount = 0;  // Счетчик столкновений

    void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулся ли куб с объектом, тег которого "AttackSphere"
        if (collision.gameObject.CompareTag("AttackSphere"))
        {
            collisionCount++; // Увеличиваем счетчик столкновений

            // Если количество столкновений равно 3, уничтожаем куб
            if (collisionCount >= 3)
            {
                Destroy(gameObject);  // Уничтожаем куб
            }
        }
    }
}
