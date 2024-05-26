using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRotate : MonoBehaviour
{
    bool isrotatable;
    private float Rspeed = 40.0f;
    Rigidbody m_Rigidbody;

    void OnMouseDown()
    {
        isrotatable = true;
    }

    void OnMouseUp()
    {
        isrotatable = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        isrotatable = false;
        m_Rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isrotatable)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.forward * Rspeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.back * Rspeed * Time.deltaTime);
            }
        }
        else
        {
            m_Rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
