using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    private Rigidbody2D rigidBody2D;
    private float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    //FixedUpdate called at a fixed framerate; 
    //used for directly influincing physics components
    void FixedUpdate() {
        Vector2 position = rigidBody2D.position;
        position.y += 3.5f * vertical * Time.deltaTime;
        position.x += 3.5f * horizontal * Time.deltaTime;
        rigidBody2D.MovePosition(position);
    }

    void ChangeHealth(int amount) {
        
    }
}
