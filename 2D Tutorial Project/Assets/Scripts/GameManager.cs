using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;

    // Logic
    public int pesos;
    public int experience;

    // Save state
    public void SaveState()
    {
        Debug.Log("SaveState");
    }

    public void LoadState()
    {
        Debug.Log("LoadState");
    }
}
