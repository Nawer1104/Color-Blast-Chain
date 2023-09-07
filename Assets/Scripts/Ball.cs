using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float moveSpeed;

    private Transform targetTrans;

    public GameObject explosionVfx;

    public void Setup(Transform target)
    {
        targetTrans = target;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTrans.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject vfx = Instantiate(explosionVfx, transform.position, Quaternion.identity);
        Destroy(vfx, 1f);
        Destroy(gameObject);

        if (collision.gameObject.tag == gameObject.tag)
        {
            collision.gameObject.GetComponent<Holder>().ActiveSlot();
        }
    }
}
