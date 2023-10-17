using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrlMixamo: MonoBehaviour {
    private Transform tr;
    private Animation anim;
    public float moveSpeed = 10.0f;
    public float turnSpeed = 80.0f;
    public float currHp;

    private readonly float initHp = 100;

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    public float animSpeed;
    // Start is called before the first frame update
    IEnumerator Start() {
        currHp = initHp;
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    // Update is called once per frame
    void Update() {
        float h = Input.GetAxis("Horizontal"); // -1.0f ~ 0.0f ~ +1.0f
        float v = Input.GetAxis("Vertical"); // -1.0f ~ 0.0f ~ +1.0f
        float r = Input.GetAxis("Mouse X");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);

        tr.Rotate(Vector3.up * Time.deltaTime * r * turnSpeed);

        PlayerAnim(h, v);

        //tr.Translate(Vector3.forward * Time.deltaTime * v * moveSpeed);
        //tr.Translate(Vector3.right * Time.deltaTime * h * moveSpeed);

        //Debug.Log("���� �Է� �� = " + h + "���� �Է� �� = " + v);
        //transform.position += new Vector3(0, 0, 0.1f);
        //transform.position += Vector3.forward * 0.1f;
        //tr.position += Vector3.forward * 0.1f;
    }

    void PlayerAnim(float h, float v)
    {
        if( v >= 0.1f){
            anim.CrossFade("RunF", 0.25f);
        //    anim["RunF"].speed = 0.25f;
        //    anim["RunF"].speed = animSpeed;
        }
        else if ( v <= -0.1f )
        {
            anim.CrossFade("RunB", 0.25f);
        }
        else if ( h >= 0.1f )
        {
            anim.CrossFade("RunR", 0.25f);
        }
        else if ( h <= -0.1f )
        {
            anim.CrossFade("RunL", 0.25f);
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);
        }
    }

    void OnTriggerEnter(Collider coll) {
        if (currHp >= 0.0f && coll.CompareTag("PUNCH")) {
            currHp -= 30.0f;
            Debug.Log($"Player hp = {currHp/initHp}");

            if (currHp <= 0.0f) {
                PlayerDie();
            }
        }
    }

    void PlayerDie(){
        Debug.Log("Player Die !");
        OnPlayerDie();
    }
}
