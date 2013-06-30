using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleStorageDebug
{
	public class Singleton
	{
		private static readonly object lockObj = new object();
		private static volatile Singleton mySingleTonInstance = new Singleton();

		private Singleton()
		{
			//private constructor
		}

		public static Singleton Instace
		{
			get
			{
				if (mySingleTonInstance == null)
				{
					lock (lockObj)
					{
						if (mySingleTonInstance == null)
						{
							mySingleTonInstance = new Singleton();
						}
					}
				}

				return mySingleTonInstance;
			}
		}
	}
}