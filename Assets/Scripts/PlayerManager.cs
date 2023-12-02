using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This singleton class is responsible for managing the player as a whole.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    // singleton
    public static PlayerManager Instance;

    public PlayerUI m_playerUI;
    public static PlayerUI s_playerUI { get { return Instance?.m_playerUI; } }
    public KnifeCamera m_knifeCamera;
    public static KnifeCamera s_knifeCamera { get { return Instance?.m_knifeCamera; } }
    public KnifeMovement m_knifeMovement;
    public static KnifeMovement s_knifeMovement { get { return Instance?.m_knifeMovement; } }

    private void Awake()
    {
        // singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}