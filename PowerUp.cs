using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    
    //PowerUp ID 
    //0 - Triple Shot
    //1 - Speed
    //2 - Shield
    [SerializeField]
    private int powerUpID;


    [SerializeField]
    private AudioClip audioclip;

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if(transform.position.y <-6f)
        {
            Destroy(this.gameObject);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Player player = collision.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(audioclip, transform.position);

            if (player != null)
            {
                switch (powerUpID)
                {
                    case 0:
                        player.TripleShot();
                        break;

                    case 1:
                        player.SpeedBoost();
                        break;

                    case 2:
                        player.SheildPowerUp();
                        break;

                    default:
                        Debug.Log("Error PoweUp!");
                        break;
                        
                }
            }

            Destroy(this.gameObject);
        }
    } 
}
