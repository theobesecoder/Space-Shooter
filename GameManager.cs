﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)&& isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void GameOver()
    {

        Debug.Log("Called");
        isGameOver = true;
    }
}
