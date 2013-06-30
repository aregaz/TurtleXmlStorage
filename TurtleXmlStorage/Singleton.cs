namespace TurtleXmlStorage
{
	public class Singleton
	{
		private static readonly object lockObj = new object();
		private static volatile Singleton mySingleTonInstance = new Singleton();

		protected Singleton()
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