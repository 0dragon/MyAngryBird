using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int maxHp = 50;
    private int currentHp;
    private StageManager stageManager;
    private SoundManager soundManager;
    private AudioSource audioSource;
    
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        stageManager = FindObjectOfType<StageManager>();
        audioSource = GetComponent<AudioSource>();
        currentHp = maxHp;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bird") || other.gameObject.CompareTag("Enemy"))
        {
            Vector2 relativeVelocity = other.relativeVelocity;
            float impactSpeed = relativeVelocity.magnitude;
            
            int damage = Mathf.RoundToInt(impactSpeed);
            // Debug.Log("입은 데미지: " + damage);
            Damage(damage);
        }
    }

    void Damage(int damage)
    {
        currentHp -= damage;
        // Debug.Log("HP 감소: " + currentHp);
        
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(soundManager.enemyDestroySound, transform.position, 5.0f);
        Destroy(gameObject);
    }
}