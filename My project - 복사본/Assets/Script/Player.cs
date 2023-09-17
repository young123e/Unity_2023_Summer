using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public GameObject[] grenades;
    public int hasBottle1;
    public int hasBottle2;
    public GameObject Bottle1Obj;
    public GameObject Bottle1;
    public GameObject Bottle2Obj;
    public GameObject Bottle2;
    public Camera followCamera;
    public GameManager manager;
    public int ammo;
    public int coin;
    public int health;
    
    public int maxAmmo;
    public int maxCoin;
    public int maxHealth;
    public int maxHasGranades;

    float hAxis;
    float vAxis;
    bool wDown;
    bool qDown;
    bool gDown;
    bool jDown;
    bool iDown;
    
    bool isJump;
    bool isDodge;
    bool isSwap;
    bool isBorder;
    bool isFireReady;
    bool isDead;

    Vector3 moveVec;
    Vector3 dodgeVec;

    Rigidbody rigid;
    public Animator animator;

    GameObject nearObject;
    GameObject equipWeapon;
    
    float firedelay;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        PlayerPrefs.SetInt("MaxScore",112500);
    }


    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        Interaction();
        Grenade1();
        Grenade2();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        gDown = Input.GetButton("Fire2");
        iDown = Input.GetButton("Interection");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
        {
            moveVec = dodgeVec;
        }
        if (isSwap||isDead){
            moveVec = Vector3.zero;
        }
        if(!isBorder)
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        animator.SetBool("isRun", moveVec != Vector3.zero);
        animator.SetBool("isWalk", wDown);

        
    }

    void Turn()
    {   //키보드에 의한 회전
        transform.LookAt(transform.position + moveVec);

        //마우스에 의한 회전
        if (Input.GetMouseButton(0) && !isDead){
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit,100)){
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y=0;
                transform.LookAt(transform.position + nextVec);
                
        }
        }
        
    }

    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge &&!isSwap&& !isDead)
        {
            rigid.AddForce(Vector3.up * 12, ForceMode.Impulse);
            animator.SetBool("isJump", true);
            animator.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Grenade1(){
       

        if( weapons[3].activeSelf == false)
            return;
        if(gDown && !isSwap&& !isDead) {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            
            //gameObject.GetComponent<BoxCollider>().enabled = false;
            if (Physics.Raycast(ray, out rayHit,100)){
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y=5;

                GameObject instantGrenade = Instantiate(Bottle1Obj , transform.position, transform.rotation);
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
                rigidGrenade.AddForce(nextVec, ForceMode.Impulse);
                rigidGrenade.AddTorque(Vector3.back*10, ForceMode.Impulse);
                weapons[3].SetActive(false);
                Bottle1.SetActive(false);

        }       
        }
    }

    void Grenade2(){

        if( weapons[4].activeSelf == false)
            return;
        if(gDown && !isSwap&& !isDead) {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            
            //gameObject.GetComponent<BoxCollider>().enabled = false;
            if (Physics.Raycast(ray, out rayHit,100)){
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y=5;

                GameObject instantGrenade = Instantiate(Bottle2Obj , transform.position, transform.rotation);
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
                rigidGrenade.AddForce(nextVec, ForceMode.Impulse);
                rigidGrenade.AddTorque(Vector3.back*10, ForceMode.Impulse);
                weapons[4].SetActive(false);
                Bottle2.SetActive(false);

        }       
        }
    }

    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge &&!isSwap&& !isDead)
        {
            dodgeVec = moveVec;
            speed *= 2;
            animator.SetTrigger("doDodge");
            isDodge = true;
            Invoke("DodgeOut", 0.5f);
        }
    }

    void DodgeOut()
    {
        isDodge = false;
        speed *= 0.5f;
    }
  
    void Interaction()
    {
        
        if((iDown) && nearObject != null && !isJump && !isDodge&& !isDead)
        {
            if (nearObject.tag == null)
    {
        return;
    }
            else if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                //hasWeapons[weaponIndex] = true;

                if (equipWeapon != null)
                    equipWeapon.SetActive(false);
                equipWeapon = weapons[weaponIndex];
                
                equipWeapon.SetActive(true);

                animator.SetTrigger("doSwap");

                //Destroy(nearObject);
            }
            
        
        else if(nearObject.tag == "Table"){
                Table table = nearObject.GetComponent<Table>();
                table.Enter(this);   
            }
    }
    }
    void FreezeRotation(){
        rigid.angularVelocity = Vector3.zero;
    }
    void StopTowall(){
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward,5, LayerMask.GetMask("Wall"));
    }
    void FixedUpdate(){
        FreezeRotation();
        StopTowall();
    }
    void Attack(){
        if (equipWeapon == null)
            return;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            animator.SetBool("isJump", false);
            isJump = false;
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon" || other.tag =="Table" )
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
{
    if (other.tag == "Weapon")
        nearObject = null;
    else if (other.tag == "Table")
    {
        if (nearObject != null)
        {
            Table table = nearObject.GetComponent<Table>();
            if (table != null)
                table.Exit();
        }

        nearObject = null;
    }
    
}
    // public void OnDie(){
    //         //animator.SetTrigger("doDie");
    //         isDead =true;
    //         manager.GameOver();
    //     }
}