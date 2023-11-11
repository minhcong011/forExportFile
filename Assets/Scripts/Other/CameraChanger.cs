using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{

    public Transform intro;
    private void Awake()
    {

        float resolutionValue = (float)Screen.width / Screen.height;
        if (resolutionValue < 1.8f)
        {
            this.transform.position = new Vector3(-1.31f, transform.position.y, transform.position.z);
        }
        else
        if (resolutionValue < 2.01f)
        {

            transform.position = new Vector3(-1.55f, transform.position.y, transform.position.z);
            Camera.main.orthographicSize = 5.5f;
        }
        else
        if (resolutionValue < 2.07f)
        {

            transform.position = new Vector3(-1.45f, transform.position.y, transform.position.z);
            Camera.main.orthographicSize = 5.4f;
        }
        else
        if (resolutionValue < 2.12f)
        {
     
            transform.position = new Vector3(-1.41f, transform.position.y, transform.position.z);
            Camera.main.orthographicSize = 5.3f;
        }
        else
        {
            transform.position = new Vector3(-1.41f, transform.position.y, transform.position.z);
            Camera.main.orthographicSize = 5.3f;
        }
        intro.position = new Vector3(transform.position.x , transform.position.y, intro.position.z);
    }
}
