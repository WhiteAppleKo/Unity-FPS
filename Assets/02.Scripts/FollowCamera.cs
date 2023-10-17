using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetTr;
    private Transform camTr;

    [Range(2.0f, 20.0f)]
    public float distance = 10.0f;

    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        camTr = GetComponent<Transform>();
    }

    //반응 속도
    public float damping = 10.0f;
    // 카메라 LookAt의 오프셋 값
    public float targetOffset = 2.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        // 구면 선형보간 함수를 사용하여 부드럽게 위치 변경
        camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping);
        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));
    }
}
