using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDirection : MonoBehaviour
{
    private LevelController levelController;
    private GameObject puzzleButton;
    public PrefabSettings prefabSettings;

    private Bucket bucketTrigger;
    public Vector3 direction;
    private Vector3 mousePos;

    public GameObject sqTrigger;
    public Rigidbody2D rb;

    public GameObject brick;
    public GameObject throwPuzzlePrefab;
    public float force;
    public int counter;
    public bool finished;

    private void Start()
    {
        puzzleButton = prefabSettings.GetComponent<PrefabSettings>().GetButton();
        levelController = prefabSettings.GetComponent<PrefabSettings>().GetLevelController();
        bucketTrigger = sqTrigger.GetComponent<Bucket>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         
        direction = mousePos - transform.position;

        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        CheckCoutner();

    }

    void Shoot()
    {
        GameObject brickInst = Instantiate(brick, transform.position, transform.rotation);
        brickInst.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
        Destroy(brickInst, 5.0f);
    }

    void CheckCoutner()
    {
        
        if(bucketTrigger.counter >= 3)
        {
            levelController.LaunchMainScreen(puzzleButton);
            throwPuzzlePrefab.SetActive(false);
            finished = true;
        }
    }
}
