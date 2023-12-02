using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This class is responsible for managing the player UI.
/// </summary>
public class PlayerUI : MonoBehaviour
{
    public Transform m_crosshair;
    
    public void ShowCrosshair(bool show)
    {
        m_crosshair.gameObject.SetActive(show);
    }
}
