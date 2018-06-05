using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.AI;

public class SwitchDimensions : MonoBehaviour
{
    //Variable to check it the dimension Switched
    public static bool Switched;
    //Reference to the Post Processing Profiles
    [SerializeField]
    private PostProcessingProfile SecondDim;
    [SerializeField]
    private PostProcessingProfile FirstDim;

    //reference to the weapon camera
    private GameObject WeaponCamera;

    //public Material OtherDimension;
    //public Material RegularDimension;
    private GameObject UpsideDownFX;
    private GameObject FirstDimFX;
    private GameObject FirstDimLights;
    private GameObject SecondDimLights;

    private bool PressedButton;
    private bool NearButton;


    private float shininess;
    Renderer thisRend;

    public float FirstwaitTime = 3.0f;
    public float SecondwaitTime = 6.0f;
    public GameObject fpsCam;
    public float cameraShake = 3.0f;
    vp_FPCamera cameraObject;
    private vp_FPPlayerEventHandler m_Player;

    private float ShakeTimer;

    private MeshRenderer[] AllMeshs;
    public List<MeshRenderer> GreenLightDoors;
    public List<MeshRenderer> RedLightDoors;
    public Material greenLight;
    public Material redLight;

    //Reference to the Alien Technology object and the battery you need for it.
    private GameObject alienTech;
    private GameObject alienBattery;

    Interact interact;

    public GameObject SwitchingAudio;
    private GameObject SwitchingAudioClone;
    private AudioSource audioSource;

    //Reference to all audio objects
    [SerializeField]
    private GameObject firstDimA;
    private AudioSource FirstDimAS;

    [SerializeField]
    private GameObject SecondDimA;
    private AudioSource SecondDimAS;

    [SerializeField]
    private GameObject BatteryPlaceAudio;
    private AudioSource BatteryPlace;

    [SerializeField]
    private GameObject G_putinBattery;
    private AudioSource A_putinBattery;
    private bool soundPlayed = false;


    // Use this for initialization
    void Start()
    {
        //Initialize Camera Objects
        WeaponCamera = GameObject.Find("WeaponCamera");
        fpsCam = GameObject.Find("FPSCamera");

        //Initialize Post Processing Profiles
        FirstDim = Resources.Load("FirstDim") as PostProcessingProfile;
        SecondDim = Resources.Load("SecondDim") as PostProcessingProfile;


        interact = gameObject.GetComponent<Interact>();
        AllMeshs = FindObjectsOfType<MeshRenderer>();

        UpsideDownFX = GameObject.Find("UpsideDownFX");
        FirstDimFX = GameObject.Find("FirstDimFX");
        FirstDimLights = GameObject.Find("FirstDimLights");
        SecondDimLights = GameObject.Find("SecondDimLights");

        cameraObject = fpsCam.GetComponent<vp_FPCamera>();
        cameraObject.ShakeSpeed = 0.0f;
        ShakeTimer = 0.0f;
        WeaponCamera.GetComponent<PostProcessingBehaviour>().profile = FirstDim;
        SwitchBack();
        UpsideDownFX.SetActive(false);
        m_Player = this.GetComponent<vp_FPPlayerEventHandler>();
        //StartCoroutine(SwichOverTime());
        PressedButton = false;


        firstDimA = Instantiate(Resources.Load("FirstDimAmbience"), transform.position, Quaternion.identity) as GameObject;
        FirstDimAS = firstDimA.GetComponent<AudioSource>();

        SecondDimA = Instantiate(Resources.Load("SecondDimAmbience"), transform.position, Quaternion.identity) as GameObject;
        SecondDimAS = SecondDimA.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Switched)
        {
            //play first dimension ambience
            if (FirstDimAS.isPlaying != true)
            {
                FirstDimAS.Play();
                SecondDimAS.Stop();
            }     
        }
        else
        {
            //play second dimension ambience
            if (SecondDimAS.isPlaying != true)
            {
                FirstDimAS.Stop();
                SecondDimAS.Play();
            }
        }
        
