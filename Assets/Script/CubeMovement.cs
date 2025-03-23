using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения куба
    private Transform player; // Ссылка на объект игрока

    void Start()
    {
        // Находим объект с тегом "Player" и получаем его позицию
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Проверяем, если игрок существует
        if (player != null)
        {
            // Вычисляем направление от куба к игроку
            Vector3 direction = (player.position - transform.position).normalized;

            // Перемещаем куб в сторону игрока
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
