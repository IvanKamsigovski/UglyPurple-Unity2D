using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControle : MonoBehaviour
{

    public bool directionSwich;
    public bool canShoot;
    public float shootSpeed;
    public float speed;
    public float changeTimer;
    public int hp;
    public int scoreReward;
    float maxTimer;
    float shootTimer;
    float maxshootTimer;
    public MapLimmits Limmits;
    public Transform shootPosition;
    Rigidbody2D rig;
    public GameObject particle;
    public GameObject bullet;
    public GameObject powerUp;
    
    void Start()
    {
        //tajmer za pucanje neprijatelja
        shootTimer = Random.Range(1f, 3f);
        maxshootTimer = shootTimer;

        rig = GetComponent<Rigidbody2D>();
        maxTimer = changeTimer;
    }

    // Update is called once per frame
    void Update()
    {
        switchTimer();
        Movement();
        //Odbijanje od grnice
        if (transform.position.x == Limmits.maxX) switchDirection(directionSwich);
        if (transform.position.x == Limmits.minx) switchDirection(directionSwich);

        //Ogranicavanje na granice
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, Limmits.minx, Limmits.maxX),
            Mathf.Clamp(transform.position.y, Limmits.minY, Limmits.maxY));

        //neprijateljsko pucanje
        shootTimer -= Time.deltaTime;
        if (canShoot)
        {
            if (shootTimer <= 0)
            {
                GameObject newBullet = Instantiate(bullet, shootPosition.transform.position, transform.rotation);
                newBullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * shootSpeed;
                shootTimer = maxshootTimer;
            }
        }

    }

    //Kretanje neprijatelja
    void Movement()
    {
        if (directionSwich)
            rig.velocity = new Vector2(speed * Time.deltaTime, -speed * Time.deltaTime);
        else
            rig.velocity = new Vector2(-speed * Time.deltaTime, -speed * Time.deltaTime);
    }

    //Vremenski namjestanje promjene smjera
    void switchTimer()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            switchDirection(directionSwich);
            changeTimer = maxTimer;
        }
    }

    //namjestanje odbijanja
    bool switchDirection(bool dir)
    {
        if (dir) directionSwich = false;
        else directionSwich = true;
        return directionSwich;
    }



    //Unistavanje neprijatelja
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FriendlyBullet")
        {
            //particle
            Instantiate(particle, transform.position, transform.rotation);

            Destroy(col.gameObject); //Unistavanje metka pri udaru
            hp--;
            if (hp <= 0)
            {
                //Dodavanje powerUP na random vrijednost
                int rand = Random.Range(0, 100);
                if (rand < 15) Instantiate(powerUp, transform.position, powerUp.transform.rotation);
                //--------------------------------------------------------------------------------
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().score
                    += scoreReward;// Dodavanje scora za unistavanje neprijatelja
                Destroy(gameObject); //Unistavanje neprijatelja
            }
        }
        //Unistavanje igraca
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().hp--;
            hp--; ///Steta na neprijatelju pri udaru

            if (hp <= 0)
            {
                //Score kad unistimo neprijatelja
                col.gameObject.GetComponent<Player>().score += scoreReward;
                Destroy(gameObject);
            }
        }
    }
}
