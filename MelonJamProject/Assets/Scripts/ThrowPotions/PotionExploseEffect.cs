using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionExploseEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.30f);
    }
}
