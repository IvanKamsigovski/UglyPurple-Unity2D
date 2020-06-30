using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public float timeBetweenWaves = 1f;
    private float timeToSpawn = 2f;
    public MapLimmits Limmits;

    void Update()
    {
        if (Time.time >= timeToSpawn)
        {
            spawner();
            timeToSpawn = Time.time + timeBetweenWaves;
        }
        
    }

    void spawner ()
    {
        //Spawnanje neprijatelja
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                {
                    Instantiate(enemy1,
                        new Vector2(Random.Range(-Limmits.maxX, Limmits.maxX),
                        Random.Range(Limmits.maxY, Limmits.minY)), enemy1.transform.rotation);
                }break;
            case 1:
                {
                    Instantiate(enemy2,
                        new Vector2(Random.Range(-Limmits.maxX, Limmits.maxX),
                        Random.Range(Limmits.maxY, Limmits.minY)), enemy2.transform.rotation);
                }
                break;
            case 2:
                {
                    Instantiate(enemy3,
                        new Vector2(Random.Range(-Limmits.maxX, Limmits.maxX),
                        Random.Range(Limmits.maxY, Limmits.minY)), enemy3.transform.rotation);
                }
                break;
            default:
                    Instantiate(enemy1,
                        new Vector2(Random.Range(-Limmits.maxX, Limmits.maxX),
                        Random.Range(Limmits.maxY, Limmits.minY)), enemy1.transform.rotation);
                
                break;
        }
    }
}
