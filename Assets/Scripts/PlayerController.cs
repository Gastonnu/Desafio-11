using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float cameraAxisX = 0f;
    [SerializeField] private int health = 5;
    [SerializeField] private float fireCooldown = 0f;
    [SerializeField] private int bullets = 7;

    [SerializeField] GameObject[] bulletsList;
    [SerializeField] GameObject reloadText;

    // Start is called before the first frame update

    private void Awake()
    {
        DeactivateReloadText();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotatePlayer();
        if(bullets == 0)
        {
            ActivateReloadText();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && fireCooldown <= 0 && bullets > 0){
            Shoot();
            DeactivateBullet();
        }
        fireCooldown -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.R) && bullets < 7)
        {
            Reload();
            ActivateBullets();
            DeactivateReloadText();
        }
    }

    private void Move()
    {
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(axisX, 0, axisY);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        cameraAxisX += Input.GetAxis("Mouse X");
        Quaternion angle = Quaternion.Euler(0, cameraAxisX, 0);
        transform.localRotation = angle;
    }

    private void Shoot() { 
        bullets -= 1;
        fireCooldown = .7f;
    }

    private void Reload()
    {
        bullets = 7;
        fireCooldown = 2f;
    }

    private void DeactivateBullet()
    {
        for (int i = 0; i < bulletsList.Length; i++)
        {
            if (bulletsList[i].activeSelf)
            {
                bulletsList[i].SetActive(false);
                return;
            }
        }
    }

    private void ActivateBullets()
    {
        for(int i = 0; i < bulletsList.Length; i++)
        {
            bulletsList[i].SetActive(true);
        }
    }

    private void ActivateReloadText()
    {
        reloadText.SetActive(true);
    }

    private void DeactivateReloadText()
    {
        reloadText.SetActive(false);
    }
}