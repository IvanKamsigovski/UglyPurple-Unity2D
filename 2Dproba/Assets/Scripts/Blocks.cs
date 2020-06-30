using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    float speed = 2.0f;

    void Start()
    {
        // Ubrzavanje blokova
        GetComponent<EnemyControle>().speed += Time.timeSinceLevelLoad * speed;
        
    }


}
