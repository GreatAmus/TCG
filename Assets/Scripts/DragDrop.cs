using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private GameObject PlayArea;
    private bool isDragging = false;
    private bool isOverPlayArea = false;
    private Vector2 startArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {

            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        isOverPlayArea = true;
        PlayArea = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverPlayArea = false;
        PlayArea = null;
    }

    public void startDragging()
    {
        startArea = transform.position;
        isDragging = true;
    }
    public void stopDragging()
    {
        isDragging = false;
        if (isOverPlayArea)
        {
            transform.SetParent(PlayArea.transform, false);
        }
        else
        {
            transform.position = startArea;
        }
    }
}
