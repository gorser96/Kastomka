using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 25f; // Скорость движения
    public float rotationSpeed = 10f;
    private Rigidbody rb; // Ссылка на Rigidbody компонента
    private Vector2 moveInput; // Вектор для хранения ввода
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody
        rb.freezeRotation = true;  // Останавливаем вращение объекта
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Получаем ввод с клавиш WASD или стрелок с использованием нового Input System
        float horizontal = 0f;
        float vertical = 0f;

        // Проверяем нажатие клавиш для оси X (влево/вправ)
        if (Keyboard.current.aKey.isPressed)
            horizontal = -1;
        else if (Keyboard.current.dKey.isPressed)
            horizontal = 1;

        // Проверяем нажатие клавиш для оси Z (вверх/вниз)
        if (Keyboard.current.wKey.isPressed)
            vertical = 1;
        else if (Keyboard.current.sKey.isPressed)
            vertical = -1;

        // Направление движения по двум осям
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        if (moveDirection.magnitude > 0.1f)
        {
            // Рассчитываем поворот, чтобы персонаж смотрел в сторону движения
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            // Плавный поворот персонажа с использованием Lerp
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Используем Rigidbody для движения с учетом физики
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        
        var attackVar = animator.GetBool("attack");
        if (moveDirection.magnitude > 0.1f)
        {
            if (attackVar)
            {
                animator.SetFloat("walkAndAttack", 0.5f);
            }
            else
            {
                animator.SetFloat("walkAndAttack", 0.1f);
            }
        }
        else
        {
            animator.SetFloat("walkAndAttack", attackVar ? 1f : 0f);
        }
    }
}
