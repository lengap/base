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
    public int respawn;
    public int health = 100;
    private int maxHealth = 100;
    bool facingRight = true;
    float speed = 3.0f;
    private float horizontal;
    private float vertical;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
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
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
		animator.SetBool("Hurt", true);
        if (health <= 0)
        {
            Die();
        }
    }
	public void HealDamage(int damage)
    {
        health += damage;
        healthBar.SetHealth(health);
    }
    
    public void recovered()
    {
        animator.SetBool("Hurt", false);
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        SceneManager.LoadScene(respawn);
    }
}
