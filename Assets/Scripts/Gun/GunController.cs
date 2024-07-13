using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GunController : MonoBehaviour
{
    [Header("Gun Rotate")]
    public float angleLimit = 55f; //활동 반경 부채꼴 각
    private Vector3 directionLimitRight;
    private Vector3 directionLimitLeft;
    public Vector3 mousePosition;
    public Vector3 direction;
    public float calAngle;

    void Start()
    {
        directionLimitRight = new Vector3(Mathf.Sin(angleLimit * Mathf.Deg2Rad), Mathf.Cos(angleLimit * Mathf.Deg2Rad), 0f);
        directionLimitLeft = new Vector3(Mathf.Sin(-angleLimit * Mathf.Deg2Rad), Mathf.Cos(angleLimit * Mathf.Deg2Rad), 0f);
    }

    void Update()
    {
        Rotate();
        Fire();
    }

    private void Rotate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        direction = mousePosition - transform.position;
        calAngle = -Mathf.Atan(direction.x / direction.y) * Mathf.Rad2Deg;
        if (CheckLimit()) return;
        transform.eulerAngles = new Vector3(0f, 0f, calAngle);
    }

    private void Fire()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (CheckLimit()) direction = (calAngle > 0) ? directionLimitLeft : directionLimitRight;
        Debug.DrawRay(transform.position, direction * 1000f, Color.red, 4f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, float.MaxValue);
        hit.collider?.gameObject.GetComponent<Enemy>().Hit();
    }

    private bool CheckLimit()
    {
        return calAngle >= angleLimit || calAngle <= -angleLimit;
    }


}