using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureControl : MonoBehaviour
{

    public PathFinder pathFinder;

    public GameObject prefab;

    public static int count = 1;

    //Starting location
    private int startX = 270;
    private int startY = 270;

    private bool a = false;
    private bool b = false;
    private bool c = false;
    private bool d = false;

    private const float pixelHeight = 0.023585f;
    // Use this for initialization
    void Start()
    {
        if (pathFinder == null)
        {
            count++;
            pathFinder = PathFinder.Instance();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int[] heatMap = pathFinder.getHeatMap();

        if (heatMap[startY * 424 + startX] == -1)//|| heatMap[startY * 424 + startX] == 0)
        {
            return;
        }

        if (heatMap[startY * 424 + startX] == 10000 && d == false)
        {
            //Instantiate(prefab, new Vector3(-1.36793f, -1.36793f, 0f), Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
            d = false;
            count--;
            Destroy(this.gameObject);
        }
        else if (heatMap[startY * 424 + startX] == 9950 && c == false)
        {
            count--;
            c = false;
            if (count < 20)
            {
                Instantiate(prefab, new Vector3(-1.36793f, -1.36793f, 0f), Quaternion.identity);
            }
        }
        else if (heatMap[startY * 424 + startX] == 9900 && b == false)
        {
            count++;
            b = false;
            //Instantiate(prefab, new Vector3(-1.36793f, -1.36793f, 0f), Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        }
        else if (heatMap[startY * 424 + startX] == 9850 && a == false)
        {
            count++;
            a = false;
            if (count < 20)
            {
                Instantiate(prefab, new Vector3(-1.36793f, -1.36793f, 0f), Quaternion.identity);
            }
        }

        Move(checkHeat(startX, startY, heatMap));
    }

    public enum Direction
    {
        North,
        East,
        South,
        West,
        NorthEast,
        NorthWest,
        SouthEast,
        SouthWest,
        DONTMOVE
    }


    private Direction checkHeat(int x, int y, int[] heatMap)
    {
        int curr = heatMap[y * 424 + x];
        int newHeight;

        int rnd = Random.Range(0, 10);

        if (rnd < 5)
        {
            if ((x - 1) > 0)
            {
                newHeight = heatMap[y * 424 + (x - 1)];

                if (newHeight > curr)
                {
                    startX--;
                    return Direction.East;
                }
            }
            if ((x + 1) < 423)
            {
                newHeight = heatMap[y * 424 + (x + 1)];

                if (newHeight > curr)
                {
                    startX++;
                    return Direction.West;
                }
            }

            if ((y - 1) > 0)
            {
                newHeight = heatMap[(y - 1) * 424 + x];

                if (newHeight > curr)
                {
                    startY--;
                    return Direction.North;
                }
            }
            if ((y + 1) < 423)
            {
                newHeight = heatMap[(y + 1) * 424 + x];

                if (newHeight > curr)
                {
                    startY++;
                    return Direction.South;
                }
            }
        }
        else
        {
            if ((y - 1) > 0)
            {
                newHeight = heatMap[(y - 1) * 424 + x];

                if (newHeight > curr)
                {
                    startY--;
                    return Direction.North;
                }
            }
            if ((y + 1) < 423)
            {
                newHeight = heatMap[(y + 1) * 424 + x];

                if (newHeight > curr)
                {
                    startY++;
                    return Direction.South;
                }
            }

            if ((x - 1) > 0)
            {
                newHeight = heatMap[y * 424 + (x - 1)];

                if (newHeight > curr)
                {
                    startX--;
                    return Direction.East;
                }
            }
            if ((x + 1) < 423)
            {
                newHeight = heatMap[y * 424 + (x + 1)];

                if (newHeight > curr)
                {
                    startX++;
                    return Direction.West;
                }
            }
        }


        return Direction.DONTMOVE;
    }


    private void Move(Direction dir)
    {
        //Debug.Log(i);

        switch (dir)
        {
            case Direction.NorthWest:
                transform.position += new Vector3(-pixelHeight, pixelHeight, 0f);//up left
                break;
            case Direction.North:
                transform.position += new Vector3(0f, pixelHeight, 0f);//up
                break;
            case Direction.NorthEast:
                transform.position += new Vector3(pixelHeight, pixelHeight, 0f);//up right
                break;
            case Direction.West:
                transform.position += new Vector3(-pixelHeight, 0f, 0f);//left
                break;
            case Direction.East:
                transform.position += new Vector3(pixelHeight, 0f, 0f);//right
                break;
            case Direction.SouthWest:
                transform.position += new Vector3(-pixelHeight, -pixelHeight, 0f);//down left
                break;
            case Direction.South:
                transform.position += new Vector3(0f, -pixelHeight, 0f);//down
                break;
            case Direction.SouthEast:
                transform.position += new Vector3(pixelHeight, -pixelHeight, 0f);//down right
                break;
            default:
                //Debug.Log("WTF invalid move!");
                break;
        }













    }
}
