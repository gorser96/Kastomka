using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;  // Префаб Cube
    public float spawnAreaSize = 10f;  // Размер области, где будут появляться кубы
    public float spawnCooldown = 2f;  // Время между спавнами новых кубов
    private float lastSpawnTime = 0f;

    void Update()
    {
        // Проверяем, прошло ли достаточно времени с последнего появления куба
        if (Time.time > lastSpawnTime + spawnCooldown)
        {
            SpawnCube();
            SpawnCube();  // Создаем новый куб в случайной позиции
            lastSpawnTime = Time.time;  // Обновляем время последнего появления
        }
    }

    void SpawnCube()
    {
        // Генерируем случайную позицию внутри заданной области
        float x = Random.Range(-spawnAreaSize, spawnAreaSize);
        float y = 0f;  // Куб будет появляться на уровне земли, если вы хотите изменить высоту, установите y в другое значение
        float z = Random.Range(-spawnAreaSize, spawnAreaSize);

        // Создаем новый куб на случайной позиции
        Vector3 spawnPosition = new Vector3(x, y, z);
        Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
    }
}
