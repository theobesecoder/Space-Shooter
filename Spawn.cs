using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject[] powerUp;
    [SerializeField]
    private GameObject container;

    private bool isPlayerAlive = true;

    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }
    //Corutine for spawning objects every 5 secons.
    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);
         
        while (isPlayerAlive == true) // dont stop when the player is dead.
        {
           
           Vector3 spawnPos = new Vector3(Random.Range(-5.4f,5.4f), 3f, 0);

            GameObject newEnemy = Instantiate(enemy, spawnPos, Quaternion.identity); // Spawn objects as new gameobjects

            newEnemy.transform.parent = container.transform; //To clean up parenting them in container.

            yield return new WaitForSeconds(3.0f);

           

        }
    }

    private IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(10f);
        while (isPlayerAlive == true) 
        {
            Vector3 spawnPos = new Vector3(Random.Range(-5.4f, 5.4f), 3f, 0);

            int randomPowerUp = Random.Range(0, 3);

            Instantiate(powerUp[randomPowerUp], spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(5, 10)); //random time between 4-8 seconds[Should spawn after the first spawn].
        }
    }

    public void StopSpawning()
    {
        isPlayerAlive = false;    
    }

}
