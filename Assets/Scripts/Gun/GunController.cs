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

    [Header("Choose Input")] 
    public bool useMouse = false; //true- mouse, false- keyboard
    
    [Header("UseMouse")]
    public Vector3 mousePosition;
    public Vector3 direction;
    public float calAngle;

    [Header("UseKeyboard")] 
    public float rotateSpeed = 2.0f;
    public float currentAngle;

    void Start()
    {
        directionLimitRight = new Vector3(Mathf.Sin(angleLimit * Mathf.Deg2Rad), Mathf.Cos(angleLimit * Mathf.Deg2Rad), 0f);
        directionLimitLeft = new Vector3(Mathf.Sin(-angleLimit * Mathf.Deg2Rad), Mathf.Cos(angleLimit * Mathf.Deg2Rad), 0f);
    }

    void Update()
    {
        if (useMouse)
        {
            MouseRotate();
            MouseFire();
        }
        else
        {
            KeyRotate();
            KeyFire();
        }
    }

    private void MouseRotate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        direction = mousePosition - transform.position;
        calAngle = -Mathf.Atan(direction.x / direction.y) * Mathf.Rad2Deg;
        if (CheckLimit()) return;
        transform.eulerAngles = new Vector3(0f, 0f, calAngle);
    }

    private void KeyRotate()
    {
        // 현재 Z 회전값을 가져오기 (유니티는 오일러 각도를 사용)
        float currentZRotation = transform.eulerAngles.z;

        // Z 회전값이 180도를 넘을 경우 음수로 변환 (유니티의 오일러 각도 특성)
        if (currentZRotation > 180f)
        {
            currentZRotation -= 360f;
        }

        // 왼쪽 화살표 키 입력 처리
        if (Input.GetKey(KeyCode.LeftArrow) && currentZRotation <= angleLimit)
        {
            transform.Rotate(0f, 0f, rotateSpeed);
        }

        // 오른쪽 화살표 키 입력 처리
        if (Input.GetKey(KeyCode.RightArrow) && currentZRotation >= -angleLimit)
        {
            transform.Rotate(0f, 0f, -rotateSpeed);
        }
    }

    private void MouseFire()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (CheckLimit()) direction = (calAngle > 0) ? directionLimitLeft : directionLimitRight;
        Debug.DrawRay(transform.position, direction * 1000f, Color.red, 4f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, float.MaxValue);
        hit.collider?.gameObject.GetComponent<Enemy>().Hit();
    }
    
    private void KeyFire()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        Debug.DrawRay(transform.position, transform.up * 1000f, Color.red, 4f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, float.MaxValue);
        hit.collider?.gameObject.GetComponent<Enemy>().Hit();
    }

    private bool CheckLimit()
    {
        return calAngle >= angleLimit || calAngle <= -angleLimit;
    }
    
    // 각도로부터 방향 벡터를 계산하는 함수
    Vector3 GetDirectionFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees) * Mathf.Rad2Deg, 0f, 0f);
    }
}