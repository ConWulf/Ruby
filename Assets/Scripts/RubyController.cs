using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RubyController : MonoBehaviour
{
    public float speed = 3.5f;
    public int maxHealth = 5;

    public int health { get {return currentHealth; }}
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
        position.y += speed * vertical * Time.deltaTime;
        position.x += speed * horizontal * Time.deltaTime;
        rigidBody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
