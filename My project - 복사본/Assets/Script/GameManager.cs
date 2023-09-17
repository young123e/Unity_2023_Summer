using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
    public Player player;
    public int stage;
    float playTime= 100.0f; // 전체 시간
    float totalPlaytime = 100.0f;
    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject gameoverPanel;
    public Text stageText;
    public Text playerTimeText;
    public Image WeaponRImg;
    public Image WeaponGImg;
    public Image WeaponBImg;
    public Image WeaponBTImg;
    public RectTransform playerHealthTimeBar; //시간줄어듦 표시


    public GameObject HandR;
    public GameObject HandG;
    public GameObject HandB;
    public GameObject Bottle1;
    public GameObject Bottle2;
    GameObject equipWeapon;
    
    
    int Statuenum =0;


    
    public void GameStart(){
        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
    }

    public void GameOver(){
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(true);
    }
    public void MainTitle(){
        SceneManager.LoadScene(0);
    }

    public void RedDestroy(){
        Statuenum =1;
        
        if (equipWeapon != null){
            equipWeapon.SetActive(false);
        }
        
        HandR.SetActive(true);
        equipWeapon = HandR;
    }
    public void GreenDestroy(){
        Statuenum =2;
        if (equipWeapon != null){
            equipWeapon.SetActive(false);
        }
        
        HandG.SetActive(true);
        equipWeapon = HandG;
    }
    public void BlueDestroy(){
        Statuenum =3;
        if (equipWeapon != null){
            equipWeapon.SetActive(false);
        }
        
        
        HandB.SetActive(true);
        equipWeapon = HandB;
    }

    public void Bottle1_Destroy(){
    Statuenum = 4;
    if (equipWeapon != null) {
        equipWeapon.SetActive(false);
    }
    
    if (Bottle1.activeSelf == false) {
        Bottle1.SetActive(true);
    }
    
    equipWeapon = Bottle1;
}

    public void Bottle2_Destroy(){
        Statuenum =5;
    if (equipWeapon != null) {
        equipWeapon.SetActive(false);
    }
    
    if (Bottle2.activeSelf == false) {
        Bottle2.SetActive(true);
    }
    
    equipWeapon = Bottle2;
    }

    
    void Update(){
        if (playTime>0){
            playTime -= Time.deltaTime;
        }
        else if (playTime<=0){
            Time.timeScale = 0.0f;
            //player.OnDie();
            GameOver();
            playTime= 10.0f;
        }
    }
    
    void LateUpdate(){
        //stage 텍스트
        stageText.text = "Stage "+ stage;
        //시간 표시 텍스트
        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour*3600) / 60);
        int second =(int)(playTime %60);
        playerTimeText.text = string.Format("{0:00}",hour)+":"+ string.Format("{0:00}",min)+":"+string.Format("{0:00}",second);
        //현재 들고 있는 조각상 이미지
        WeaponRImg.color = new Color(1,1,1, Statuenum==1  ? 1:0);   
        WeaponGImg.color = new Color(1,1,1, Statuenum==2  ? 1:0);
        WeaponBImg.color = new Color(1,1,1, Statuenum==3  ? 1:0);
        WeaponBTImg.color = new Color(1,1,1, Statuenum==4  ? 1:0);

        //time bar
        playerHealthTimeBar.localScale = new Vector3(playTime/totalPlaytime,1 ,1);

        
    }

        
}
