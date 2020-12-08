﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float stopDist;
    public float retreatDist;

    private Transform player;

    private float timeBtwShots;
    public float startTimeBtwShots;
    public Transform FirePoint;

    public GameObject projectile;
    public GameObject deathEffect;

    public int health = 100;

    public string shootNoise;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stopDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else if (Vector2.Distance(transform.position, player.position) < stopDist && Vector2.Distance(transform.position, player.position) > retreatDist)
        {
            transform.position = this.transform.position;
            animator.SetBool("isRunning", false);
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDist) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }

        if (timeBtwShots <= 0)
        {
            //shoot
            Instantiate(projectile, FirePoint.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("EnemyShoot");
            timeBtwShots = startTimeBtwShots;

        }
        else {
            timeBtwShots -= Time.deltaTime;
        }



    }

    public void TakeDamage( int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
