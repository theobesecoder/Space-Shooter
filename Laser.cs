using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Speed of Laser
    [SerializeField]
    [Header("Laser Speed")]
    private float _speed = 8f;

    [SerializeField]
    private float eLaserSpeed = 5f;
    

    private bool isEnemyLaser = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemyLaser == false)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }
    }

    void MoveDown()
    {
        //Laser going up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 4.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            //Destroy in Seconds
            Destroy(gameObject);
        }
    }

    void MoveUp()
    {
        //Laser going up
        transform.Translate(Vector3.down * eLaserSpeed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            //Destroy in Seconds
            Destroy(gameObject);
        }
    }

    public void AssignLaser()
    {
        isEnemyLaser = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isEnemyLaser == true)
        {
            Player player = collision.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
        }
    }
}
