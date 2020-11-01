using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    GameObject turrent;
    float speed = 5f;
    float rotationSpeed = 20f;
    float turrentRotationLimit = 45f;
    float turrentRotLimitQtn;


    private void Awake()
    {
        turrent = transform.GetChild(0).gameObject;
        turrentRotLimitQtn = Quaternion.Euler(0f, 0f, turrentRotationLimit + 0.1f).z;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            MoveTank(new Vector2(-1f, 0f));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveTank(new Vector2(1f, 0f));
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveTurrent(new Vector3(0f, 0f, 1f));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveTurrent(new Vector3(0f, 0f, -1f));
        }
    }

    void MoveTank(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void MoveTurrent(Vector3 direction)
    {
        float rotZ = turrent.transform.rotation.z;
        if (rotZ < turrentRotLimitQtn && rotZ > -turrentRotLimitQtn)
        {
            turrent.transform.Rotate(direction * rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (rotZ > 0)
                turrent.transform.eulerAngles = new Vector3(0f, 0f, turrentRotationLimit);
            else
                turrent.transform.eulerAngles = new Vector3(0f, 0f, -turrentRotationLimit);
        }
    }
}
