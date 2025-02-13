using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private GameObject choosePanel;
    [SerializeField] private GameObject upgradPanel;
    [SerializeField] private GameObject FireEleB;
    [SerializeField] private GameObject WaterEleB;
    [SerializeField] private GameObject EarthEleB;
    [SerializeField] private GameObject heall;
    [SerializeField] private GameObject upgradd;
    [SerializeField] private  TextMeshProUGUI counterLvFire;
    [SerializeField] private  TextMeshProUGUI counterLvWater;
    [SerializeField] private  TextMeshProUGUI counterLvEarth;
    public static int lvFire = 1;
    public static int lvWater = 1;
    public static int lvEarth = 1;
    private AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        counterLvFire.text = "Lv "+lvFire;
        counterLvWater.text = "Lv "+lvWater;
        counterLvEarth.text = "Lv "+lvEarth;
    }

    public void Fire()
    {
        if (MoveController.FireEleC >=2)
        {
            _audioManager.PlaySFX(_audioManager.button);
            MoveController.FireEleC -= 2;
            lvFire += 1;
            MoveController.damageBullet += 0.5f;
            WaterEleB.SetActive(false);
            EarthEleB.SetActive(false);
        }
    }
    public void Water()
    {
        if (MoveController.WaterEleC>=2)
        {
            _audioManager.PlaySFX(_audioManager.button);
            MoveController.WaterEleC -= 2;
            lvWater += 1;
            MoveController.moveSpeed += 0.4f;
            FireEleB.SetActive(false);
            EarthEleB.SetActive(false);
        }
    }
    public void Earth()
    {
        if (MoveController.EarthEleC >=2)
        {
            _audioManager.PlaySFX(_audioManager.button);
            MoveController.EarthEleC -= 2;
            lvEarth += 1;
            GunController.spawnBulletTime -= 0.2f;
            WaterEleB.SetActive(false);
            FireEleB.SetActive(false);
           
        }
    }

    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        SceneManager.LoadScene("First");
    }
    public void Upgrad()
    {
        upgradPanel.SetActive(true);
        choosePanel.SetActive(false);
    }

    public void Healing()
    {
       
       if (MoveController.EarthEleC >= 1 && MoveController.FireEleC >= 1 && MoveController.WaterEleC >= 1&&MoveController.curHealthPot<2)
       {
           heall.SetActive(false);
           upgradd.SetActive(false);
           _audioManager.PlaySFX(_audioManager.button);
           MoveController.EarthEleC -= 1;
           MoveController.FireEleC -= 1;
           MoveController.WaterEleC -= 1;
           MoveController.curHealthPot +=1;
          
       }
       else
       {
           
       }
       
    }
}