        //Once the player is near the alien tech, interact with it.
        if (NearButton == true)
        {
            //Make sure the player is looking at the object.
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1.25f))
            {
                if(hit.collider.gameObject.tag == "AlienTech")
                {
                    if (alienBattery.activeInHierarchy == true)
                    {
                        m_Player.HUDText.Send("Press E Interact");
                    }
                    else if (alienBattery.activeInHierarchy == false)
                    {
                        m_Player.HUDText.Send("Not Charged." + '\n' + "Press E to charge.");
                        if (interact.batteryCollected == true)
                        {
                            if (Input.GetKeyDown(KeyCode.E) == true)
                            {
                                if (G_putinBattery == null)
                                {
                                    G_putinBattery = Instantiate(Resources.Load("PutInBatterySound"), transform.position, transform.rotation) as GameObject;
                                    A_putinBattery = G_putinBattery.GetComponent<AudioSource>();
                                }
                                if (soundPlayed == true)
                                {
                                    A_putinBattery.Play();
                                }
                                interact.batteryCollected = false;
                                Invoke("Charge", .1f);
                                soundPlayed = false;
                            }
                        }
                    }

                    if (PressedButton == false)
                    {
                        if (alienBattery.activeInHierarchy == false)
                        {
                            if (interact.batteryIcon.activeInHierarchy == true)
                            {
                                m_Player.HUDText.Send("Not Charged. Press E to add piece.");
                            }
                            else
                                m_Player.HUDText.Send("Not Charged. You're missing a piece.");
                        }
                        if (Input.GetKeyDown(KeyCode.E) == true)
                        {
                            //See if it's charged.
                            if (alienBattery.activeInHierarchy == false)
                            {
                                //m_Player.HUDText.Send("Not Charged. You're missing a piece.");
                            }
                            //if it is charged, switch.
                            else if (alienBattery.activeInHierarchy == true)
                            {
                                print("charged!");
                                //Switched = true
                                PressedButton = true;
                            }
                        }
                    }
                }   
            }        
        }
        if (PressedButton == true)
        {
            //if item is in thing
            //check for item
            if (Switched == false)
            {
                //shake happens
                cameraObject.ShakeSpeed = cameraShake;

                if (SwitchingAudioClone == null)
                {
                    SwitchingAudioClone = Instantiate(SwitchingAudio, transform.position, Quaternion.identity);
                    audioSource = SwitchingAudioClone.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        StartCoroutine(FadeOut(audioSource, 3.0f));
                    }

                }

                ShakeTimer += Time.deltaTime;
                if (ShakeTimer >= 3.1f)
                {
                    if (SwitchingAudioClone.activeInHierarchy == true)
                    {
                        Destroy(SwitchingAudioClone);
                    }
                    cameraObject.ShakeSpeed = 0.0f;
                    Switch();
                    ShakeTimer = 0;
                    PressedButton = false;
                }

            }
            if (Switched == true)
            {
                //
                if (SwitchingAudioClone == null)
                {
                    SwitchingAudioClone = Instantiate(SwitchingAudio, transform.position, Quaternion.identity);
                    audioSource = SwitchingAudioClone.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        StartCoroutine(FadeOut(audioSource, 3.0f));
                    }

                }
                //shake happens
                cameraObject.ShakeSpeed = cameraShake / 2;
                ShakeTimer += Time.deltaTime;
                if (ShakeTimer >= 3.1f)
                {
                    if (SwitchingAudioClone.activeInHierarchy == true)
                    {
                        Destroy(SwitchingAudioClone);
                    }
                    cameraObject.ShakeSpeed = 0.0f;
                    SwitchBack();
                    ShakeTimer = 0;
                    PressedButton = false;
                }
            }
        }
        else
            cameraObject.ShakeSpeed = 0.0f;


        //check for green light materials
        //for (int i = 0; i < AllMeshs.Length; i++)
        //{
        //    if (AllMeshs[i].material.name == "GreenLight (Instance)")
        //    {
        //        GreenLightDoors.Add(AllMeshs[i]);
        //        GreenLightDoors.ToArray();
        //    }
        //    else if (AllMeshs[i].material.name == "RedLight (Instance)")
        //    {
        //        RedLightDoors.Add(AllMeshs[i]);
        //    }
        //}
    }

    //Fade the Audio out when it plays.
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        
        while (audioSource.volume > 0)
        {
            if (audioSource != null)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            }

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "AlienTech")
        {
            alienTech = col.gameObject;
            //alienTechAudio = col.gameObject.GetComponent<AudioSource>();
            alienBattery = col.gameObject.transform.GetChild(0).gameObject;
            NearButton = true;
        }
        if (col.gameObject.tag == "SwitchDimensionCollider")
        {
            PressedButton = true;
            col.gameObject.SetActive(false);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "AlienTech")
        {
            NearButton = false;
        }
    }

    //Change The SkyLight and possible materials of all objects.
    void Switch()
    {
        //alienTechAudio.enabled = false;
        Switched = true;
        WeaponCamera.GetComponent<PostProcessingBehaviour>().profile = SecondDim;
        ShakeTimer = 0.0f;
        //RenderSettings.skybox = OtherDimension;
        RenderSettings.fog = true;
        DynamicGI.UpdateEnvironment();
        UpsideDownFX.SetActive(true);
        //UpsideDownFX.transform.Translate(gameObject.transform.position.x, 0, 0);
        SecondDimLights.SetActive(true);

        FirstDimLights.SetActive(false);
        FirstDimFX.SetActive(false);


        //check through all meshes looking for light gameobjects.

        //for (int i = 0; i < AllMeshs.Length; i++)
        //{
        //    if (AllMeshs[i].material.name == "GreenLight (Instance)")
        //    {
        //        //GreenLightDoors.Add(AllMeshs[i]);
        //        //GreenLightDoors.ToArray();
        //        AllMeshs[i].material = redLight;
        //    }
        //    else if (AllMeshs[i].material.name == "RedLight (Instance)")
        //    {
        //        RedLightDoors.Add(AllMeshs[i]);
        //        AllMeshs[i].material = greenLight;
        //    }
        //}
    }

    void SwitchBack()
    {
        ShakeTimer = 0.0f;
        Switched = false;
        WeaponCamera.GetComponent<PostProcessingBehaviour>().profile = FirstDim;
        cameraObject.ShakeSpeed = 0.0f;
        //RenderSettings.skybox = RegularDimension;
        RenderSettings.fog = false;
        DynamicGI.UpdateEnvironment();
        UpsideDownFX.SetActive(false);
        SecondDimLights.SetActive(false);

        FirstDimLights.SetActive(true);
        FirstDimFX.SetActive(true);

        //check through all meshes looking for all gameobjects with a specific material.

        //for (int i = 0; i < AllMeshs.Length; i++)
        //{
        //    if (AllMeshs[i].material.name == "GreenLight (Instance)")
        //    {
        //        //GreenLightDoors.Add(AllMeshs[i]);
        //        //GreenLightDoors.ToArray();
        //        AllMeshs[i].material = redLight;
        //    }
        //    else if (AllMeshs[i].material.name == "RedLight (Instance)")
        //    {
        //        RedLightDoors.Add(AllMeshs[i]);
        //        AllMeshs[i].material = greenLight;
        //    }
        //}
        //Enemy.GetComponent<MeshRenderer>().material = DimMaterial;
    }

    void Charge()
    {
        alienBattery.SetActive(true);
        //DimensionalBattery.charged = true;
        interact.batteryIcon.SetActive(false);
        //play Audio
        if(BatteryPlaceAudio == null)
        {
            GameObject batteryplaceInstance = Instantiate(Resources.Load("AttachObjectSound"),transform.position,transform.rotation) as GameObject;
            BatteryPlace = batteryplaceInstance.GetComponent<AudioSource>();
        }
        BatteryPlace.Play();
    }

    //Switch Dimensions via Timer

    //private IEnumerator SwichOverTime()
    //{
    //    //float waitTime = Random.Range(2, 20);
    //    yield return new WaitForSeconds(FirstwaitTime);
    //    Switch();
    //    yield return StartCoroutine(SwichBackOverTime());

    //}
    //private IEnumerator SwichBackOverTime()
    //{
    //    //float waitTime = Random.Range(2, 20);
    //    yield return new WaitForSeconds(SecondwaitTime);
    //    SwitchBack();
    //    yield return StartCoroutine(SwichOverTime());

    //}

}
