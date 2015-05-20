using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Animals : MonoBehaviour
{

    public PathFinder pathFinder;
    public SpawnControl spawnControl;

    public static int count = 1;

    //Creature type
    /*
     * 1 = landMan
     * 2 = seaMan
     * 
     */
    protected int type = 1;

    //Starting location
    protected int startX = 270;
    protected int startY = 270;

    //Direction of this sprite is facing
    private Direction facing;

    private bool a = false;
    private bool b = false;
    private bool c = false;
    private bool d = false;

    protected const float pixelHeight = 0.023585f;
    // Use this for initialization
 
    // Use this for initialization
    protected virtual void Start()
    {
        if (pathFinder == null)
        {
            pathFinder = PathFinder.Instance();
        }

        if (spawnControl == null)
        {
            spawnControl = SpawnControl.Instance();
        }
        facing = Direction.DONTMOVE;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Choose which heat map as path finding guide
        int[] heatMap = pathFinder.getHeatMap(type);

        //Spawning code
        if (heatMap[startY * 424 + startX] == -1)//|| heatMap[startY * 424 + startX] == 0)
        {
            return;
        }

        if (heatMap[startY * 424 + startX] == 10000 && d == false)
        {
            spawnControl.Spawn(type);
            Destroy(this.gameObject);
            spawnControl.Spawn(-(type));

        }
        else if (heatMap[startY * 424 + startX] == 9900)
        {
            spawnControl.Spawn(type);
        }
        else if (heatMap[startY * 424 + startX] == 9900 && b == false)
        {
            //Instantiate(prefab, new Vector3(-1.36793f, -1.36793f, 0f), Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        }
        else if (heatMap[startY * 424 + startX] == 9850 && a == false)
        {
            //don't know yet
        }

        //moves character using current position and the heatmap choosen
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


    protected Direction checkHeat (int x, int y, int[] heatMap)
    {
        int curr = heatMap[y * 424 + x];
        int newHeight;
        Direction myDirection = Direction.DONTMOVE;
        //int rnd = Random.Range(0, 10);

        //Determine which direction this is heading according to heat and current direction
        if ((x - 1) > 0)
        {
            newHeight = heatMap[y * 424 + (x - 1)];

            if (newHeight > curr)
            {
                curr = newHeight;
                myDirection = Direction.East;
            }
            else if (newHeight == curr && facing == Direction.East)
            {
                curr = newHeight;
                myDirection = Direction.East;
            }
        }

        if ((x + 1) < 423)
        {
            newHeight = heatMap[y * 424 + (x + 1)];

            if (newHeight > curr)
            {
                curr = newHeight;
                myDirection = Direction.West;
            }
            else if (newHeight == curr && facing == Direction.West)
            {
                curr = newHeight;
                myDirection = Direction.West;
            }
        }

        if ((y - 1) > 0)
        {
            newHeight = heatMap[(y - 1) * 424 + x];

            if (newHeight > curr)
            {
                curr = newHeight;
                myDirection = Direction.North;
            }
            else if (newHeight == curr && facing == Direction.North)
            {
                curr = newHeight;
                myDirection = Direction.North;
            }
        }

        if ((x - 1) > 0 && (y - 1) > 0)
        {
            newHeight = heatMap[(y - 1) * 424 + x - 1];

            if (newHeight > curr)
            {
                curr = newHeight;
                myDirection = Direction.NorthEast;
            }
            else if (newHeight == curr && facing == Direction.NorthEast)
            {
                curr = newHeight;
                myDirection = Direction.NorthEast;
            }
        }

        if ((x + 1) < 423 && (y - 1) > 0)
        {
            newHeight = heatMap[(y - 1) * 424 + x + 1];

            if (newHeight > curr)
            {
                curr = newHeight;
                myDirection = Direction.NorthWest;
            }
            else if (newHeight == curr && facing == Direction.NorthWest)
            {
                curr = newHeight;
                myDirection = Direction.NorthWest;
            }
        }

        if ((y + 1) < 423)
        {
            newHeight = heatMap[(y + 1) * 424 + x];

            if (newHeight > curr)
            {
                curr = newHeight;
                myDirection = Direction.South;
            }
            else if (newHeight == curr && facing == Direction.South)
            {
                curr = newHeight;
                myDirection = Direction.South;
            }
        }

        if ((x + 1) < 423 && (y + 1) < 423)
        {
            newHeight = heatMap[(y + 1) * 424 + x + 1];

            if (newHeight > curr)
            {
                curr = newHeight;
                myDirection = Direction.SouthWest;
            }
            else if (newHeight == curr && facing == Direction.SouthWest)
            {
                curr = newHeight;
                myDirection = Direction.SouthWest;
            }
        }

        if ((x - 1) > 0 && (y + 1) < 423)
        {
            newHeight = heatMap[(y + 1) * 424 + x - 1];

            if (newHeight > curr)
            {
                curr = newHeight;
                myDirection = Direction.SouthEast;
            }
            else if (newHeight == curr && facing == Direction.SouthEast)
            {
                curr = newHeight;
                myDirection = Direction.SouthEast;
            }
        }

        //Final direction is returned
        return myDirection;

        /*
        if (rnd < 5)
        {

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
        }*/


        //return Direction.DONTMOVE;
    }


    protected void Move(Direction dir)
    {
        //Debug.Log(i);

        switch (dir)
        {
            case Direction.NorthWest:
                startX++;
                startY--;
                transform.rotation = Quaternion.Euler(0, 0, 45);
                transform.position += new Vector3(-pixelHeight, pixelHeight, 0f);//up left
                break;
            case Direction.North:
                startY--;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.position += new Vector3(0f, pixelHeight, 0f);//up
                break;
            case Direction.NorthEast:
                startX--;
                startY--;
                transform.rotation = Quaternion.Euler(0, 0, 315);
                transform.position += new Vector3(pixelHeight, pixelHeight, 0f);//up right
                break;
            case Direction.West:
                startX++;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.position += new Vector3(-pixelHeight, 0f, 0f);//left
                break;
            case Direction.East:
                startX--;
                transform.rotation = Quaternion.Euler(0, 0, 270);
                transform.position += new Vector3(pixelHeight, 0f, 0f);//right
                break;
            case Direction.SouthWest:
                startX++;
                startY++;
                transform.rotation = Quaternion.Euler(0, 0, 135);
                transform.position += new Vector3(-pixelHeight, -pixelHeight, 0f);//down left
                break;
            case Direction.South:
                startY++;
                transform.rotation = Quaternion.Euler(0, 0, 180);
                transform.position += new Vector3(0f, -pixelHeight, 0f);//down
                break;
            case Direction.SouthEast:
                startX--;
                startY++;
                transform.rotation = Quaternion.Euler(0, 0, 225);
                transform.position += new Vector3(pixelHeight, -pixelHeight, 0f);//down right
                break;
            default:
                //Debug.Log("WTF invalid move!");
                break;
        }













    }
}
