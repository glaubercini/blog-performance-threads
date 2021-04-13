using Blog.Performance.Threads.EF.Context;
using Blog.Performance.Threads.SimpleThreading;
using System;
using System.Linq;

namespace Blog.Performance.Threads
{
    sealed class SummedSales : ITaskfy
    {
        public int VendorId { get; set; }

        public void Execute()
        {
            using var ctx = new PurchasingContext();

            decimal total = ctx.PurchaseOrderHeader.Where(poh => poh.VendorId == VendorId).Sum(poh => poh.SubTotal);

            decimal qty = (from details in ctx.PurchaseOrderDetail
                           join header in ctx.PurchaseOrderHeader
                               on details.PurchaseOrderId equals header.PurchaseOrderId
                           where header.VendorId == VendorId
                           select details.PurchaseOrderDetailsId).Count();

            Console.WriteLine($"VendorId: {VendorId}, Total: {total}, Items {qty}.");

            //System.Threading.Thread.Sleep(50);
        }
    }
}
