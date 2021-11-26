using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject theEnemy;
    private int enemyCount;

    private void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount < 10)
        {
            Instantiate(theEnemy, new Vector3(transform.position.x + Random.Range(1, 40), 1.75f, transform.position.z + Random.Range(1, 40)), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            enemyCount += 1;
        }
    }
}
