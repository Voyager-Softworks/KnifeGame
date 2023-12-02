using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This singleton class is responsible for managing the player as a whole.
/// </summary>
public class Player : MonoBehaviour
{
    // singleton
    public static Player Instance;

    public PlayerUI m_playerUI;
    public KnifeMovement m_knifeMovement;

    private void Awake() {
        // singleton
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
}
