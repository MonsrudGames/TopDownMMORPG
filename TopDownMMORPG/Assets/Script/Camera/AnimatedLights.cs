using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedLights : MonoBehaviour
{

    public Sprite[] Frames;

    public SpriteMask[] Masks;

    private void Start()
    {
        Masks = GetComponentsInChildren<SpriteMask>();
    }

    void FixedUpdate()
    {
        int f = Random.Range(1, 4);

        if(f == 1)
        {
            for (int i = 0; i < Masks.Length; i++)
            {
                int e = Random.Range(0, Frames.Length);

                Masks[i].sprite = Frames[e];
            }
        }
    }
}
