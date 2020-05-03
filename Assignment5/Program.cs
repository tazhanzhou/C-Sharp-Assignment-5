using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assignment5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Write a program in C# Sharp to display the number and frequency of number from giving array.
            //Expected Output:
            // -The number and the Frequency are:
            // -Number 5 appears 3 times
            // - Number 9 appears 2 times
            // - Number 1 appears 1 times
            int[] numberArray = { 1, 2, 1, 3, 1, 2, 2, 1, 28, 5, 4, 2, 1, 5, 3, 4, 28 };
            Dictionary<int, NumberContainer> resultDic = new Dictionary<int, NumberContainer>();
            foreach (int number in numberArray)
            {
                if (resultDic.ContainsKey(number))
                {
                    resultDic[number].times += 1;
                }
                else
                {
                    resultDic.Add(number, new NumberContainer(number));
                }
            }
            foreach (NumberContainer number in resultDic.Values)
            {
                Console.WriteLine("Without Linq method: Array item {0} appears {1} times", number.number, number.times);
            }
            // Linq method
            var resultlist = from n in numberArray
                             group n by n into numberAndTimes
                             orderby numberAndTimes.Key
                             select new
                             {
                                 number = numberAndTimes.Key,
                                 times = numberAndTimes.Count()
                             };
            foreach (var v in resultlist)
            {
                Console.WriteLine("Linq Method: Array item {0} appears {1} times", v.number, v.times);
            }
            //Write a program with LINQ Sharp to display the top nth records. 
            //Test Data : 
            //The members of the list are : 5 7 13 24 6 9 8 7
            //How many records you want to display ? : 3
            //Expected Output : 
            //The top 3 records from the list are: 24 13 9
            Console.WriteLine("How many records you want to display ? :  ");
            int howMany = Int32.Parse(Console.ReadLine());
            var orderList = from n in numberArray
                          group n by n into sigleNumber
                          orderby sigleNumber.Key descending
                          select new
                          {
                              number = sigleNumber.Key
                          };
            ArrayList topList = new ArrayList();
            for (int i = 0; i < howMany; i++)
            {
                topList.Add(orderList.ElementAt(i).number);
            }
            Console.Write("The top {0} records from the list are: ",howMany);
            foreach(int i in topList)
            {
                Console.Write(i+" ");
            }
            Console.WriteLine("");
            //Write a program with LINQ to generate an Inner Join between two data sets. 
            IList<Item_mast> itemList = new List<Item_mast>();
            itemList.Add(new Item_mast(1, "Dell"));
            itemList.Add(new Item_mast(2, "HP"));
            itemList.Add(new Item_mast(3, "Lenovo"));
            itemList.Add(new Item_mast(4, "Mac"));
            itemList.Add(new Item_mast(5, "Surface"));

            IList<Purchase> purchaseList = new List<Purchase>();
            purchaseList.Add(new Purchase(1, 1, 1));
            purchaseList.Add(new Purchase(1, 4, 2));
            purchaseList.Add(new Purchase(2, 2, 1));
            purchaseList.Add(new Purchase(2, 3, 1));
            purchaseList.Add(new Purchase(2, 5, 1));

            var buyWhat = from i in itemList
                          join o in purchaseList on i.ItemId equals o.ItemId
                          select new
                          {
                              invoiceNumber = o.InvNo,
                              itemDes = i.ItemDes,
                              Qty = o.PurQty
                          };
            foreach(var v in buyWhat)
            {
                Console.WriteLine("order {0} has {1} {2}",v.invoiceNumber,v.Qty,v.itemDes);
            }

        }
    }
}
class NumberContainer
{
    public int number { get; set; }
    public int times { get; set; }

    public NumberContainer(int number)
    {
        this.number = number;
        this.times = 1;
    }
}

public class Item_mast
{
    public int ItemId { get; set; }
    public string ItemDes { get; set; }

    public Item_mast(int itemId, string itemDes)
    {
        ItemId = itemId;
        ItemDes = itemDes;
    }
}

public class Purchase
{
    public int InvNo { get; set; }
    public int ItemId { get; set; }
    public int PurQty { get; set; }

    public Purchase(int invNo, int itemId, int purQty)
    {
        InvNo = invNo;
        ItemId = itemId;
        PurQty = purQty;
    }
}

