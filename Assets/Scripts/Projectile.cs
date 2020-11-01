using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    float power = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    public void Shoot(Vector3 initialPosition, Vector3 direction)
    {
        gameObject.SetActive(true);
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * power, ForceMode2D.Impulse);
    }
}
