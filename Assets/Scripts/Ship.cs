using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    public Transform ShipBody;
    public GameObject Ammo;
    public float Speed;
    public PauseMenu PauseScreen;
    public DeathMenu DeathScreen;
    public bool InMenu;
    
    private int _health;

    public int Health
    {
        get { return _health; }
        private set
        {
            _health = value;
            if (_health<=0)
            {
                hud.SetActive(false);
                DeathScreen.Show();
                //Destroy(gameObject);
                Globals.UnlockCursor();
            }
        }
    }

    Transform trans;
    Rigidbody rigid;

    GameObject hud;

    Vector3 lastMousePos;
    float lastFireValue;
    float hitDelay;

    void Start()
    {
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();

        hud = GameObject.Find("HUD");

        _health = 3;

        if (!InMenu)
        {
            Globals.LockCursor();
        }

        lastMousePos = Input.mousePosition;
    }
    void Update()
    {
        if (!Globals.Paused)
        {
            #region NonVRCamControl

            trans.Rotate(Vector3.up, Input.GetAxis("CamX"));
            trans.Rotate(Vector3.left, Input.GetAxis("CamY"));
            trans.localEulerAngles = new Vector3(trans.localEulerAngles.x, trans.localEulerAngles.y, 0);
            #endregion

            float fireValue = Input.GetAxis("Fire1");
            if (fireValue > 0.9 && lastFireValue <= 0.9)
            {
                GameObject go = Globals.Instantiate<GameObject>(Ammo);
                Transform ammoTrans = go.GetComponent<Transform>();
                Rigidbody ammoRig = go.GetComponent<Rigidbody>();
                ammoTrans.position = ShipBody.position + ShipBody.forward * 2;
                ammoTrans.rotation = ShipBody.rotation;
                ammoTrans.Rotate(new Vector3(-90, 0, 0), Space.Self);
                ammoRig.velocity = ShipBody.forward * Speed + rigid.velocity;
            }
            lastFireValue = fireValue;

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Globals.Pause();
                PauseScreen.Show();
            }

            hitDelay -= Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        Vector3 force = 6 * (ShipBody.forward * Input.GetAxis("Vertical") + ShipBody.right * Input.GetAxis("Horizontal") + ShipBody.up * Input.GetAxis("Depth"));
        if (rigid.velocity.magnitude > 9)
        {
            if (force.x * rigid.velocity.x > 0)
            {
                force.x = 0;
            }
            if (force.y * rigid.velocity.y > 0)
            {
                force.y = 0;
            }
            if (force.z * rigid.velocity.z > 0)
            {
                force.z = 0;
            }
        }
        rigid.AddForce(force);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (hitDelay<0)
        {
            Health--;
            hitDelay = 1.5f;
        }
    }
}
