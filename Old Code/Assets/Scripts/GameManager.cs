using UnityEngine;
using UnityEngine.UI; // Note this new line is needed for UI
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public Text gameOverText;
    public Text gameWonText;
    CollisionDetector collisionDetector;
    public List<GameObject> rocks;
    public List<GameObject> antirocks;
    public List<GameObject> MeMoRocks;
    public List<GameObject> attack;
    public List<GameObject> circles;
    public List<GameObject> lasers;
    public List<GameObject> movingLasers;
    public GameObject rocket;
    public GameObject monster;
    public GameObject healthBar;
    ShipControl shipControl;
    BulletSpawn bulletSpawn;
    GameObject antirock;
    GameObject rock;
    public GameObject MeMoRock;
    public double elapsedTime;
    Transform bar;

    int playerScore = 0;
    int playerHealth = 300;
    int monsterHealth = 2000;
    int gemsAquired;
    int time = 180;
    int damage = 15;
    int lastDamageTime = 0;
    int laserTime = 0;
    int lastShot = 0;

    bool rockOut;
    bool fireShot = false;  
    bool tripleShot = false;
    bool quickShot = false;
    public bool bossBattle = false;

    string fire;
    string triple;
    string quick;

    AudioSource audioSource;
    public AudioClip main;
    public AudioClip boss;
    public AudioClip shoot;
    public AudioClip gem;
    public AudioClip monsterSound;
    public AudioClip hit;
    public AudioClip rockBreak;
    public AudioClip circleShoot;
    public AudioClip laserShoot;
    public AudioClip purchase;

    void Start()
    {

        collisionDetector = gameObject.GetComponent<CollisionDetector>();
        rocks = new List<GameObject>();
        antirocks = new List<GameObject>();
        MeMoRocks = new List<GameObject>();
        attack = new List<GameObject>();
        circles = new List<GameObject>();
        lasers = new List<GameObject>();
        movingLasers = new List<GameObject>();
        shipControl = rocket.GetComponent<ShipControl>();
        bulletSpawn = rocket.GetComponent<BulletSpawn>();
        elapsedTime = 0f;
        monster.SetActive(false);
        healthBar.SetActive(false);
        bar = healthBar.transform.Find("Bar");
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = main;
        audioSource.loop = true;
        audioSource.Play();

    }


    void Update()
    {

        elapsedTime += Time.deltaTime;

        //Exercise 15 requires that you check for collisions between MeMoRocks and the Rocket

        //check to see whether any rock has rolled onto the rocket
        foreach (GameObject rck in rocks)
        {
            if (collisionDetector.AABBTest(rocket, rck))
            {
                RemoveRockFromList(rck);
                Destroy(rck);
                PlayerDamage();
                break;
            }

        }

        foreach (GameObject memorock in MeMoRocks)
        {
            if (memorock.transform.position.x + memorock.GetComponent<SpriteInfo>().upRight.x < -17)
            {
                RemoveMeMoRockFromList(memorock);
                Destroy(memorock);
                break;

            }

            if (collisionDetector.AABBTest(rocket, memorock))
            {
                RemoveMeMoRockFromList(memorock);
                Destroy(memorock);
                gemsAquired++;
                audioSource.PlayOneShot(gem, 0.8f);
                break;
            }
        }

        rockOut = false;

        foreach (GameObject antirck in antirocks)
        {
            foreach (GameObject rck in rocks)
            {
                if (collisionDetector.AABBTest(antirck, rck))
                {
                    rockOut = true;
                    antirock = antirck;
                    rock = rck;
                    audioSource.PlayOneShot(rockBreak, 0.7f);
                    break;
                }
            }

            if (rockOut)
                break;
        }

        foreach(GameObject hit in attack)
        {

            if (hit.transform.position.x + hit.GetComponent<SpriteInfo>().upRight.x < -17)
            {
                RemoveMissileFromList(hit);
                Destroy(hit);
                break;

            }

            if (collisionDetector.AABBTest(rocket, hit))
            {
                RemoveMissileFromList(hit);
                Destroy(hit);
                PlayerDamage();
                break;
            }
        }

        if (rockOut)
        {
            //Exercise 15 requires that you modify this code as such:
            //a new GameObject called a MeMoRock should be instantiated with the same position and velocity as the Rock that collided with the AntiRock
            RemoveRockFromList(rock);
            RemoveAntiRockFromList(antirock);
            Destroy(rock);
            Destroy(antirock);
            AddScore();
            AddMeMoRockFromList(Instantiate(MeMoRock, new Vector3(rock.transform.position.x, rock.transform.position.y, rock.transform.position.z), Quaternion.identity));
        }

        if (playerHealth == 0)
        {
            PlayerDied();
        }

        //Exercise 15 requires that you remove MeMoRocks that have gone out of bounds, to the left of the Rockeet

        if (Input.GetKeyDown(KeyCode.Alpha1) && gemsAquired >= 50 && !fireShot){
            fireShot = true;
            gemsAquired = gemsAquired - 50;
            damage = damage * 2;
            audioSource.PlayOneShot(purchase, 0.7f);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && gemsAquired >= 50 && !tripleShot)
        {
            tripleShot = true;
            gemsAquired = gemsAquired - 50;
            audioSource.PlayOneShot(purchase, 0.7f);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && gemsAquired >= 50 && !quickShot)
        {
            quickShot = true;
            gemsAquired = gemsAquired - 50;
            audioSource.PlayOneShot(purchase, 0.7f);
        }

        if (time - (int)elapsedTime <= 0 && !bossBattle)
        {
            bossBattle = true;
            audioSource.Stop();
            audioSource.clip = boss;
            audioSource.Play();
            audioSource.PlayOneShot(monsterSound, 1.5f);
            monster.SetActive(true);
            healthBar.SetActive(true);
        }

        if (bossBattle)
        {
            foreach (GameObject rck in rocks)
            {
                rock = rck;
                RemoveRockFromList(rock);
                RemoveAntiRockFromList(antirock);
                Destroy(rock);
                break;
            }

            foreach (GameObject memorock in MeMoRocks)
            {
                memorock.GetComponent<RockMover>().bossBattle = true;

            }

            foreach (GameObject antirck in antirocks)
            {
                if (collisionDetector.AABBTest(monster, antirck))
                {
                    MonsterDamage();
                    RemoveAntiRockFromList(antirck);
                    Destroy(antirck);
                    break;
                }
            }

            foreach (GameObject circle in circles)
            {
                if (collisionDetector.AABBTest(rocket, circle))
                {
                    PlayerDamage();
                    RemoveCircleFromList(circle);
                    Destroy(circle);
                    break;
                }

                if (circle.transform.position.x + circle.GetComponent<SpriteInfo>().upRight.x < -17)
                {
                    RemoveCircleFromList(circle);
                    Destroy(circle);
                    break;

                }
            }

            foreach(GameObject laser in lasers)
            {
                if(collisionDetector.AABBTest(rocket, laser))
                {
                    PlayerDamage();
                    RemoveLaserFromList(laser);
                    Destroy(laser);
                    break;
                }
            }

            foreach (GameObject laser in movingLasers)
            {
                if (collisionDetector.AABBTest(rocket, laser))
                {
                    PlayerDamage();
                    RemoveMovingLaserFromList(laser);
                    Destroy(laser);
                    break;
                }
            }

            if (collisionDetector.AABBTest(rocket, monster))
            {
                if (lastDamageTime <= (int)elapsedTime - 1)
                {
                    lastDamageTime = (int)elapsedTime;
                    PlayerDamage();
                }
            }

            if (lastShot <= (int)elapsedTime -1)
            {
                lastShot = (int)elapsedTime;
                float theta;
                theta = calculateTheta(monster.transform.position);
                shipControl.purSeek(theta, fireShot, quickShot);
                if (tripleShot)
                {
                    shipControl.purSeek(theta + 10, fireShot, quickShot);
                    shipControl.purSeek(theta - 10, fireShot, quickShot);
                }
            }

            if (!(monsterHealth / 2000f < 0))
            {
                bar.localScale = new Vector3(monsterHealth / 2000f, 1f);
            }
            else
            {
                bar.localScale = new Vector3(0f, 1f);
            }

            if(monsterHealth <= 0)
            {
                MonsterDied();
            }

            if (laserTime == (int)elapsedTime - 5)
            {
                foreach (GameObject laser in lasers)
                {
                        RemoveLaserFromList(laser);
                        Destroy(laser);
                        break;
                }

                foreach (GameObject laser in movingLasers)
                {
                    RemoveMovingLaserFromList(laser);
                    Destroy(laser);
                    break;
                }
            }

        }

    }

    //Exercise 15 requires that you implement this method, using the formula derived in Case Study 15
    public float calculateTheta(Vector3 pos)
    {
        float A, B, C, t;
        float p1, p2, q1, q2;
        float w, s;
        float discriminant;
        float theta;

        q1 = pos.x;
        q2 = pos.y;

        p1 = rocket.transform.position.x;
        p2 = rocket.transform.position.y;

        //theta = 0f;  //initial value

        w = 2f; //NOTE: could obtain this value through RockMover.speed
        s = 10f; //NOTE: could obtain this through AntiRock.speed

        A = (Mathf.Pow(s, 2) - Mathf.Pow(w, 2));
        B = 2 * w * (q1 - p1);
        C = -(Mathf.Pow(q1 - p1, 2) + Mathf.Pow(q2 - p2, 2));

        discriminant = Mathf.Pow(B, 2) - (4 * A * C);

        t = (-B + Mathf.Sqrt(discriminant)) / (2 * A);

        theta = (Mathf.Asin((q2 - p2) / (t * s))) * Mathf.Rad2Deg;

        return theta;
    }

    public void AddRockToList(GameObject rock)  //Note this is where calculateTheta() is called, followed by call to purSeek(theta)
    {
        float theta;
        rocks.Add(rock);
        theta = calculateTheta(rock.transform.position);
        shipControl.purSeek(theta, fireShot, quickShot);
        if (tripleShot)
        {
            shipControl.purSeek(theta + 10, fireShot, quickShot);
            shipControl.purSeek(theta - 10, fireShot, quickShot);
        }
    }

    public bool RemoveRockFromList(GameObject rock)
    {
        return rocks.Remove(rock);
    }

    public void AddMissileToList(GameObject missile)
    {
        attack.Add(missile);
    }

    public bool RemoveMissileFromList(GameObject missile)
    {
        return attack.Remove(missile);
    }


    public void AddMeMoRockFromList(GameObject MeMoRock)
    {
        MeMoRocks.Add(MeMoRock);
    }

    public bool RemoveMeMoRockFromList(GameObject MeMoRock)
    {
        return MeMoRocks.Remove(MeMoRock);
    }

    public void AddAntiRockToList(GameObject antirock)
    {
        antirocks.Add(antirock);
        audioSource.PlayOneShot(shoot, 0.7f);
    }

    public bool RemoveAntiRockFromList(GameObject antirock)
    {
        return antirocks.Remove(antirock);
    }

    public void AddCircleFromLIst(GameObject Circle, GameObject Circle2)
    {
        circles.Add(Circle);
        circles.Add(Circle2);
        audioSource.PlayOneShot(circleShoot, 0.7f);
    }

    public bool RemoveCircleFromList(GameObject Circle)
    {
        return circles.Remove(Circle);
    }

    public void AddLaserFromList(GameObject laser, GameObject laser2, GameObject laser3)
    {
        lasers.Add(laser);
        lasers.Add(laser2);
        lasers.Add(laser3);
        laserTime = (int)elapsedTime;
        audioSource.PlayOneShot(laserShoot, 0.7f);
    }

    public bool RemoveLaserFromList(GameObject laser)
    {
        return lasers.Remove(laser);
    }

    public void AddMovingLaserFromList(GameObject laser)
    {
        movingLasers.Add(laser);
        laserTime = (int)elapsedTime;
        audioSource.PlayOneShot(laserShoot, 0.7f);
    }

    public bool RemoveMovingLaserFromList(GameObject laser)
    {
        return movingLasers.Remove(laser);
    }

    public void AddScore()
    {
        playerScore++;
        //This converts the score (a number) into a string
       
    }

    public void PlayerDied()
    {
        gameOverText.enabled = true;
        audioSource.Stop();

        // This freezes the game
        Time.timeScale = 0;
    }

    public void MonsterDied()
    {
        gameWonText.enabled = true;
        Time.timeScale = 0;
        audioSource.Stop();
    }

    public void PlayerDamage()
    {
        playerHealth = playerHealth - 5;
        audioSource.PlayOneShot(hit, 0.7f);

    }

    public void MonsterDamage()
    {
        monsterHealth = monsterHealth - damage;
    }

    void OnGUI()
    {

        GUI.color = Color.white;
        GUI.skin.box.fontSize = 15;
        GUI.skin.box.wordWrap = false;
        int currentTime = 0;
        if ((time - (int)elapsedTime) > 0)
        {
            currentTime = time - (int)elapsedTime;
        }

        //note:  must use (int) or else the float digits flicker

        GUI.Box(new Rect(0, 0, 300, 30), "Health: " + playerHealth);

        GUI.Box(new Rect(0, 30, 300, 30), "Score: " + playerScore); //NOTE:  the radii are scaled from their initial assigned values in the array declaration

        GUI.Box(new Rect(0, 60, 300, 30), "Time Left: " + currentTime);

        GUI.Box(new Rect(0, 90, 300, 30), "Gems Collcted: " + gemsAquired);

        GUI.Box(new Rect(0, 120, 400, 30), fire);

        GUI.Box(new Rect(0, 150, 400, 30), triple);

        GUI.Box(new Rect(0, 180, 400, 30), quick);

        fire = "Fire Attack (2x Damage): Purchased";
        triple = "Triple Attack: Purchased";
        quick = "Attack Speed Increase: Purchased";

        if (gemsAquired >= 50)
        {
            if (!fireShot)
            {
                fire = $"Fire Attack (2x Damage): <color=green>50 Gems</color> Press 1 to purchase";
            }

            if (!tripleShot)
            {
                triple = "Triple Attack: <color=green>50 Gems</color> Press 2 to purchase";
            }

            if (!quickShot)
            {
                quick = "Attack Speed Increase: <color=green>50 Gems</color> Press 3 to purchase";
            }
        }

        else
        {
            if (!fireShot)
            {
                fire = "Fire Attack (2x Damage): <color=red>50 Gems</color>";
            }

            if (!tripleShot)
            {
                triple = "Triple Attack: <color=red>50 Gems</color>";
            }

            if (!quickShot)
            {
                quick = "Attack Speed Increase: <color=red>50 Gems</color>";
            }
        }

    }
}
