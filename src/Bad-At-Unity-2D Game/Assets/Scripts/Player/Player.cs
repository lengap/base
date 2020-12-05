﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    public Animator animator;
    public GameObject deathEffect;
    //variables
    //public int respawn;
    public int battery;
    public bool emptyBattery = false;
    public int health;
    private int maxHealth = 100;
    public bool facingRight = true;
    private bool activeShield;
    private bool activeDamage;
    float speed = 3.0f;
    private float horizontal;
    private float vertical;
    
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
		health = PlayerPrefs.GetInt("PlayerCurrHealth");
		battery = PlayerPrefs.GetInt("PlayerCurrAmmo");
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(PlayerPrefs.GetInt("PlayerCurrHealth"));
        activeShield = false;
        activeDamage=false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
		animator.SetBool("Hurt", false);
        if(horizontal==0)
        {
            animator.SetFloat("Speed", Mathf.Abs(vertical));
        }
	
        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }

        else if (horizontal < 0 && facingRight)
        {
            Flip();
        }

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);

        if(Input.GetButtonDown("Fire1"))
        {
            if(!emptyBattery)
            {
                battery-= 1;
				PlayerPrefs.SetInt("PlayerCurrAmmo", battery);
            } else
            {
                checkBattery();
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    public void checkBattery()
    {
        if(battery > 1) 
        {
            emptyBattery = false;
        } else
        {
            emptyBattery = true;
        }
    }
    public void incBattery()
    {
        battery = battery + 25;
		PlayerPrefs.SetInt("PlayerCurrAmmo", battery);
    }


    public void TakeDamage(int damage)
    {
        if (activeShield == false)
        {

            health -= damage;
			PlayerPrefs.SetInt("PlayerCurrHealth", health);
            healthBar.SetHealth(health);
            animator.SetBool("Hurt", true);

            if (health <= 0)
            {
                Die();
            }
        }
    }
	public void HealDamage(int damage)
    {
        health += damage;
		PlayerPrefs.SetInt("PlayerCurrHealth", health);
        healthBar.SetHealth(health);
    }
    
    public void recovered()
    {
        animator.SetBool("Hurt", false);
    }

    public void activateShield()
    {
        activeShield = true;
    }
	
	public void deactivateShield()
    {
        activeShield = false;
    }

    public void activateDamage()
    {
        if(activeDamage  == false)
        {
            activeDamage = true;
        }

        else
        {
            activeDamage = false;
        }
        
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        SceneManager.LoadScene("Level");
    }
}
