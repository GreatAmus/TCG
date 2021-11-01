using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour
{

    bool dragging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startDrag()
    {
        dragging = true;
    }

    public void endDrag()
    {
        dragging = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
}
