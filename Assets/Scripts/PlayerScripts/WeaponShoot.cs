using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{


    #region -gunStats-
    public int maxInReserve = 30, inReserve = 30, maxInMag = 10, inMag = 10;
    // ints handling ammo/mag capacity and current
    //bools for handling reload states and if you have ammo

    public float Dam = 1.0f, RangeOptimal = 1.0f, RangeMax = 50f, Rof = 0.25f, LoadTime = 1.0f, ArmorPen = 1.0f, disRunMulti = 2f, HitForce = 100f, BurstMin = 1f, BurstMax = 1f, ShotsPerShot = 1f;
    public float minAmmo = 1.0f;

    //values for calculating weapon dispersion

    public float DisBase = 15f; //degress of weapon spread at maximum, the following floats modify this value 
    public float DisStand = 1f, DisCrouch = 1f, DisProne = 1f, DisWalk = 2f, DisCrouchWalk = 1f, DisRun = 5f, DisSlide = 1f, DisJump = 1f;

    public int ADSSpeed = 10;

    public bool isAuto;
    #endregion

    public bool canAttack = true;
    public bool hasFired = false;

    public bool isReloading = false, hasAmmo = true;

    public float nextFireAt = 0;
    private Vector3 bulletTrajectory;

    public LineRenderer laserLine;                                        // Reference to the LineRenderer component which will display our laserline
    public WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    public AudioSource gunSound;
    public AudioClip bang, reload, click;

        private Transform gunEnd;
    public GameObject gun, hipPosition, ADSPosition;

    private GameObject hitDecal;

    private bool Fire1_In;    /*----FIX THIS THIS IS----------*/


    
    #region -Init Connections-
    private bool gunisInit;

    private PlayerManager playerManager;
    private PlayerInputHandler playerInputHandler;
    private GameObject playerCap;
    private GameObject leanPoint;
    private GameObject playerHead;
    private Camera playerCam;
    private Camera gunCam;    

    private PlayerController playerController; 

    private GameObject WeaponUI;
    private GameObject activeWeapon;

    private WeaponUIController WUIC;

    public InventoryController inventoryController;

    private TMPro.TextMeshProUGUI ScoreCard;
    private TMPro.TextMeshProUGUI AmmoCounter;
    private TMPro.TextMeshProUGUI CST; //centerscreentext

    private CharacterController characterController;
   
    #endregion


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region  -Funcs-

    public void init(PlayerManager initializer)
    {

        playerManager = initializer;
        playerCap = playerManager.playerCap;
        playerController = playerManager.playerController;
        // leanPoint = playerManager.leanPoint;
        playerHead = playerManager.playerHead;
        playerCam = playerManager.playerCam;
        // gunCam = playerManager.gunCam;   
        playerInputHandler = playerManager.playerInputHandler;

        // inventoryController = initializer.GetComponent<InventoryController>();
        characterController = initializer.characterController;

        // WeaponUI = playerManager.WeaponUI;
        // WUIC = playerManager.WUIC;
        // ScoreCard = playerManager.ScoreCard;
        // AmmoCounter = playerManager.AmmoCounter;
        // CST = playerManager.CST;

    }

    public void weaponFire1(PlayerManager attacker)
    {

          
        if(canAttack && (Fire1_In == true && (hasFired == false || isAuto == true))){         //check if something is blocking attack action
            if (Time.time > nextFireAt && inReserve > minAmmo)   {   //// gun loaded behavior check ////


                // Update the time when our player can fire next
                nextFireAt = Time.time + (60/Rof);       

                // Start our ShotEffect coroutine to turn our laser line on and off
                StartCoroutine(ShotEffect());

                 // Create a vector at the center of our camera's viewport
                Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                RaycastHit hit;
                //Declare a raycast hit to store information about what our ractast has hit


                // Set the start position for our visual effect for our laser to the position of gunEnd
                laserLine.SetPosition(0, gunEnd.position);

                int curShots = 0;
                // Check if our raycast has hit anything

                while (curShots < ShotsPerShot)
                {
                    if (Physics.Raycast(rayOrigin, bulletTrajectory, out hit, RangeMax, 1, QueryTriggerInteraction.Ignore))
                    {




                        // Set the end position for our laser line
                        laserLine.SetPosition(1, hit.point);

                        GameObject newHit = GameObject.Instantiate(hitDecal);

                        newHit.transform.position = hit.point + new Vector3(0, 0, .01f);


                        // // Get a refernce to a health script attached to the collider we hit
                        // if (hit.collider.GetComponent<ShootableBox>())
                        // {
                        //     ShootableBox health = (hit.collider.GetComponent<ShootableBox>());

                        //     // If there was a health script attached
                        //     if (health != null)
                        //     {
                        //         // Call the damage function of the script, passing in our gunDam variable
                        //         health.Damage(this);
                        //     }

                        //     // Check if the object we hit has a rigidbody attached
                        //     if (hit.rigidbody != null)
                        //     {
                        //         hit.rigidbody.AddForce(-hit.normal * gunHitForce);
                        //     }
                        // }
                        // else if (hit.collider.GetComponentInParent<ZombAI>())
                        if (hit.collider.GetComponentInParent<ZombAI>())
                        {
                            ZombAI health = (hit.collider.GetComponentInParent<ZombAI>());
                            health.Damage(this, hit.collider.gameObject);
                            playerManager.updateScore();
                        }



                    }
                    else
                    {
                        //If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                        laserLine.SetPosition(1, rayOrigin + (bulletTrajectory * RangeMax));
                    }

                    curShots++;

                }

                //Play gunshot audio and post to debug log
                gunSound.PlayOneShot(bang);
                Debug.Log("BANG!");
                //reduces current mag contents
                inMag--;

                // WUIC.updateAmmo();

            }
            else if (Time.time > nextFireAt && inMag <= 0 && isReloading != true && hasFired == false)
            {
                Debug.Log("click");
                gunSound.PlayOneShot(click);
                StartCoroutine(loadGun());


            }


            hasFired = true;
        }
    }


        private IEnumerator loadGun()
    {

        Debug.Log("reloading");

        if (inReserve == 0)
        {
            Debug.Log("No Ammo");
        }
        else if (inReserve + inMag > maxInMag)
        {
            gunSound.PlayOneShot(reload);
            isReloading = true; // important to prevent reloading bugs                   
            yield return new WaitForSeconds(LoadTime);
            inReserve -= maxInMag - inMag;
            inMag = maxInMag;
            // WUIC.updateAmmo();

            isReloading = false;
            gunSound.PlayOneShot(reload);
            Debug.Log(inReserve + " ammo remaining");
        }
        else
        {
            gunSound.PlayOneShot(reload);
            isReloading = true; // important to prevent reloading bugs  
            yield return new WaitForSeconds(LoadTime);
            inMag += inReserve;
            inReserve = 0;
            // WUIC.updateAmmo();

            isReloading = false;
            gunSound.PlayOneShot(reload);
            Debug.Log(inReserve + " ammo remaining");
        }
    }


        private IEnumerator ShotEffect()
    {
        //Turn on our line renderer
        laserLine.enabled = true;

        //wait for .07 seconds
        yield return shotDuration;

        //Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }


        public void hitScore(float input)
    {
        playerManager.scoreAdd(input);
    }

    #endregion
}
