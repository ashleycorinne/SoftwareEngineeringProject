using UnityEngine;
using System.Collections;
using System;

namespace Counter 
{
	[Serializable]
	public class Count 
	{
		public int minimum;
		public int maximum;

		public Count(int min, int max) 
		{
			minimum = min;
			maximum = max;
		}
	}

}
