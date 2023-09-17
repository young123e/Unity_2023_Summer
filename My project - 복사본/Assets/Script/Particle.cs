using UnityEngine;

public class Particle : MonoBehaviour
{
    // 파티클 시스템의 Collider 컴포넌트
    private Collider particleCollider;

    // 파티클 시스템 참조
    public ParticleSystem myParticleSystem;
    public GameObject LetterW;
    public GameObject LetterB;
    public GameObject AppearZoneW;
    public GameObject AppearZoneB;
    public Player player;
    GameObject nearObject;

    private int letterType = 0;
    //Animator animator;
    

    void Start()
    {
        // 파티클 시스템의 Collider 컴포넌트 가져오기
        particleCollider = GetComponent<Collider>();

        // Collider가 없는 경우 오류를 방지하기 위해 추가로 체크합니다.
        if (particleCollider == null)
        {
            Debug.LogError("Particle script requires a Collider component on the same GameObject!");
        }
        myParticleSystem.gameObject.SetActive(false);
    }

    // 다른 Collider와 충돌할 때 호출되는 콜백 함수
 void OnParticleCollision(GameObject other)
{
    if (letterType == 1)
    {
        LetterW.SetActive(false);
        Debug.Log("collision");
        AppearZoneW.SetActive(true);
    }

    if (letterType == 2)
    {
        LetterB.SetActive(false);
        Debug.Log("collision");
        AppearZoneB.SetActive(true);
    }
}

void OnTriggerStay(Collider other)
{
    if (other.tag == "LetterW")
    {
        letterType = 1;
        Debug.Log("1");
    }
    else if (other.tag == "LetterB")
    {
        letterType = 2;
        Debug.Log("2");
    }
}

    void Update()
    {
        //animator = GetComponentInChildren<Animator>();
        // "q" 키를 눌렀을 때 파티클 시스템을 활성화/비활성화
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (myParticleSystem.isPlaying)
            {
                // 파티클이 이미 재생 중이라면 멈추고 비활성화
                myParticleSystem.Stop();
                myParticleSystem.gameObject.SetActive(false);
                
            }
            else
            {
                // 파티클이 재생 중이 아니라면 재생하고 활성화
                myParticleSystem.gameObject.SetActive(true);
                myParticleSystem.Play();

                player.animator.SetBool("doShoot",true );
                
        }
    }
    
}

}



