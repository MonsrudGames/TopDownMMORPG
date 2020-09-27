using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    GameObject _CameraFollowPlayerTip;

    // Start is called before the first frame update
    void Start()
    {
        _CameraFollowPlayerTip = GameObject.Find("CameraFollowTip");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            _CameraFollowPlayerTip.SetActive(false);
        }
    }
}
