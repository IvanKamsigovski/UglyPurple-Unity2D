using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    public int dropSpeed;


    void Update()
    {
        transform.Translate(Vector2.down * dropSpeed * Time.deltaTime);
    }
}
