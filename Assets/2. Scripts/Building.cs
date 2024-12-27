using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int maxHp = 50;
    private int currentHp;
    private StageManager stageManager;
    
    void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
        currentHp = maxHp;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bird") || other.gameObject.CompareTag("Enemy"))
        {
            Vector2 relativeVelocity = other.relativeVelocity;
            float impactSpeed = relativeVelocity.magnitude;
            
            int damage = Mathf.RoundToInt(impactSpeed);
            Debug.Log("입은 데미지: " + damage);
            Damage(damage);
        }
    }

    void Damage(int damage)
    {
        currentHp -= damage;
        Debug.Log("HP 감소: " + currentHp);
        
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Animator animator = GetComponent<Animator>();
        // animator.Play("Dead");
        // Invoke("Destroy(gameObject)", 2f);
        Destroy(gameObject);
    }
}