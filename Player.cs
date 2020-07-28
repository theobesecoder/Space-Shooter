using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Header("Tweakable Settings")]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _sppedMulti = 1.5f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private float fireRate = 0.25f;
    private float canFire = 0f;
    [SerializeField]
    [Header("Player Lives")]
    private int lives = 3;

    private Vector3 startPos =new  Vector3(0, -4.5f, 0);
    private Vector3 laserOffset = new Vector3(0, 0.82f, 0);

    private Spawn _spawn;
    [SerializeField]
    private bool tripleShootActive = false;
    [SerializeField]
    private bool speedBoostActive = false;
    [SerializeField]
    private bool sheildActive = false;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private int Score;

    [SerializeField]
    private GameObject leftEngine;
    [SerializeField]
    private GameObject rightEngine;
    

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip laserSound;
   
    

    private UIManager uIManager;


    


    void Start()
    {
        //Lets set the positon of the player to the origin when the game starts.
        transform.position = startPos;

        _spawn = GameObject.Find("SpawnManager").GetComponent<Spawn>();

        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        audioSource = GetComponent<AudioSource>();

        if(audioSource == null)
        {
            Debug.LogError("Audio Source");
        }
        else
        {
            audioSource.clip = laserSound;
        }
        

        if (_spawn == null)
        {
            Debug.Log("Spawn not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            //Time.time will tell us how long the game is running.
            //canFire = time + fireRate
            canFire = Time.time + fireRate;

            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    void CalculateMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalAxis, verticalAxis, 0); //Direction Vector3

        transform.Translate(direction * _movementSpeed * Time.deltaTime);

      

        //Limiting Up and Down Movement.
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.47f, -1.84f), transform.position.z);


        //Warping Left and Right
        if (transform.position.x <= -6.99f)
        {
            transform.position = new Vector3(6.99f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 6.99f)
        {
            transform.position = new Vector3(-6.99f, transform.position.y, transform.position.z);
        }
    }

    void Shoot()
    {
        //Laser shooting

        //check if the triple shot is active or not
        if (tripleShootActive)
        {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);       
        }

        audioSource.Play();
       
    }

    public void Damage()
    {
        if(sheildActive == true)
        {
            //Do Nothing
            sheildActive = false;

            shield.SetActive(false);

            return;
        }

        
        lives--;

        if(lives == 2)
        {
            leftEngine.SetActive(true);
        }
        if(lives == 1)
        {
            rightEngine.SetActive(true);
        }

        UpdateImage();

        if(lives < 1)
        {
            Destroy(this.gameObject);

            uIManager.GameOverText();

            _spawn.StopSpawning();            
        }
    }

   public void TripleShot()
   {
        //Activate
        tripleShootActive = true;
        //Disable after 5 secs
        StartCoroutine(coolDown());
   }

   private IEnumerator coolDown()
   {
        yield return new WaitForSeconds(5f);
        tripleShootActive = false;
   }

   public void SpeedBoost()
   {
        speedBoostActive = true;
        _movementSpeed = _movementSpeed * _sppedMulti;
        StartCoroutine(SpeedBoostCoolDown());
   }

   private IEnumerator SpeedBoostCoolDown()
   {
        yield return new WaitForSeconds(5f);
        speedBoostActive = false;
        _movementSpeed = _movementSpeed / _sppedMulti;
   }
   
   public void SheildPowerUp()
   {
        sheildActive = true;

        shield.SetActive(true);
   }

   public void AddScore(int points)
   {
        Score += points;//Add 10

        uIManager.UpdateScore(Score);
   }
   
   public void UpdateImage()
   {
        uIManager.UpdateImage(lives);
   }
   
}
