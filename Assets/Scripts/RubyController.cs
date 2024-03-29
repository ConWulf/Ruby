using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.5f;
    public int maxHealth = 5;

    public int health { get {return currentHealth; }}
    int currentHealth;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    public GameObject projectilePrefab;

    Rigidbody2D rigidBody2D;
    float horizontal;
    float vertical;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    AudioSource audioSource;
    public AudioClip throwSound;
    public AudioClip hitSound;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip, 0.3f);
    }
    
    // Update is called once per frame
    void Update() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)){
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if(isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer <= 0) {
                isInvincible = false;
            }
        }

        if(Input.GetMouseButtonDown(0)) {
            launch();
        }

        if(Input.GetKeyDown(KeyCode.X)) {
            RaycastHit2D hit = Physics2D.Raycast(GetComponent<Rigidbody2D>().position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider != null) {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if(character != null) {
                    character.DisplayDialog();
                }
            }
        }
    }

    //FixedUpdate called at a fixed framerate; 
    //used for directly influincing physics components
    void FixedUpdate() {
        Vector2 position = rigidBody2D.position;
        position.y += speed * vertical * Time.deltaTime;
        position.x += speed * horizontal * Time.deltaTime;
        rigidBody2D.MovePosition(position);
    }

    //change health by the passed in amount
    public void ChangeHealth(int amount) {
        if(amount < 0) {
            animator.SetTrigger("Hit");
            if(isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            PlaySound(hitSound);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void launch() {
        GameObject projectileObject = Instantiate(projectilePrefab, 
        rigidBody2D.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
        PlaySound(throwSound);
    }
}
