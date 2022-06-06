using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objdirection : MonoBehaviour
{
    //public GameObject obj;
    //public float power;

    private LevelController levelController;
    private GameObject puzzleButton;
    public PrefabSettings prefabSettings;

    private trigger trigger;
    public Vector2 direction;


    public GameObject sqTrigger;
    public Rigidbody2D rb;

    public GameObject brick;
    public GameObject prefab;
    public float force;
    public int counter;
    public bool finished;

    private void Start()
    {

        
        puzzleButton = prefabSettings.GetComponent<PrefabSettings>().GetButton();
        levelController = prefabSettings.GetComponent<PrefabSettings>().GetLevelController();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Trakemove();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 objPos = transform.position;

        direction = mousePos - objPos;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        //Debug.Log("value - " + counter);

        checkcoutner();
        Throwobj();



    }

    void Throwobj()
    {
        transform.right = direction;
    }

    void Shoot()
    {
        GameObject brickInst = Instantiate(brick, transform.position, transform.rotation);
        brickInst.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
        Destroy(brickInst, 5.0f);
    }

    void Trakemove()
    {
        Vector2 thowdir = rb.velocity;
        float angle = Mathf.Atan2(thowdir.y, thowdir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void checkcoutner()
    {
        trigger = sqTrigger.GetComponent<trigger>();

        if(trigger.counter > 3)
        {
            levelController.LaunchMainScreen(puzzleButton);
            prefab.SetActive(false);
            finished = true;
        }
    }
}
