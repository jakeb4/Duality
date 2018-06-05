using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections;

public class SwitchLightMaps : MonoBehaviour
{


    private bool SwapBack;
    private bool Swap;

    void Start()
    {
        Swap = false;
        SwapBack = true;
        //SwapLightmaps();
    }

    public void SwapLightmaps()
    {
        Swap = true;
        SwapBack = false;
        LightmapData[] sceneLightmaps = LightmapSettings.lightmaps;
        foreach (LightmapData lmd in sceneLightmaps)
        {
            lmd.lightmapColor = Resources.Load("L2/" + lmd.lightmapColor.name) as Texture2D;
            lmd.lightmapColor = Resources.Load("L2/" + lmd.lightmapColor.name) as Texture2D;
        }
        LightmapSettings.lightmaps = sceneLightmaps;
    }

    public void SwapLightmapsBack()
    {
        SwapBack = true;
        Swap = false;
        LightmapData[] sceneLightmaps = LightmapSettings.lightmaps;
        foreach (LightmapData lmd in sceneLightmaps)
        {
            lmd.lightmapColor = Resources.Load("L1/" + lmd.lightmapColor.name) as Texture2D;
            lmd.lightmapColor = Resources.Load("L1/" + lmd.lightmapColor.name) as Texture2D;
        }
        LightmapSettings.lightmaps = sceneLightmaps;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            if (Swap != true)
            {
                SwapLightmaps();
            }
            else
                SwapLightmapsBack();
        }
    }
}