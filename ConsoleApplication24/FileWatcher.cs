using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication24
{
    public class FileWatcher
    {
        public object eventLocker = new object();

        public event Action OnWatch;

        public object lockObject = new object();

        public string FilePath;

        private DateTime CurrentTime { get; set; } = DateTime.Now;

        public FileWatcher(string path)
        {
            FilePath = path;
        }

        public void Start()
        {
            Thread t = new Thread(Watch);
            t.Start();
        }

        public void Watch()
        {
            lock (lockObject)
            {
                while (true)
                {
                    var writeTime = File.GetLastWriteTime(FilePath);
                    if (writeTime > CurrentTime)
                    {
                        CurrentTime = writeTime;
                        OnWatching();
                    }
                }
            }
        }

        protected virtual void OnWatching()
        {
            Action handler;
            lock (lockObject)
            {
                handler = OnWatch;
            }
            if (handler != null)
            {
                handler();
            }
        }
    }
}
