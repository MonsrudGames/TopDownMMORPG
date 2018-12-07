using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskFollowPlayer : MonoBehaviour
{

    public GameObject Player;

    void Update()
    {
        this.transform.position = Player.transform.position;
    }
}
