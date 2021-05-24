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
    public ParticleSystem smokeEffect;

    public int health { get {return currentHealth; }}
    int currentHealth;
    Rigidbody2D rigidBody2D;
    Animator animator;
    bool broken = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!broken) {
            return;
        }
        timer -= Time.deltaTime;
        if(timer < 0) {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate() {
        if(!broken) {
            return;
        }
        Vector2 position = rigidBody2D.position;

        if(vertical) {
            position.y += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        } else {
            position.x += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
            
            
        rigidBody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other) {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if(player != null) {
            player.ChangeHealth(-1);
        }
    }

    public void Fix() {
        broken = false;
        rigidBody2D.simulated = false;
        smokeEffect.Stop();
        animator.SetTrigger("Fixed");
    }
}
