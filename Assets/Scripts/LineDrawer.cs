using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class LineDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private Vector2 mousePos;
    private Vector2 startMousePos;

    public Transform shootPoint;

    private bool canDrawLine = false;

    private int Id;

    public GameObject[] prefabsToShoot;

    public Color[] colors;

    private int index = 0;

    private new SpriteRenderer renderer;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        Id = GetInstanceID();

        renderer = GetComponent<SpriteRenderer>();

        renderer.color = colors[index];
    }

    private void OnMouseDown()
    {
        canDrawLine = true;
        startMousePos = transform.position;
    }

    private void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
        lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
    }

    private void OnMouseUp()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject != this.gameObject)
        {
            StartCoroutine(Shoot(hit.collider.gameObject.transform));
        }

    }
    private IEnumerator Shoot(Transform transform)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject ballToShoot = Instantiate(prefabsToShoot[index], shootPoint.position, Quaternion.identity);
            ballToShoot.GetComponent<Ball>().Setup(transform);

            yield return new WaitForSeconds(.1f);
        }
    }

    public void ChangeColor()
    {
        index += 1;

        if (index > colors.Length - 1) index = 0;

        renderer.color = colors[index];
    }

}