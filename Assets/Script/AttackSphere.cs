using UnityEngine;

public class AttackSphere : MonoBehaviour
{
    public float speed = 60f; // Скорость вылета сферы
    public float maxDistance = 20f; // Максимальная дистанция, на которую должна пролететь сфера
    public float followDistance = 15f; // Расстояние, на котором сфера начнёт двигаться к объекту "Mob"
    private GameObject target; // Цель, к которой будет двигаться сфера (например, Cube с тегом "Mob")

    private Vector3 startPosition; // Начальная позиция
    private float traveledDistance = 0f; // Пройденное расстояние
    private Rigidbody rb;

    void Start()
    {
        // Запоминаем начальную позицию для расчета расстояния
        rb = GetComponent<Rigidbody>();
        // Устанавливаем Rigidbody как кинематический, чтобы избежать отталкивания
        rb.isKinematic = true;
        startPosition = transform.position;

        // Находим объект с тегом "Mob" (Cube)
         GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");

        // Если объекты с тегом "Mob" найдены, находим ближайший
        if (mobs.Length > 0)
        {
            target = FindClosestTarget(mobs);
        }
        
    }

    void Update()
    {
        // Если цель найдена и она в пределах заданного расстояния, начинаем движение к ней
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= followDistance)
        {
            // Двигаемся к цели
            Vector3 direction = (target.transform.position - transform.position).normalized; // Направление к цели
            transform.Translate(direction * speed * Time.deltaTime, Space.World); // Двигаем сферу
        }
        else
        {
            // Вычисляем движение сферы, если цель не активна (по-прежнему двигаемся на максимальное расстояние)
            float step = speed * Time.deltaTime;
            transform.Translate(Vector3.forward * step);
        }

        // Вычисляем пройденное расстояние
        traveledDistance = Vector3.Distance(startPosition, transform.position);

        // Если сфера преодолела максимальную дистанцию, удаляем её
        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject); // Удаляет объект
        }
    }

    GameObject FindClosestTarget(GameObject[] mobs)
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity; // Начинаем с максимально возможной дистанции

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
        // Удаляем сферу при столкновении с любым объектом
        Destroy(gameObject);
    }
}