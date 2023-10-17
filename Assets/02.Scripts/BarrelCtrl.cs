using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 반드시 필요한 컴포넌트를 명시해 컴포넌트가 삭제되는 것을 방지하는 어트리뷰트
// 오디오소스 컴포넌트가 없으면 붙혀서 씀
[RequireComponent(typeof(AudioSource))]

public class BarrelCtrl: MonoBehaviour {
    public GameObject expEffect;

    public Texture[] textures;

    public float radius = 10.0f;

    private new MeshRenderer renderer;

    private Transform tr;
    private Rigidbody rb;

    public AudioClip fireSfx;
    public new AudioSource audio;

    private int hitCount = 0;
    // Start is called before the first frame update
    void Start() {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<MeshRenderer>();

        audio = GetComponent<AudioSource>();   

        int idx = Random.Range(0, textures.Length);

        renderer.material.mainTexture = textures[idx];
    }

    void OnCollisionEnter(Collision coll) {
        if (coll.collider.CompareTag("BULLET")) {
            if (++hitCount == 3) {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel() {
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);

        Destroy(exp, 2.0f);

        //rb.mass = 1.0f; rb.AddForce(Vector3.up * 1500.0f);

        IndirectDamage(tr.position);

        Destroy(gameObject, 3.0f);

        audio.PlayOneShot(fireSfx, 1.0f);
    }

    void IndirectDamage(Vector3 pos) {
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);

        foreach(var coll in colls) {
            if (coll) {
                rb = coll.GetComponent<Rigidbody>();
                rb.mass = 1.0f;
                rb.constraints = RigidbodyConstraints.None;

                rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f);
            }
        }
    }

    // Update is called once per frame
    void Update() {}
}
