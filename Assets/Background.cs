using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform mainCam;
    public Transform midBG;
    public Transform sideBG;
    public float length;

    // Update is called once per frame
    void Update()
    {
        if(mainCam.position.x > midBG.position.x)
        {
            UpdateBackgroundPosition(Vector3.right);
        }else if(mainCam.position.x < midBG.position.x)
        {
            UpdateBackgroundPosition(Vector3.left);
        }
    }

    void UpdateBackgroundPosition(Vector3 direction)
    {
        sideBG.position = midBG.position + direction * length;
        Transform temp = midBG;
        midBG = sideBG;
        midBG = temp;
    }
}
