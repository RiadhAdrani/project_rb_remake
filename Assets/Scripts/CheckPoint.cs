using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Level level;

    private void Start()
    {
        level = GetComponentInParent<Level>();
    }
}
