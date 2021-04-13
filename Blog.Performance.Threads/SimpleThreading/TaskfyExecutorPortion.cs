using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Performance.Threads.SimpleThreading
{
    sealed class TaskfyExecutorPortion
    {
        public List<ITaskfy> SubList { get; set; }

        public void Execute()
        {
            foreach (var item in SubList)
            {
                item.Execute();
            }
        }
    }
}
