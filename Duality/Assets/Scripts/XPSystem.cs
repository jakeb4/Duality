using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class XPSystem : MonoBehaviour
{

    public int rand1;
    public int rand2;
    public Slider pistolXP_slider;
    public Slider ShotGunXP_slider;
    public Slider KnifeXP_slider;

    public Text PistolLevel;
    public Text ShotgunLevel;
    public Text knifeLevel;
    private int pistollevel;
    private int shotgunlevel;
    private int knifelevel;

    private float pistolXP;
    private float ShotGunXP;
    private float knifeXP;

    vp_WeaponHandler weaponhandler;
    vp_PlayerInventory playerinventory;
    float itemid;
    private string currentWeapon;


    public GameObject pistolbullet;
    public GameObject shotgunPellet;
    public GameObject knifeAttack;

    vp_FXBullet pistolBullet;
    vp_FXBullet Shotgun;
    vp_FXBullet knife;
   
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject knifeObject;


    // Use this for initialization
    void Start()
    {
        

        pistolBullet = pistolbullet.GetComponent<vp_FXBullet>();
        Shotgun = shotgunPellet.GetComponent<vp_FXBullet>();
        knife = knifeAttack.GetComponent<vp_FXBullet>();

        weaponhandler = gameObject.GetComponent<vp_WeaponHandler>();
        playerinventory = gameObject.GetComponent<vp_PlayerInventory>();
        pistollevel = 0;
        PistolLevel.text = pistollevel.ToString();
        pistolXP_slider.value = 0;

        shotgunlevel = 0;
        ShotgunLevel.text = shotgunlevel.ToString();
        ShotGunXP_slider.value = 0;

        knifelevel = 0;
        knifeLevel.text = knifelevel.ToString();
        KnifeXP_slider.value = 0;

        //pistolBullet.Damage = 1;
        //Shotgun.Damage = 1;
        //knife.Force = 5;


    }

    // Update is called once per frame
    void Update()
    {
        pistol = GameObject.Find("1PistolTransform");
        shotgun = GameObject.Find("3ShotgunTransform");
        knifeObject = GameObject.Find("5KnifeTransform");

        if (pistol != null)
        {
            currentWeapon = "pistol";
        }

        else if (shotgun != null)
        {
            currentWeapon = "shotgun";
        }
        else if (knifeObject != null)
        {
            print("knife");
            currentWeapon = "knife";
        }
        else
            currentWeapon = null;
        //itemid = playerinventory.CurrentWeaponIdentifier.GetItemID();
        //if (itemid != null)
        //{
        //    itemid = playerinventory.CurrentWeaponIdentifier.GetInstanceID();
        //}
        // add xp to the gun that killed enemy when enemy is killed

        //print(itemid);
        //print(pistolXP_slider.value);
        print(ShotGunXP_slider.value);

        if (pistolXP_slider.value == 100)
        {
            pistolXP = 0;
            pistolXP_slider.value = 0;
            LevelUp();
            pistolBullet.Damage += 1;
        }
        if(ShotGunXP_slider.value == 100)
        {
            ShotGunXP_slider.value = 0;
            LevelUp();
            Shotgun.Damage += 1;
        }
        if(KnifeXP_slider.value == 100)
        {
            KnifeXP_slider.value = 0;
            LevelUp();
            knife.Force += 5;
        }
    }

    public void AddXP()
    {
        if (currentWeapon == "pistol")
        {
            float value = Random.Range(rand1, rand2);
            pistolXP += value;
            StartCoroutine(AnimateSliderOverTime(1));
        }
        if (currentWeapon == "shotgun")
        {
            float value = Random.Range(rand1, rand2);
            ShotGunXP_slider.value += value;
            ShotGunXP += value;
            StartCoroutine(AnimateSliderOverTime(1));
        }
        if (currentWeapon == "knife")
        {
            float value = Random.Range(rand1, rand2);
            KnifeXP_slider.value += value;
            knifeXP += value;
            StartCoroutine(AnimateSliderOverTime(1));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "thing")
        {
            AddXP();
        }
    }

    void LevelUp()
    {
        if (currentWeapon == "pistol")
        {
            pistollevel++;
            PistolLevel.text = pistollevel.ToString();
            Instantiate(Resources.Load("ExpPlayerEffect"), transform.position, transform.rotation);
        }
        if (currentWeapon == "shotgun")
        {
            shotgunlevel++;
            ShotgunLevel.text = shotgunlevel.ToString();
            Instantiate(Resources.Load("ExpPlayerEffect"), transform.position, transform.rotation);
        }
        if(currentWeapon == "knife")
        {
            knifelevel++;
            knifeLevel.text = knifelevel.ToString();
            Instantiate(Resources.Load("ExpPlayerEffect"), transform.position, transform.rotation);
        }
    }

    IEnumerator AnimateSliderOverTime(float seconds)
    {
        if (currentWeapon == "pistol")
        {
            float leftOver = 0;
            float animationTime = 0f;
            while (animationTime < seconds)
            {
                animationTime += Time.deltaTime;
                float lerpValue = animationTime / seconds;
                if (pistolXP > 100)
                {
                    //level++;
                    leftOver += pistolXP - 100;
                    pistolXP_slider.value = Mathf.Lerp(pistolXP_slider.value, 100, lerpValue);
                    if (pistolXP_slider.value == 100)
                    {
                        pistollevel++;
                        PistolLevel.text = pistollevel.ToString();
                        pistolXP_slider.value = 0;
                        pistolXP = 0;
                    }

                }
                else if (pistolXP < 100)
                {
                    pistolXP_slider.value = Mathf.Lerp(pistolXP_slider.value, pistolXP, lerpValue);
                }
                //if (leftOver != 0 && xp == 0)
                //{

                //    xp_slider.value = 0;
                //    xp_slider.value = Mathf.Lerp(0, leftOver, leftOver / lerpValue);
                //    //print(leftOver);
                //    leftOver = 0;
                //}

                yield return null;
            }
            if (currentWeapon == "shotgun")
            {
                float _leftOver = 0;
                float _animationTime = 0f;
                while (_animationTime < seconds)
                {
                    _animationTime += Time.deltaTime;
                    float lerpValue = _animationTime / seconds;
                    if (ShotGunXP > 100)
                    {
                        //level++;
                        _leftOver += ShotGunXP - 100;
                        ShotGunXP_slider.value = Mathf.Lerp(ShotGunXP_slider.value, 100, lerpValue);
                        if (ShotGunXP_slider.value == 100)
                        {
                            pistollevel++;
                            PistolLevel.text = pistollevel.ToString();
                            ShotGunXP_slider.value = 0;
                            ShotGunXP = 0;
                        }

                    }
                    else if (pistolXP < 100)
                    {
                        ShotGunXP_slider.value = Mathf.Lerp(ShotGunXP_slider.value, ShotGunXP, lerpValue);
                    }
                    //if (leftOver != 0 && xp == 0)
                    //{

                    //    xp_slider.value = 0;
                    //    xp_slider.value = Mathf.Lerp(0, leftOver, leftOver / lerpValue);
                    //    //print(leftOver);
                    //    leftOver = 0;
                    //}

                    yield return null;
                }
                //if (itemid == -1178)
                //{
                //    //float leftOver = 0;
                //    //float animationTime = 0f;
                //    while (animationTime < seconds)
                //    {
                //        animationTime += Time.deltaTime;
                //        float lerpValue = animationTime / seconds;
                //        if (ShotGunXP > 100)
                //        {
                //            //level++;
                //            leftOver += ShotGunXP - 100;
                //            ShotGunXP_slider.value = Mathf.Lerp(ShotGunXP_slider.value, 100, lerpValue);
                //            if (ShotGunXP_slider.value == 100)
                //            {
                //                level++;
                //                Level.text = level.ToString();
                //                ShotGunXP_slider.value = 0;
                //                ShotGunXP = 0;
                //            }

                //        }
                //        else if (pistolXP < 100)
                //        {
                //            ShotGunXP_slider.value = Mathf.Lerp(ShotGunXP_slider.value, ShotGunXP, lerpValue);
                //        }
                //        //if (leftOver != 0 && xp == 0)
                //        //{

                //        //    xp_slider.value = 0;
                //        //    xp_slider.value = Mathf.Lerp(0, leftOver, leftOver / lerpValue);
                //        //    //print(leftOver);
                //        //    leftOver = 0;
                //        //}

                //        yield return null;
                //    }
                //}
            }
        }
    }
}
