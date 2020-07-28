using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>(); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimation()
    {
        animator.SetTrigger("ThrusterPlay"); // Play Animation
    }

    public void StopAnimation()
    {
        animator.SetTrigger("ThrusterPlay"); // Stop Animation.
    }
}
