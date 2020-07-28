using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float _eSpeed = 4f;

    [SerializeField]
    private Player player;

    [SerializeField]
    private Animator animator;

    private AudioSource audioSource;

    [SerializeField]
    private GameObject enemyLaserPrefab;

    private float canFire = -1f;
    private float FireRate = 3f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        if(audioSource == null)
        {
            Debug.LogError("Audio Error");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        EnemyLaser();
    }

    void EnemyLaser()
    {
        if (Time.time > canFire)
        {
            FireRate = Random.Range(7f, 11f);

            canFire = Time.time + FireRate;

            GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);

            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignLaser();
            }
        }
    }

    void Movement()
    {
        //Move down with defined speed
        transform.Translate(Vector3.down * Time.deltaTime * _eSpeed);
        //Respawn if alive at the top of the screen when it reaches botton


        if (transform.position.y < -6.5f)
        {
            //Random Positon Respawn
            //y = 3.5f
            // X = random
            transform.position = new Vector3(Random.Range(-5.94f, 5.94f), 3.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //player
        if(other.gameObject.name == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                
                //Damage Player
                Debug.Log("Damage");
            }
            animator.SetTrigger("OnDeath"); //Play Animation

            Destroy(GetComponent<Collider2D>());

            audioSource.Play();

            _eSpeed = 0f;
            //Destroy self
            Destroy(this.gameObject,2.8f);
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            
            //Laser
            Destroy(other.gameObject);
            //Add 10 to the score.
            if(player != null)
            {
                player.AddScore(10);
            }

            animator.SetTrigger("OnDeath"); //Play Animation

            Destroy(GetComponent<Collider2D>());

            audioSource.Play();

            _eSpeed = 0f;
            //Self
            Destroy(this.gameObject,2.8f);
        }
    }

    void EnemyShoot()
    {
        
    }
}
