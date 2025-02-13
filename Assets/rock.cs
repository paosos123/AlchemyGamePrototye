using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    [SerializeField] private float speedChase;
    private float timerDestoryitself= 5f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down*speedChase*Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > timerDestoryitself)
        {
            Destroy(gameObject);
        }
    }
}
