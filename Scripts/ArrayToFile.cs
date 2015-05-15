using UnityEngine;
using System.Collections;
using System.IO;

public class ArrayToFile : MonoBehaviour {

	// Use this for initialization




	public void Test()
	{
		ushort[] depth = {1, 300, 60000};
		string path = "C:\\Users\\Muldy\\Desktop\\farts.txt";


		using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
		{
			using (BinaryWriter bw = new BinaryWriter(fs))
			{
				foreach (ushort value in depth)
				{
					bw.Write(value);
				}
			}
		}
	

	}



}

	

