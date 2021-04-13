using System;
using System.Collections.Generic;
using System.Threading;

namespace Blog.Performance.Threads.SimpleThreading
{
    sealed class TaskfyExecutor
    {
        public int SplitSize { get; set; } = 1;

        public List<ITaskfy> TaskList { get; set; } = new List<ITaskfy>();

        public void Execute()
        {
            int tasks = (int)Math.Ceiling(TaskList.Count / (double)SplitSize);

            using var e = new CountdownEvent(tasks);

            for (int i = 0; i < TaskList.Count; i += SplitSize)
            {
                var c = (i + SplitSize) > TaskList.Count ?
                            (TaskList.Count - i)
                                : SplitSize;

                var subList = TaskList.GetRange(i, c);

                var portion = new TaskfyExecutorPortion
                {
                    SubList = subList
                };

                ThreadPool.QueueUserWorkItem(delegate
                {
                    portion.Execute();
                    e.Signal();
                });
            }

            e.Wait();
        }
    }
}
