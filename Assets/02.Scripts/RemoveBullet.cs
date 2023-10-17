using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet: MonoBehaviour {
    public GameObject sparkEffect;
    void OnCollisionEnter(Collision coll) {
        if (coll.collider.tag == "BULLET") {
            // 첫 번째 충돌 지점의 정보 추출
            ContactPoint cp = coll.contacts[0];
            // 충돌한 총알의 법선 벡터를 쿼터니언 타입으로 변환
            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            GameObject spark = Instantiate(sparkEffect, cp.point, rot);

            Destroy(spark, 0.5f);

            Destroy(coll.gameObject);
            
        }
    }
}
