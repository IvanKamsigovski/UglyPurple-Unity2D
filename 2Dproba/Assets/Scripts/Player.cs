using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float shootSpeed;
    public float mapWidth;
    public float mapLength;
    public int hp;
    public int score;
    public Text scoreText;
    public Text highscoreText;
    public Transform shootPoss1;
    public Transform shootPossL;
    public Transform shootPossR;
    public GameObject bullet;
    public AudioClip shootSound;
    int power;
    float powerTimer = 0f;
    float powerSeconds = 9f;
    AudioSource audioS;


    void Start()
    {
        power = 1;
        audioS = GetComponent<AudioSource>();
        highscoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
       
;    }

    // Update is called once per frame
    void Update()
    {



        scoreText.text = score.ToString(); //Score update

        //Namjestanje highscora da ne moze biti manji
        if (score > PlayerPrefs.GetInt("HighScore"))
        { 
        PlayerPrefs.SetInt("HighScore", score); //Highscore
            highscoreText.text = score.ToString();
        }
        if (hp <= 0)
        {
            FindObjectOfType<GameControler>().EndGame();//Zavrsetak igre
            Destroy(gameObject); //Unistavanje igraca
        }

        Movement();
        Shooting();

        //Vremensko ogranicenje powerUpa
        if (power <= 3 || power >= 1)
        {
            powerTimer += Time.deltaTime;
            if (powerTimer > powerSeconds)
            {
                powerTimer = 0;
                if (power > 1)
                    power--;
               
            }
        }
 
        //Ogranicavanje kretnje unutar granica mape
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -mapWidth, mapWidth)
            ,mapLength);
    }

    void Movement()
    {

       

        //Kretanje
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
    }

    void Shooting() //Pucanje
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioS.PlayOneShot(shootSound);//zvuk pri pucanju 
            // Spawn metka i power-upovi
            switch (power)
            {
                case 1:
                    {
                        GameObject Bullet1 = Instantiate(bullet, shootPoss1.transform.position, transform.rotation);
                        Bullet1.GetComponent<Rigidbody2D>().velocity = Vector2.up * shootSpeed;
                    }
                    break;
                case 2:
                    {
                        GameObject Bullet1 = Instantiate(bullet, shootPossL.transform.position, transform.rotation);
                        Bullet1.GetComponent<Rigidbody2D>().velocity = Vector2.up * shootSpeed;

                        GameObject Bullet2 = Instantiate(bullet, shootPossR.transform.position, transform.rotation);
                        Bullet2.GetComponent<Rigidbody2D>().velocity = Vector2.up * shootSpeed;

                    }
                    break;
                case 3:
                    {
                        GameObject Bullet1 = Instantiate(bullet, shootPoss1.transform.position, transform.rotation);
                        Bullet1.GetComponent<Rigidbody2D>().velocity = Vector2.up * shootSpeed;

                        GameObject Bullet2 = Instantiate(bullet, shootPossL.transform.position, transform.rotation);
                        Bullet2.GetComponent<Rigidbody2D>().velocity = Vector2.up * shootSpeed;

                        GameObject Bullet3 = Instantiate(bullet, shootPossR.transform.position, transform.rotation);
                        Bullet3.GetComponent<Rigidbody2D>().velocity = Vector2.up * shootSpeed;
                    }
                    break;
                default:
                    {
                        GameObject Bullet1 = Instantiate(bullet, shootPoss1.transform.position, transform.rotation);
                        Bullet1.GetComponent<Rigidbody2D>().velocity = Vector2.up * shootSpeed;
                    }break;

            }
        }
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyBullet")//Steta prilikom udara neprijateljskog metka
            hp--;
        //Power up
        if (col.gameObject.tag == "PowerUp")
        {

            if (power <= 3)
            {
                power++;
                Destroy(col.gameObject);
            }
         
        }
    }

    /*  void OnCollisionEnter2D()
     {
       FindObjectOfType<GameControler>().EndGame();
     }*/
}
