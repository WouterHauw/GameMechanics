using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float Walkspeed = 10.0f;
    public float RotationSpeed = 100.0f;

    //info for bullet
    public GameObject BulletGameObject;

    //public GameObject[] Bullets;
    public float Bulletspeed = 10.0f;

    [SerializeField]
    //private int _currentBullet;

    private Animator _animator;
    private bool _firstTimekeyisdown = true;


    void Start()
    {
        _animator = GetComponent<Animator>();
        Physics2D.gravity = Vector2.zero;
        //_currentBullet = 0;
        //foreach (var bullet in Bullets)
        //{
        //    bullet.SetActive(false);
        //}
    }
	
	// Update is called once per frame
	void Update () {
		//inputManager
        //Check Player Input
	    var speed = Input.GetAxis("Vertical") * Walkspeed;
	    var rotation = Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;

        //Let the player walk with animation
        transform.Translate(0,speed * Time.deltaTime, 0);
        transform.Rotate(0,0,-rotation);
	    if (speed > 5)
	    {
	        _animator.Play("Player Walks");
	    }
	    else
	    {
	        _animator.Play("Idle State");
	    }

        //Player Input Handeling Shoots
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        if (!_firstTimekeyisdown) return;
	        Attack();
	        _firstTimekeyisdown = false;
	    }
	    if (Input.GetKeyUp(KeyCode.Space))
	    {
	        if (!Input.GetKey(KeyCode.J)) _firstTimekeyisdown = true;
	    }

        //Input handeling for shield
	    if (Input.GetKeyDown(KeyCode.J))
	    {
	        TurnOnShield();
	        _firstTimekeyisdown = false;
        }
	    if (Input.GetKeyUp(KeyCode.J))
	    {
	        TurnOffShield();
	        _firstTimekeyisdown = true;
        }

    }

    void Attack()
    {
        //if (_currentBullet == Bullets.Length) _currentBullet = 0;
        var positon = gameObject.transform.Find("BulletSpawnPoint").position;

        //Bullets[_currentBullet].transform.position = positon;
        //Bullets[_currentBullet].transform.rotation = transform.rotation;
        //Bullets[_currentBullet].SetActive(true);
        //Bullets[_currentBullet].GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0,200));
        var bullet = Instantiate(BulletGameObject, positon, transform.rotation);
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 200));
        var test = bullet.GetComponent<Rigidbody2D>().velocity;



    }

    void TurnOnShield()
    {
        gameObject.transform.Find("Shield").gameObject.SetActive(true);
    }

    void TurnOffShield()
    {
        gameObject.transform.Find("Shield").gameObject.SetActive(false);
    }
}
