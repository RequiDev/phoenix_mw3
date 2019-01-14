using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Phoenix.ThreadingSystem
{
    internal static class ThreadManager
    {
        private static readonly Dictionary<Thread, ThreadFunction> Functions = new Dictionary<Thread, ThreadFunction>();

        public static void Add(ThreadFunction funcThread)
        {
			var thread = new Thread(new ThreadStart(funcThread.Function))
			{
				Name = funcThread.Name,
				//IsBackground = true
			};
            if (!Functions.ContainsValue(funcThread))
                Functions.Add(thread, funcThread);
        }

        public static void RunAll()
        {
            foreach (var func in Functions)
            {
                Run(func.Value.Name);
            }
        }

        public static void Run(string name)
        {
            var foundFunc = Functions.FirstOrDefault(ft => ft.Value.Name == name);
            if (foundFunc.Key == null) return;
            if (foundFunc.Key.IsAlive) return;
            foundFunc.Key.Start();
        }
    }
}
