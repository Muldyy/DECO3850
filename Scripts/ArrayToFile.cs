using UnityEngine;
using System.Collections;
using System.IO;
using System;


public class ArrayToFile : MonoBehaviour {

	// Use this for initialization

    public DepthSourceManager dsm;
    public ushort[] depth;


	public void Test()
	{
        ushort[] depth = dsm.GetData();
		string path = "C:\\Users\\Studio\\Documents\\DECO3850\\ushort.txt";

        
		using (FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.Write))
		{
            using (StreamWriter sw = new StreamWriter(fs))
			{
				foreach (ushort value in depth)
				{
					sw.Write(Convert.ToString(value) + ",");
				}
			}
		}

   
	

	}



    public ushort[] GetData()
    {
        depth = new ushort[512 * 424];
        string text = System.IO.File.ReadAllText("C:\\Users\\Studio\\Documents\\DECO3850\\ushort.txt");
        string[] lineValues = text.Split(',');
        Debug.Log(lineValues.Length);
        for (int i = 0; i < lineValues.Length - 1; i++)
        {
            //Debug.Log(lineValues[i]);
            try
            {
                depth[i] = ushort.Parse(lineValues[i]);
            }
            catch (Exception ex)
            {
                Debug.Log((i - 1) + ": " + lineValues[i - 1]);
                Debug.Log(i + ": " + lineValues[i]);
                break; //throw new Exception();
            }
            // depth[i] = Convert.ToUInt16(lineValues[i]);
        }
            return depth;
    }



}

	

