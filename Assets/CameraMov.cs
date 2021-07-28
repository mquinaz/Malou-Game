using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMov : MonoBehaviour
{
    float speed = 1.0f;
    float zoomSpeed = 75.0f;
    float rotateSpeed = 0.1f;

    float maxHeight = 5f;
    float minHeight = 1f;
    float minLeft = -2.5f;
    float minRight = 2.5f;
    float minUp = -2.5f;
    float minDown = 4f;
    Vector2 p1;
    Vector2 p2;

    void Update()
    {
        float hsp = transform.position.y * speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float vsp = transform.position.y * speed * Input.GetAxis("Vertical") * Time.deltaTime;
        float scrollSp = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;

        if(  (transform.position.y >= maxHeight) && (scrollSp > 0 ) )
        {
            scrollSp = 0;
        }
        else if( (transform.position.y <= minHeight) && (scrollSp < 0))
        {
            scrollSp = 0;   
        }

        if( (transform.position.y + scrollSp) > maxHeight)
        {
            scrollSp = maxHeight - transform.position.y;
        }
        else if( (transform.position.y + scrollSp) < minHeight)
        {
            scrollSp = minHeight - transform.position.y;
        }
        if ((transform.position.z >= minRight) && (hsp > 0))
        {
            hsp = 0;
        }
        if ((transform.position.z <= minLeft) && (hsp < 0))
        {
            hsp = 0;
        }
        if ((transform.position.x >= minDown) && (vsp < 0))
        {
            vsp = 0;
        }
        if ((transform.position.x <= minUp) && (vsp > 0))
        {
            vsp = 0;
        }
        Vector3 verticalMove = new Vector3(0,scrollSp,0);
        Vector3 lateralMove = hsp * transform.right;
        Vector3 forwardMove = transform.forward;
        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vsp; 

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;

        getCameraRotation();
    }


    void getCameraRotation()
    {
        if(Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));

            p1 = p2;
        }

    }
}