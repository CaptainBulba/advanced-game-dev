using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDirection : MonoBehaviour
{
    private LevelController levelController;
    private GameObject puzzleButton;
    public PrefabSettings prefabSettings;
    public GameObject bucketTrigger;

    public Vector3 direction;
    private Vector3 mousePos;

    public GameObject sqTrigger;
    public Rigidbody2D rb;

    public GameObject brick;
    public GameObject throwPuzzlePrefab;
    public float force;
    public int counter;
    public bool finished;

    public GameObject inventoryItem;

    private void Start()
    {
        puzzleButton = prefabSettings.GetComponent<PrefabSettings>().GetButton();
        levelController = prefabSettings.GetComponent<PrefabSettings>().GetLevelController();
        Bucket.counter = 0;


        rb = GetComponent<Rigidbody2D>();
        counter = 0;
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
        counter = Bucket.counter;
        if (counter >= 5)
        {
            Inventory.Instance.AddItem(inventoryItem);
            levelController.LaunchMainScreen(puzzleButton);
            throwPuzzlePrefab.SetActive(false);
            finished = true;
        }
        
    }
}
