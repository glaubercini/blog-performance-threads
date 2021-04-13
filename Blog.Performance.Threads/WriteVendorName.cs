using Blog.Performance.Threads.SimpleThreading;
using System;

namespace Blog.Performance.Threads
{
    sealed class WriteVendorName : ITaskfy
    {
        public string VendorName { get; set; }

        public void Execute()
        {
            foreach (var item in VendorName)
            {
                Console.WriteLine(item);
            }
        }
    }
}
