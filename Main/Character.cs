using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Added for the Coins Scoring System
using UnityStandardAssets.CrossPlatformInput; //Added for PC2Android convert movement.

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float moveSpeed;
    private float dirX;
    private bool facingRight = true;
    public Vector3 localScale;
    public Vector3 dustLoc;

    // particles
    public bool shieldActivated;
    public GameObject coinParticle;
    public GameObject bombParticle;
    public ParticleSystem dust;
    public ParticleSystem shield;
    public ParticleSystem shieldDestroyed;

    public Controller control; // replaced:
    //Controller control = new Controller();

    public PrefabsControl prefab; // replaced:
    //PrefabsControl prefab = new PrefabsControl(); // --> Gets a log error saying he cant create a new monoBehavior, but works.

    public Achievements tracker; // replaced:
    //Achievements tracker = new Achievements();



    public static bool IsPlayerAlive = true;
    //Coins Scoring System
    [SerializeField]
    public Text PointsUI; // UI Text object from Hiearchy
    public int collidedCoinValue = 10; // how much a coin is worth
    public int coinsCapturedInThisRun; // how many coins were "captured" in this current run. usable in the points system.
    public int buffsCapturedInThisRun;


    // Start is called before the first frame update - Use this for initialization
    private void Start()
    {
        DataManagement.dataManagement.LoadData();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //coinParticle.GetComponent<ParticleSystem>().enableEmission = false;
        localScale = transform.localScale;
        moveSpeed = 8f;
        coinsCapturedInThisRun = 0;
    }

        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hit by Poop - END GAME
        if (collision.gameObject.CompareTag("Poop"))
        {
            // Shield activated = SAVE PLAYER
            if ( shieldActivated == true)
            {
                Destroy(collision.gameObject);
                shield.Stop(shield, ParticleSystemStopBehavior.StopEmittingAndClear);
                shieldDestroyed.Play();
                Handheld.Vibrate(); // indication shield has lost.
                shieldActivated = false;
            }

            // Shield De-activated = KILL PLAYER
            else
            {
            if (System.Math.Round(Time.timeSinceLevelLoad, 1) > DataManagement.dataManagement.highScore)
                DataManagement.dataManagement.highScore = Time.timeSinceLevelLoad;
            Time.timeScale = 0f;
            tracker.addCount("Poop");
            DataManagement.dataManagement.SaveData();
            SoundManager.sndMan.PlayFartSound();
            IsPlayerAlive = false;
            }
        }
        // Hit by Coin
        if (collision.gameObject.CompareTag("Coin"))
        {
            tracker.addCount("Coin");
            Vector3 particlePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z); // Receive's player position and add abit more in the Y so the particle will be above him.
            GameObject particle2Destroy = Instantiate(coinParticle, particlePos, transform.rotation);
            Destroy(particle2Destroy, 3f);
            SoundManager.sndMan.PlayCoinSound();
            coinsCapturedInThisRun += prefab.addCoin("smallCoin");
            DataManagement.dataManagement.coinsCollected += prefab.addCoin("smallCoin"); // Increase the coin in the DataManagement class.
            prefab.addCoin("smallCoin");
            Destroy(collision.gameObject);
        }
        // Hit by Big Coin
        if (collision.gameObject.CompareTag("BigCoin"))
        {
            tracker.addCount("Buff");
            buffsCapturedInThisRun += 1;
            Vector3 particlePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z); // Receive's player position and add abit more in the Y so the particle will be above him.
            Instantiate(coinParticle, particlePos, transform.rotation);
            SoundManager.sndMan.PlayCoinSound();
            coinsCapturedInThisRun += prefab.addCoin("bigCoin");
            DataManagement.dataManagement.coinsCollected += prefab.addCoin("bigCoin"); // Increase the coin in the DataManagement class.
            Destroy(collision.gameObject);
        }
        // Hit By poopStick
        if (collision.gameObject.CompareTag("poopStick"))
        {
            tracker.addCount("Buff");
            buffsCapturedInThisRun += 1;
            Vector3 particlePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            GameObject buff2Destroy = Instantiate(bombParticle, particlePos, transform.rotation);
            Destroy(buff2Destroy, 3f);
            control.DestroyAllGameObjects();
            Destroy(collision.gameObject);
            ShakeBehaviour.TriggerShake();
            Debug.Log("particle debug is: " + control.poopStickParticlePrefab); // Check if particle is null
        }
        // Hit By tissuePaper
        if (collision.gameObject.CompareTag("tissuePaper"))
        {
            tracker.addCount("Buff");
            buffsCapturedInThisRun += 1;
            shield.Play();
            shieldActivated = true;
            Vector3 particlePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z); // Receive's player position and add abit more in the Y so the particle will be above him.
            Destroy(collision.gameObject);
        }

        // Hit by Poop but have Umbrella - TBD.
    }

    
    // Update is called once per frame
    private void Update()
    {
        
        // if you want keyboard movement for Editor do "Input" instead of "CrossPlatformInputManager"
        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;

        if (CrossPlatformInputManager.GetButtonDown("Jump") && rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * 700f);
            dustLoc.Set(localScale.x, localScale.y, 1);
            dust.transform.position.Set(dustLoc.x,dustLoc.y,1f);
            dust.Play();
        }

        //  Controls Animator states
        //Running animation
        if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
        //On Ground animation
        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
        // Jumping animation
        if (rb.velocity.y > 0)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isFalling", false);
        }
        //Falling animation
        if (rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }


        //Coins Update
        PointsUI.text = "Score: " + (coinsCapturedInThisRun + System.Math.Round(Time.timeSinceLevelLoad,1)) + " , Coins: " + DataManagement.dataManagement.coinsCollected.ToString();


    } // end of Update





    // We pass the velocity to the rigidBody depends on the direction of the X value.
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    } // end of FixedUpdate






    // We flip a character depending on where it goes atm and when he is currently faced to.
    private void LateUpdate()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && localScale.x < 0) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    } // end of LateUpdate
}
