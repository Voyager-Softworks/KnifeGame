using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using static PlayerManager;

/// <summary>
/// This class is responsible for managing the knife camera.
/// </summary>
public class KnifeCamera : MonoBehaviour
{

    public float m_mouseSensitivityX = 1f;
    public float m_mouseSensitivityY = 1f;

    private KnifeMovement knifeMovement { get { return PlayerManager.Instance?.m_knifeMovement; } }

    private void Update() {
        // null checks
        if (knifeMovement == null) {
            Debug.LogError("KnifeCamera.Update | knifeMovement is null.");
            return;
        }

        // lerp to be 3m away from knife
        transform.position = Vector3.Lerp(transform.position, knifeMovement.transform.position - transform.forward * 3, Time.deltaTime * 10);

        // get mouse movement
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        // rotate around knife
        transform.RotateAround(knifeMovement.transform.position, Vector3.up, mouseDelta.x * m_mouseSensitivityX);
        transform.RotateAround(knifeMovement.transform.position, transform.right, -mouseDelta.y * m_mouseSensitivityY);

        // look at knife
        transform.LookAt(knifeMovement.transform);
    }
}
