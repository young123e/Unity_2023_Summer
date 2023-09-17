using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type {Melee, Range};
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffecet;
    public void Use(){
        if (type == Type.Melee){
            StopCoroutine("Shoot");
            StartCoroutine("Shoot");
        }
    }
    
    IEnumerable Shoot(){
        //1
        yield return new WaitForSeconds(0.1f);//0.1초 대기
        meleeArea.enabled  =true;
        trailEffecet.enabled = true;
        //2 
        yield break;

    }
}
