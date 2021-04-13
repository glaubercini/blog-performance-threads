using Blog.Performance.Threads.EF.Context;
using Blog.Performance.Threads.EF.Models;
using Blog.Performance.Threads.SimpleThreading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Blog.Performance.Threads
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch stopwatch;
            long ticks1 = 0, ticks2 = 0;

            var list = new List<ITaskfy>();

            using var ctx = new PurchasingContext();

            var vendors = ctx.Vendor
                            .Select(v => new Vendor()
                                    {
                                        BusinessEntityId = v.BusinessEntityId
                                    }).ToList();

            stopwatch = Stopwatch.StartNew();


            //Single
            foreach (var item in vendors)
            {
                new SummedSales
                {
                    VendorId = item.BusinessEntityId
                }.Execute();
            }

            stopwatch.Stop();
            ticks1 = stopwatch.ElapsedTicks;


            //Threads
            foreach (var item in vendors)
            {
                list.Add(new SummedSales
                {
                    VendorId = item.BusinessEntityId
                });
            }

            stopwatch = Stopwatch.StartNew();

            var te = new TaskfyExecutor()
            {
                SplitSize = 10,
                TaskList = list
            };

            te.Execute();


            stopwatch.Stop();
            ticks2 = stopwatch.ElapsedTicks;

            Console.WriteLine($"Single: {ticks1}.");
            Console.WriteLine($"Threads: {ticks2}.");
        }
    }
}
