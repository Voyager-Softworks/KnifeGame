using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

using static PlayerManager;

/// <summary>
/// This class is responsible for managing the player UI.
/// </summary>
public class PlayerUI : MonoBehaviour
{
    public Transform m_crosshair;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // null checks
        if (s_knifeCamera == null) {
            Debug.LogError("PlayerUI.Update | knifeCam is null.");
            return;
        }
        if (s_knifeMovement == null) {
            Debug.LogError("PlayerUI.Update | knifeMovement is null.");
            return;
        }

        // if aiming, enable crosshair
        if (s_knifeCamera.gameObject.activeSelf && s_knifeMovement.IsAiming) {
            ShowCrosshair(true);
        } else {
            ShowCrosshair(false);
        }
    }

    public void ShowCrosshair(bool show)
    {
        m_crosshair.gameObject.SetActive(show);
    }
}
