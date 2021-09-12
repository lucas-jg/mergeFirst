using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dongle : MonoBehaviour
{

    public bool isDrag;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDrag)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float leftBorder = -4.2f + transform.localScale.x / 2f;
            float rightBorder = 4.2f - transform.localScale.x / 2f;

            if (mousePosition.x < leftBorder)
            {
                mousePosition.x = leftBorder;
            }
            else if (mousePosition.x > rightBorder)
            {
                mousePosition.x = rightBorder;
            }

            mousePosition.z = 0;
            mousePosition.y = 8;

            // 천천히 따라가게 하는 기능
            transform.position = Vector3.Lerp(transform.position, mousePosition, 0.1f);
        }

    }

    public void Drage()
    {
        isDrag = true;
        rigidbody.simulated = false;

    }

    public void Drop()
    {
        isDrag = false;
        rigidbody.simulated = true;
    }
}
