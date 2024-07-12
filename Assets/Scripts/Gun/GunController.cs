using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Rotate")]
    public float angle = 55f; //활동 반경 부채꼴 각
    public Vector3 mousePosition;
    public Vector3 direction;
    public float calAngle;

    void Update()
    {
        Rotate();
        Fire();
    }

    public void Rotate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        direction = mousePosition - transform.position;
        calAngle = -Mathf.Atan(direction.x / direction.y) * Mathf.Rad2Deg;
        if (calAngle > angle || calAngle < -angle) return;
        transform.eulerAngles = new Vector3(0f, 0f, calAngle);
    }

    public void Fire()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, float.MaxValue);
        hit.collider.gameObject.GetComponent<Enemy>().Hit();
    }
}