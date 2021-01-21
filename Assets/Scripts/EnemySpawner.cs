using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Harald;
    GameObject[] spawnEnemyPoints;
    GameObject currentPoint;
    int index;

    private void Start()
    {
        spawnEnemyPoints = GameObject.FindGameObjectsWithTag("enemypoint");
        index = Random.Range(0, spawnEnemyPoints.Length);
        currentPoint = spawnEnemyPoints[index];
        Instantiate(Harald, currentPoint.transform);
        print(currentPoint.name);
    }
}
