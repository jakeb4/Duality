using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Boo.Lang;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };


    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private GameObject[] enemySpawned;
    private int enemyCount;

    private int MaxEnemies;
    private int currentEnemies;

    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public Transform[] spawnPoints;
    public RuntimeAnimatorController[] anim;
    Animator animator;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    private Transform enemy;
    private NavMeshAgent agent;
    private WanderingAI wanderScript;
    private DamageHandler_Enemy enemyScript;

    private GameObject portalEffect;

    public float WaveCountdown
    {
        get { return waveCountdown; }
    }

    private float searchCountdown = 1f;
    public List<Transform> Children = new List<Transform>();

    private SpawnState state = SpawnState.COUNTING;
    public SpawnState State
    {
        get { return state; }
    }
    //public GameObject[] EnemyCount;

    RuntimeAnimatorController _anim;
    void Start()
    {

        MaxEnemies = 5;
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
        //enemyCount = enemySpawned.Length;
    }

    void Update()
    {
        if (SwitchDimensions.Switched == true)
        {
            enemySpawned = GameObject.FindGameObjectsWithTag("Enemy");
            if (state == SpawnState.WAITING)
            {
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[0]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }

    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {
            nextWave++;
            //IncreaseEnemies(waves[nextWave]);
        }
    }

    bool EnemyIsAlive()
    {
        if (Children != null)
        {
            Children.Clear();
        }
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            //if (GameObject.FindGameObjectWithTag("Enemy") == null)
            //{
            //    return false;
            //}
            foreach (Transform child in transform)
            {
                if (child.tag == "Enemy")
                {
                    if (child != null)
                    {
                        if (Children != null)
                        {
                            Children.Add(child);
                        }
                    }

                }
                //child is your child transform
            }
            if (Children.Count == 0)
            {
                //if (Children.Count == 0)
                //{
                print("children = 0");
                return false;
                //}
            }
            print(Children.Count);
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {

        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.count; i++)
        {
            yield return new WaitForSeconds(1f / _wave.rate);
            SpawnEnemy(_wave.enemy);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        if (_enemy != null)
        {
            Debug.Log("Spawning Enemy: " + _enemy.name);

            animator = _enemy.GetComponent<Animator>();
            //animator.runtimeAnimatorController = anim[Random.Range(0, anim.Length)];
            //_anim = anim[Random.Range(0, anim.Length)];
            Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            enemy = Instantiate(_enemy, _sp.position, _sp.rotation);
            enemy.SetParent(transform);
            portalEffect = Instantiate(Resources.Load("CrazyAssPortal"), new Vector3(_sp.transform.position.x, _sp.transform.position.y + 1.5f, _sp.transform.position.z), _sp.rotation) as GameObject;
            
            //GameObject spawnsound = Instantiate(Resources.Load("SpawnSound"), _sp.position, _sp.rotation) as GameObject;
            Destroy(portalEffect, 3.0f);
            
            //StartCoroutine(SlowGrow(CrazyAssPortal,2.0f));
            
            //wanderScript = _enemy.GetComponent<WanderingAI>();
            //enemyScript = _enemy.GetComponent<DamageHandler_Enemy>();
            //agent = _enemy.GetComponent<NavMeshAgent>();
            //Invoke("EnableNav", .2f);
            //Invoke("EnableWander", .3f);
            //Invoke("EnableEnemy", .4f);
            //_anim = _enemy.GetComponent<RuntimeAnimatorController>();
        }
    }

    void IncreaseEnemies(Wave _wave)
    {
        //for (int i = 0; i < nextWave; i++)
        //{
        //    _wave.count *= 2;
        //}
    }
    public static IEnumerator SlowGrow(GameObject PortalEffect, float GrowTime)
    {
        Vector3 startgrow = new Vector3(.01f,.01f,.01f);

        while (PortalEffect.transform.localScale.x < 1 && PortalEffect.transform.localScale.y < 1 && PortalEffect.transform.localScale.z < 1)
        {

            PortalEffect.transform.localScale += startgrow * Time.deltaTime / GrowTime;


            yield return null;
        }

        
        //audioSource.volume = startVolume;
    }

}
