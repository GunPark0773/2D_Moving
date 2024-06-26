using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float degreePerSecond;
    private float checkF = 0f;
    private bool check = true;

    private Vector3 SetPosition = Vector3.zero;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = SetPosition;
        transform.Rotate(Vector3.forward * Time.deltaTime * degreePerSecond);
        checkF += Time.deltaTime;
        if(checkF > 1)
        {
            checkF = -1f;
            check = !check;
        }
        if(check)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * degreePerSecond);
            SetPosition.x += 0.02f;
        }
        else
        {
            transform.Rotate(Vector3.down * Time.deltaTime * degreePerSecond);
            SetPosition.x -= 0.02f;
        }
    }
}
