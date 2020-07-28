using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 40f;

    private Animator animator;

    private Spawn spawn;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        spawn = GameObject.Find("SpawnManager").GetComponent<Spawn>();

        if(spawn == null)
        {
            Debug.LogError("Spawn Null");
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Audio Error");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime); //rotate
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            Destroy(collision.gameObject);

            animator.SetTrigger("OnExplosion");

            audioSource.Play();

            Destroy(this.gameObject,3f);

            spawn.StartSpawn();
        }
    }
}
