using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for the knife movement. 
/// </summary>
public class KnifeMovement : MonoBehaviour
{
    public float m_speed = 10f;
    public float m_rotationSpeed = 10f;

    private Rigidbody2D m_rigidbody2D;

    private void Awake() {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            m_rigidbody2D.velocity = Vector2.up * m_speed;
        }

        transform.Rotate(0f, 0f, -m_rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Log")) {
            m_rigidbody2D.velocity = Vector2.zero;
            m_rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.transform);
            Player.Instance.m_knifeMovement = this;
        }
    }
}
