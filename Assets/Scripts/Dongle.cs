using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dongle : MonoBehaviour
{

    public int level;
    public bool isDrag;
    public bool isMerge;
    Rigidbody2D rigid;
    Animator anim;
    CircleCollider2D circle;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();
    }

    void OnEnable()
    {
        anim.SetInteger("Level", level);
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
        rigid.simulated = false;

    }

    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dongle")
        {
            Dongle other = collision.gameObject.GetComponent<Dongle>();

            if (level == other.level && !isMerge && !other.isMerge && level < 7)
            {
                // Merge
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                if (meY < otherY || (meY == otherY && meX > otherX))
                {
                    other.Hide(transform.position);
                }


            }
        }
    }

    public void Hide(Vector3 targetPos)
    {
        isMerge = true;

        // 물리효과 제거
        rigid.simulated = false;
        circle.enabled = false;

        StartCoroutine(HideRoutine(targetPos));
    }

    IEnumerator HideRoutine(Vector3 targetPos)
    {

        int framecount = 0;

        while (framecount < 20)
        {
            framecount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
            yield return null;
        }

        yield return null;

        isMerge = false;
        gameObject.SetActive(false);
    }
}
