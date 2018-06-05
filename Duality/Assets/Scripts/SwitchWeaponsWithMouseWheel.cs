using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponsWithMouseWheel : MonoBehaviour {

    private int c = 0;
    public int maxc = 8;
    private vp_FPPlayerEventHandler m_player;
    private GameObject go_Player;

    void Start()
    {
        go_Player = GameObject.FindGameObjectWithTag("PlayerSpawner");
        m_player = go_Player.GetComponent<vp_FPPlayerEventHandler>();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // next
        {
            if (c < maxc)
            {
                c++;
            }
            if (c == maxc)
            {
                c = maxc;
            }
            // Weapon Change
            m_player.SetNextWeapon.Try();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // previous
        {
            if (c <= maxc)
            {
                c--;
            }
            if (c <= 0)
            {
                c = 0;
            }
            // Weapon Change
            m_player.SetPrevWeapon.Try();
        }
    }
}
