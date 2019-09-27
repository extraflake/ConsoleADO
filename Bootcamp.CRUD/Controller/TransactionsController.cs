using Bootcamp.CRUD.Context;
using Bootcamp.CRUD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.CRUD.Controller
{
    public class TransactionsController
    {
        MyContext _context = new MyContext();
        public int? count;
        public void ManageTransaction()
        {
            Item item = new Item();
            Transaction transaction = new Transaction();
            TransactionsItem transactionsItem = new TransactionsItem();

            // insert master transaction
            transaction.TransactionDate = DateTimeOffset.Now.LocalDateTime;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            // get id transaction last 
            var getTransaction = _context.Transactions.Max(x => x.Id);
            var getTransactionDetail = _context.Transactions.Find(getTransaction);

            Console.WriteLine("Transaction Id : " + getTransaction);
            Console.WriteLine("Transaction Date : " + getTransactionDetail.TransactionDate);

            Console.Write("How many items : ");
            count = Convert.ToInt16(Console.ReadLine());
            if (count == null)
            {
                Console.WriteLine("Please insert count of item that you want to buy");
                Console.Read();
            }
            else
            {
                for (int i = 1; i <= count; i++)
                {
                    Console.Write("Insert id item : ");
                    int? id = Convert.ToInt16(Console.ReadLine());
                    if (id == null)
                    {
                        Console.WriteLine("Please insert id that you want to buy");
                        Console.Read();
                    }
                    else
                    {
                        // Pencarian Id Item
                        var getItem = _context.Items.Find(id);
                        if(getItem == null)
                        {
                            Console.WriteLine("We don't have item with id : " + id);
                            Console.Read();
                        }
                        else
                        {
                            Console.Write("Insert amount of item : ");
                            int? quantity = Convert.ToInt16(Console.ReadLine());
                            if(getItem.Stock < quantity)
                            {
                                Console.Write("Stock is not enough, we have " + getItem.Stock);
                                Console.Read();
                            }
                            else
                            {
                                transactionsItem.Transaction = getTransactionDetail;
                                transactionsItem.Items = getItem;
                                transactionsItem.Quantity = quantity;
                                _context.TransactionsItem.Add(transactionsItem);
                                _context.SaveChanges();
                            }
                        }
                    }
                }

                var getPrice = _context.TransactionsItem.Where(x => x.Transaction.Id == getTransactionDetail.Id).ToList();
                int? total = 0;
                foreach (var proceed in getPrice)
                {
                    total += (proceed.Quantity * proceed.Items.Price);
                }
                Console.WriteLine("Total Price : " + total);
                Console.Write("Balance     : ");
                int? balance = Convert.ToInt32(Console.ReadLine());
                Console.Write("Exchange    : " + (balance - total));
            }
        }

        public void Print()
        {
            var getTransactionId = _context.TransactionsItem.Max(x => x.Transaction.Id);
            var getTransactionDetail = _context.TransactionsItem.Where(x => x.Transaction.Id == getTransactionId).ToList();
            Console.WriteLine("=============================================");
            Console.WriteLine("       " + getTransactionId + "       ");
            //Console.WriteLine(tampilin.Transaction.TransactionDate);
            Console.WriteLine("=============================================");
            Console.WriteLine(" Name         Quantity     Price        Total");
            Console.WriteLine("=============================================");
            foreach (var tampilin in getTransactionDetail)
            {
                Console.WriteLine(tampilin.Items.Name + " " + tampilin.Quantity + "" + tampilin.Items.Price + "" + tampilin.Quantity * tampilin.Items.Price);
            }
            Console.WriteLine("TOTAL                                  " + )
        }
    }
}
