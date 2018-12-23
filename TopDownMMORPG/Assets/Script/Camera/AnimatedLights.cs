using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedLights : MonoBehaviour
{

    public Sprite[] Frames;

    public GameObject BlackTileMap;
    public GameObject DarkTileMap;
    public GameObject BlurredTileMap;

    public SpriteMask[] Masks;

    public GameObject InnerMask;
    public GameObject OuterMask;

    float ScaleNumber;

    public bool IsDay;

    private void Start()
    {
            IsDay = false;
           Masks = GetComponentsInChildren<SpriteMask>();
        foreach (SpriteMask mask in Masks)
        {
            if(mask.name == "InnerMask")
            {
                InnerMask = mask.gameObject;
            }else if(mask.name == "OuterMask")
            {
                OuterMask = mask.gameObject;
            }
        }
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

    private void Update()
    {
        ResizeCircle(InnerMask);

        if (Input.GetKeyDown(KeyCode.X) && ScaleNumber == 0)
        {
            IsDay = !IsDay;
            for (int i = 0; i < Masks.Length; i++)
            {
                if (IsDay)
                {
                    ScaleNumber = 1f;

                    if (Masks[i].name == "DayMask")
                    {
                        Masks[i].enabled = true;
                    }
                    else if (Masks[i].name == "InnerMask")
                    {
                        ResizeCircle(Masks[i].gameObject);
                    }
                    else
                    {
                        /*
                        Masks[i].enabled = false;
                        BlackTileMap.SetActive(false);
                        DarkTileMap.SetActive(false);
                        BlurredTileMap.SetActive(false);
                        */
                    }
                }
                else
                {
                    ScaleNumber = 2f;

                    if (Masks[i].name == "DayMask")
                    {
                        Masks[i].enabled = false;
                    }
                    else if (Masks[i].name == "InnerMask")
                    {
                        ResizeCircle(Masks[i].gameObject);
                    }
                    else
                    {
                        /*
                        Masks[i].enabled = true;
                        BlackTileMap.SetActive(true);
                        DarkTileMap.SetActive(true);
                        BlurredTileMap.SetActive(true);
                        */
                    }
                }
            }
        }
    }

    public float CircleCurrentSize;
    public float CircleBigSize;
    public float CircleSmallSize;

    void ResizeCircle(GameObject InnerCircle)
    {
        if (ScaleNumber == 1f)
        {
            InnerMask.transform.localScale = Vector3.Lerp(InnerMask.transform.localScale, new Vector3(CircleBigSize, CircleBigSize, CircleBigSize), 1f * Time.deltaTime);
            OuterMask.transform.localScale = Vector3.Lerp(OuterMask.transform.localScale, new Vector3(CircleBigSize, CircleBigSize, CircleBigSize) * 1.5f, 1f * Time.deltaTime);

            if (Vector3.Distance(InnerMask.transform.localScale, new Vector3(CircleBigSize, CircleBigSize, CircleBigSize)) < 2f)
            {
                ScaleNumber = 0;
            }
        }

        else if (ScaleNumber == 2f)
        {
            InnerMask.transform.localScale = Vector3.Lerp(InnerMask.transform.localScale, new Vector3(CircleSmallSize, CircleSmallSize, CircleSmallSize), 1f * Time.deltaTime);
            OuterMask.transform.localScale = Vector3.Lerp(OuterMask.transform.localScale, new Vector3(CircleSmallSize, CircleSmallSize, CircleSmallSize) * 1.5f, 1f * Time.deltaTime);

            if (Vector3.Distance(InnerMask.transform.localScale, new Vector3(CircleSmallSize, CircleSmallSize, CircleSmallSize)) < 0.02f)
            {
                InnerMask.transform.localScale = new Vector3(CircleSmallSize, CircleSmallSize, CircleSmallSize);
                OuterMask.transform.localScale = new Vector3(CircleSmallSize, CircleSmallSize, CircleSmallSize) * 1.5f;
                ScaleNumber = 0;
            }
        }
    }
}
