using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    public int maxHealth = 3;

    public int health { get {return currentHealth; }}
    int currentHealth;
    Rigidbody2D rigidBody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0) {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate() {
        Vector2 position = rigidBody2D.position;

        if(vertical)
            position.y += Time.deltaTime * speed * direction;
        else
            position.x += Time.deltaTime * speed * direction;
            
        rigidBody2D.MovePosition(position);
    }
}
