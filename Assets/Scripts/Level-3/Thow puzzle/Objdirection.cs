using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDirection : MonoBehaviour
{
    private LevelController levelController;
    private GameObject puzzleButton;
    public PrefabSettings prefabSettings;

    private Trigger trigger;
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
        trigger = sqTrigger.GetComponent<Trigger>();

        if(trigger.counter > 3)
        {
            levelController.LaunchMainScreen(puzzleButton);
            prefab.SetActive(false);
            finished = true;
        }
    }
}
