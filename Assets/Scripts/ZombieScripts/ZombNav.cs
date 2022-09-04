using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombNav : MonoBehaviour
{

    // private NavMeshAgent nm;
    // public GameObject navTarget;
    // private ZombAI zombAI;
    // private GameManager gameManager;
    // private EntryManager entryManager;
    // private WaveManager waveManager;

    // private GameObject targetEntry;
    // private GameObject boundingBox;

    // #region bools
    // private bool isInside;
    // private bool interruptMove;
    // private bool canMove;
    // private bool atWindow;
    // private bool isBusy;
    // #endregion 

    // Start is called before the first frame update
    // void Start()
    // {

    //     init();
    //     StartCoroutine(think());
    // }

    // Update is called once per frame
    void Update()
    {

    }


    // public void init()
    // {
    //     nm = gameObject.GetComponent<NavMeshAgent>();
    //     zombAI = this.GetComponent<ZombAI>();
    //     zombAI.getManagers(out gameManager, out waveManager, out entryManager, out boundingBox);
        
    //     isInside = false;
    //     canMove = true;
    //     isBusy = false;
    //     interruptMove = false;
    //     atWindow = false;
    // }


    


    // IEnumerator think()
    // {
    //     while (true)
    //     {
    //         if (atWindow == true && isBusy == false)
    //         {
    //             isBusy = true;
    //             yield return attemptEntry();
    //         }

    //         if (canMove == true && isBusy == false)
    //         {
    //             float targetRange;
                
                
    //             calculateNavTarget(out navTarget, out targetRange);

    //             StartCoroutine(zombAI.meleePlayer(navTarget, targetRange));   

                           

    //             if(targetRange < zombAI.meleeRange/1.5f)
    //             {
    //                 nm.speed = 0f;
    //             } else if(targetRange < zombAI.meleeRange)
    //             {
    //                  nm.speed = 1f;
    //             } else 
    //             {
    //                 nm.speed = 3.5f;
    //             }
                
                
                
    //                 nm.SetDestination(navTarget.transform.position);
    //                 yield return new WaitForSeconds(0.2f);
                
                
                
    //         }

    //         yield return new WaitForSeconds(0.1f);
    //     }

    // }

     

    

    //functions, not coroutines
    // #region functions  
    // private void calculateNavTarget(out GameObject navTarget, out float targetRange)
    // {


    //     if (isInside == true)
    //     {
    //      calculateNearestPlayerNav(out navTarget, out targetRange);
    //     }
    //     else
    //     {
    //      calculateNearestEntry(out navTarget, out targetRange);
    //     }

    // }

    // private void calculateNearestEntry(out GameObject navTarget, out float targetRange)
    // {
    //     // >find nearest window, if targetWindow.isValie==true >>> move to targetWindow.child("entryPoint")
    //     // >if (isInside)

    //     if (entryManager.availableEntries.Count > 0)
    //     {
    //         // GameObject[] entries;
    //         // entries = entryManager.availableEntries;
    //         GameObject closest = null;
    //         float closeDistance = Mathf.Infinity;
    //         Vector3 pos = transform.position;
    //         foreach (GameObject entry in entryManager.availableEntries)
    //         {
    //             Vector3 diff = entry.transform.position - pos;
    //             float curDistance = diff.sqrMagnitude;
    //             if (curDistance < closeDistance)
    //             {
    //                 closest = entry;
    //                 closeDistance = curDistance;
    //             }
    //         }


    //         navTarget = closest.transform.Find("entryPoint").gameObject; //placeholder
    //         targetRange = closeDistance;
    //     }
    //     else
    //     {
    //         navTarget = null;
    //         targetRange = 0f;
    //     }
        

    // }


    // private void calculateNearestPlayerNav(out GameObject navTarget, out float targetRange)
    // {
    //     GameObject[] players;
    //     players = GameObject.FindGameObjectsWithTag("Player");
    //     GameObject closest = null;
    //     float closeDistance = Mathf.Infinity;
    //     Vector3 pos = transform.position;
    //     foreach (GameObject player in players)
    //     {
    //         Vector3 diff = player.transform.position - pos;
    //         float curDistance = diff.sqrMagnitude;
    //         if (curDistance < closeDistance)
    //         {
    //             closest = player;
    //             closeDistance = curDistance;
    //         }
    //     }
    //     navTarget = closest;
    //     targetRange = closeDistance;
        
    // }

    // #endregion


    // #region coroutines
    // IEnumerator attemptEntry()
    // {
    //     BarricadeController TEBC;
    //     int barHealth;

    //     TEBC = targetEntry.transform.parent.GetComponent<BarricadeController>();
    //     barHealth = TEBC.getBarHealth();
    //     if(barHealth > 0)
    //     {
    //         yield return new WaitForSeconds(1f);
    //         TEBC.updateBoards(false);
    //     } else 
    //     {
    //         bool check = TEBC.getIsOccupied();
    //         if (check == false )
    //         yield return enterPlaceHolder(TEBC);

    //     }

    //     // yield return new WaitForSeconds(0.3f);
    //     isBusy = false;
    // }

    // IEnumerator enterPlaceHolder(BarricadeController TEBC)
    // {
    //     Vector3 myPos = this.gameObject.transform.position;
    //     GameObject exitPoint = TEBC.getExitPoint();
    //     Vector3 targetPos = exitPoint.transform.position;
    //     int timer = 0;
    //     gameObject.GetComponent<NavMeshAgent>().enabled=false;
    //     while(timer < 5)
    //     {
    //         this.gameObject.transform.position = Vector3.Lerp(myPos, targetPos, 0.2f*timer);
    //         timer++;
    //         yield return new WaitForSeconds(0.5f);
    //     }
    //     if (timer >= 5)
    //     {
    //         setIsInside(true);
    //         setAtWindow(false);
    //     }
    //     gameObject.GetComponent<NavMeshAgent>().enabled=true;

        
    //     //         gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition, ADSPosition.transform.localPosition, ADSSpeed * Time.deltaTime);

    // }

    // #endregion

    // #region gets&sets  
    // public void setCanMove(bool to)
    // {
    //     canMove = to;
    // }

    // public bool getCanMove()
    // {
    //     return canMove;
    // }

    // public void setInterruptMove(bool to)
    // {
    //     interruptMove = to;
    // }

    // public bool getInterruptMove()
    // {
    //     return interruptMove;
    // }

    // public void setAtWindow(bool to)
    // {
    //     atWindow = to;
    // }
    // public bool getAtWindow()
    // {
    //     return atWindow;
    // }

    // public void setIsInside(bool to)
    // {
    //     isInside = to;
    // }
    // public void setTargetEntry(GameObject to)
    // {
    //     targetEntry = to;
    // }
    // public bool getIsInside()
    // {
    //     return isInside;
    // }
    //  #endregion 

}
