using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHp = 50;
    private int currentHp;
    
    void Start()
    {
        currentHp = maxHp;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bird") || other.gameObject.CompareTag("Building"))
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
