﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    int health, damage;
    int level, experience;
    void Start()
    {
        if (gameObject.tag == "Boss")
        {
            health = 10000;
            damage = 100;
        }
        else
        {
            level = Random.Range(1, (GameManager.playerLevel + 2));
            health = 50 * level;
            experience = 5 * level;
            damage = 5 * level;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            if (gameObject.tag == "Boss")
            {
                SceneManager.LoadScene("Victory");
            }
            else
            {
                GameManager.addExperience(experience);
                Debug.Log(GameManager.experience);
                Destroy(gameObject);
            }
        }
    }

    void removeHealth(int h)
    {
        health -= h;
    }

    bool isDead()
    {
        return health <= 0;
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Laser")
        {
            removeHealth(GameManager.damage);
            //Debug.Log(health);
            //Debug.Log(GameManager.damage);
        }
        else if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -100, 0) * 10);
            GameManager.removeHealth(damage);
        }
    }
}
