using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

using static PlayerManager;

/// <summary>
/// This class is responsible for the knife movement.
/// The knife IS the player, and can be thrown around while while the camera follows it.
/// </summary>
public class KnifeMovement : MonoBehaviour
{
    public float m_speed = 10f;
    public float m_awayForce = 1f;
    public float m_rotationSpeed = 10f;

    private Rigidbody m_rigidbody;

    [ReadOnly, SerializeField] private bool m_isAiming = false;
    public bool IsAiming { get { return m_isAiming; } }


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (s_knifeCamera == null)
        {
            Debug.LogError("KnifeMovement.Update | knifecam is null.");
            return;
        }

        // hold right click to aim
        if (Mouse.current.rightButton.isPressed)
        {
            m_isAiming = true;
        }
        else
        {
            m_isAiming = false;
        }

        // if right click is released, throw knife
        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            ThrowKnife();
        }
    }

    private void FixedUpdate() 
    {

        // if aiming
        if (m_isAiming)
        {
            // move knife away from any nearby objects
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
            foreach (Collider collider in colliders)
            {
                // skip self or children
                if (collider.gameObject == gameObject || collider.gameObject.transform.IsChildOf(transform))
                {
                    continue;
                }

                // get closest point on collider and move away from it
                Vector3 closestPoint = collider.ClosestPoint(transform.position);
                Vector3 awayDirection = transform.position - closestPoint;

                transform.position += awayDirection * m_awayForce * Time.fixedDeltaTime;
            }

            // orient knife towards camera direction
            Vector3 camDirection = s_knifeCamera.transform.forward;
            transform.rotation = Quaternion.LookRotation(camDirection, Vector3.up);
        }

        // ensure knife is not rotated around z axis
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    public void ThrowKnife()
    {
        // null checks
        if (s_knifeCamera == null)
        {
            Debug.LogError("KnifeMovement.ThrowKnife | knifecam is null.");
            return;
        }

        // get cam direction
        Vector3 camDirection = s_knifeCamera.transform.forward;

        // throw knife (dont mess up scale)
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        m_rigidbody.isKinematic = false;
        m_rigidbody.useGravity = true;
        m_rigidbody.velocity = camDirection * m_speed + Vector3.up * 2;

        // spin knife forwards along right axis
        m_rigidbody.angularVelocity = transform.right * m_rotationSpeed;
    }

    private void OnCollisionEnter(Collision other) {
        // if parent is not null, we are already stuck to something
        if (transform.parent != null) {
            return;
        }

        // stick to objects, dont mess up scale
        transform.SetParent(other.transform, true);
        transform.localScale = new Vector3(1 / other.transform.lossyScale.x, 1 / other.transform.lossyScale.y, 1 / other.transform.lossyScale.z);
        m_rigidbody.isKinematic = true;
        m_rigidbody.useGravity = false;
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = Vector3.zero;
    }
}
