using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSet : MonoBehaviour
{
    public static EffectSet Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EffectSet>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("EffectSet");
                    instance = instanceContainer.AddComponent<EffectSet>();
                }
            }
            return instance;
        }
    }
    private static EffectSet instance;
    // Start is called before the first frame update
    [Header("Monster")]
    public GameObject DuckAtkEffect;
    public GameObject DuckDmgEffect;
    [Header("Player")]
    public GameObject PlayerAtkEffect;
    public GameObject PlayerDmgEffect;
}
