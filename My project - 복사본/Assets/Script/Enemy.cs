using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider boxCollider;
    void Awake(){
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    

    
    public void HitByGrenade(Vector3 explosionPos){
        
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage(reactVec));
    }

    IEnumerator OnDamage(Vector3 reactVec){
        yield return new WaitForSeconds(0.1f);
        reactVec = reactVec.normalized;
        reactVec += Vector3.up *3;
        //rigid.FreezeRotation = false;
        rigid.AddForce(reactVec*5, ForceMode.Impulse);
        rigid.AddTorque(reactVec*15, ForceMode.Impulse);
    }
}
