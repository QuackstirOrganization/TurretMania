using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float TimeToSpawn;
    public float Timer;
    public bool isTimer = true;

    // Start is called before the first frame update
    void Start()
    {
        //Timer = TimeToSpawn;

        //StartTimer();
        StartCoroutine(SpawnTime());
    }

    Vector3 RandomPostionInSquare()
    {
        //transform.localScale;
        Vector3 randPositionOnScale;

        float xRand = Random.Range(transform.position.x - (transform.localScale.x / 2), transform.position.x + (transform.localScale.x / 2));

        float yRand = Random.Range(transform.position.y - (transform.localScale.y / 2), transform.position.y + (transform.localScale.y / 2));

        randPositionOnScale = new Vector3(xRand, yRand, 0);

        return randPositionOnScale;
    }

    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(TimeToSpawn);
        Instantiate(Enemy, RandomPostionInSquare(), Quaternion.identity);
        StartCoroutine(SpawnTime());

    }

}
