using UnityEngine;
using System.Collections;

public class SpawnControl : MonoBehaviour {
    public GameObject seaMan;
    public GameObject landMan;

    public GameObject sandPitView;

    private int seaManCount = 0;
    private int seaManMax = 20;
    private int landManCount = 0;
    private int landManMax = 20;
    private static bool seaManSpawn = false;
    private static bool landManSpawn = false;

    private static SpawnControl _instance;

    public static SpawnControl Instance()
    {
        if (_instance == null)
        {
            _instance = new SpawnControl();
        }
        return _instance;

    }

	// Use this for initialization
	void Start () {
        Instantiate(seaMan);
        Instantiate(landMan);
	}
	
	// Update is called once per frame
	void Update () {
        if (seaManSpawn == true)
        {
            Debug.Log(seaManCount);
            Debug.Log(seaManMax);
            Instantiate(seaMan, new Vector3(-1.36793f, -1.36793f, 0f), Quaternion.identity);
            seaManCount++;
            seaManSpawn = false;
        }

        if (landManSpawn == true )//&& landManCount < landManMax)
        {
            Instantiate(landMan);
            landManCount++;
            landManSpawn = false;
        }
       

        //Instantiate(seaMan);
	}

    public void Spawn(int type)
    {
        switch (type)
        {
            case 1:
                Debug.Log("SPAWN");
                seaManSpawn = true;
                Debug.Log(seaManSpawn);
                break;
            case 2:
                landManSpawn = true;
                break;
        }
    }
}
