﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public bool gamePaused = false;
    public GameObject pauseMenu;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
            }

            else
            {
                pauseMenu.SetActive(false);
                gamePaused = false;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
        }
    }
}