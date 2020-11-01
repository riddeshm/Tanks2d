using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Action<GameObject> OnCollision;
    public float playerDamage = 10f;

    Rigidbody2D rb;
    float powerMultiplier = 2f;
    float power = 0f;
    float powerLimit = 12f;

    float groundDamageRadius = 0.7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    public void CalculatePower()
    {
        if(power < powerLimit)
        {
            power += powerMultiplier;
        }
    }

    public void Shoot(Vector3 initialPosition, Vector3 direction)
    {
        gameObject.SetActive(true);
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * power, ForceMode2D.Impulse);
        power = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision?.Invoke(collision.gameObject);
        gameObject.SetActive(false);
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                collision.gameObject.GetComponent<Ground>().DamageGround(contact.point, groundDamageRadius);
            }
        }
    }
}
