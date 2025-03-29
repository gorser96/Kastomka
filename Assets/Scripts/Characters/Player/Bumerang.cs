using UnityEngine;

public class Bumerang : MonoBehaviour
{
    public float speed = 60f; // Скорость вылета сферы
    public float maxDistance = 20f; // Максимальная дистанция, на которую должна пролететь сфера
    public float followDistance = 15f; // Расстояние, на котором сфера начнёт двигаться к объекту "Mob"
    private GameObject target; // Цель, к которой будет двигаться сфера (например, Cube с тегом "Mob")
    private GameObject player; // Игрок, к которому бумеранг будет возвращаться

    private Vector3 startPosition; // Начальная позиция
    private float traveledDistance = 0f; // Пройденное расстояние
    private Rigidbody rb;
    private bool isReturning = false; // Флаг, чтобы отслеживать, возвращается ли бумеранг

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Отключаем физику
        startPosition = transform.position;

        // Находим игрока
        player = GameObject.FindGameObjectWithTag("Player");

        // Находим ближайшую цель с тегом "Mob"
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        if (mobs.Length > 0)
        {
            target = FindClosestTarget(mobs);
        }
    }

    void Update()
    {
        if (target != null && !isReturning)
        {
            // Бумеранг двигается к Mob
            Vector3 direction = (target.transform.position - transform.position).normalized; // Направление к цели
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Если достигли цели, начинаем возвращаться к игроку
            if (Vector3.Distance(transform.position, target.transform.position) <= followDistance)
            {
                isReturning = true;
            }
        }
        else if (isReturning)
        {
            // Бумеранг двигается обратно к игроку
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Если бумеранг достиг игрока, удаляем его
            if (Vector3.Distance(transform.position, player.transform.position) <= followDistance)
            {
                Destroy(gameObject); // Удаляем бумеранг
            }
        }

        traveledDistance = Vector3.Distance(startPosition, transform.position);

        // Удаляем бумеранг, если он достиг максимальной дистанции
        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject); // Удаляет объект
        }
    }

    // Метод для нахождения ближайшего объекта Mob
    GameObject FindClosestTarget(GameObject[] mobs)
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject mob in mobs)
        {
            float distance = Vector3.Distance(transform.position, mob.transform.position);
            if (distance < closestDistance)
            {
                closestTarget = mob;
                closestDistance = distance;
            }
        }

        return closestTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Если мы столкнулись с игроком, игнорируем столкновение и не уничтожаем бумеранг
        if (collision.gameObject.CompareTag("Player"))
        {
            return; // Просто выходим из метода, не уничтожая объект
        }

        // Бумеранг не должен уничтожаться при столкновении с другими объектами
        // Можно добавить дополнительные условия для уничтожения при столкновении с определенными объектами, например:
        // if (collision.gameObject.CompareTag("Enemy")) { Destroy(gameObject); }
    }
}
