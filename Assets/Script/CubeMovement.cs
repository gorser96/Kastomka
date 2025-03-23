using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения куба
    public float rotationSpeed = 10f;
    private Transform player; // Ссылка на объект игрока
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
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

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Перемещаем куб в сторону игрока
            transform.position = Vector3.MoveTowards(transform.position, player.position , moveSpeed * Time.deltaTime);
            animator.SetBool("isWalk", true);
        }
    }
}
