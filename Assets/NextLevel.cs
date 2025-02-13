using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private GameObject fButton;
    [SerializeField] private GameObject choosePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            fButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                choosePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            fButton.SetActive(false);
          
        }
    }
}
