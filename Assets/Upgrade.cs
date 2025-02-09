using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private GameObject choosePanel;
    [SerializeField] private GameObject upgradPanel;
    [SerializeField] private GameObject FireEleB;
    [SerializeField] private GameObject WaterEleB;
    [SerializeField] private GameObject EarthEleB;
    [SerializeField] private  TextMeshProUGUI counterLvFire;
    [SerializeField] private  TextMeshProUGUI counterLvWater;
    [SerializeField] private  TextMeshProUGUI counterLvEarth;
    public static int lvFire = 1;
    public static int lvWater = 1;
    public static int lvEarth = 1;
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (MoveController.FireEleC > 0)
        {
            MoveController.FireEleC -= 1;
            lvFire += 1;
            MoveController.damageBullet += 0.25f;
            WaterEleB.SetActive(false);
            EarthEleB.SetActive(false);
        }
    }
    public void Water()
    {
        if (MoveController.WaterEleC > 0)
        {
            MoveController.WaterEleC -= 1;
            lvWater += 1;
            MoveController.moveSpeed += 0.2f;
            FireEleB.SetActive(false);
            EarthEleB.SetActive(false);
        }
    }
    public void Earth()
    {
        if (MoveController.EarthEleC > 0)
        {
            MoveController.EarthEleC -= 1;
            lvEarth += 1;
            GunController.spawnBulletTime -= 0.1f;
            WaterEleB.SetActive(false);
            FireEleB.SetActive(false);
           
        }
    }

    public void Upgrad()
    {
        upgradPanel.SetActive(true);
        choosePanel.SetActive(false);
    }

    public void Healing()
    {
       choosePanel.SetActive(false);
       MoveController.curHealthPot = 2;
    }
}
