using Linq01.Data;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using static Linq01.ListGenrator;

namespace Linq01
{
    internal class Program
    {
        class CaseInSenstive : IComparer<string>
        {
            public int Compare(string? x, string? y)
            {
                return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
            }
        }

        class custumEqualityCompare : IEqualityComparer<string>
        {
            public bool Equals(string? x, string? y)
            {
                return SortArray(x) == SortArray(y);
            }

            public int GetHashCode([DisallowNull] string obj)
            {
                return SortArray(obj).GetHashCode();
            }

            string SortArray(string Word)
            {
                char[] WordsChar = Word.ToCharArray();
                Array.Sort(WordsChar);
                return new string(WordsChar); 
            }
        }

        static void Main(string[] args)
        {
            #region List Genrator [Operation] #13
            #region Out of Stock
            // var Results = ProductList.Where(P => P.UnitsInStock == 0);
            //foreach (var Result in Results)
            //{
            //    Console.WriteLine(Result);
            //}
            // Quary Syntax
            //Results = from p in ProductList
            //          where p.UnitsInStock == 0
            //          select p;

            #endregion

            #region Product in Stock
            //var Results = ProductList.Where(p => p.UnitsInStock > 0 && p.Category == "Meat/Poultry");

            //Results = from p in ProductList
            //          where p.UnitsInStock > 0 && p.Category == "Meat/Poultry"
            //          select p;
            // var Results = ProductList.Where((p, i) => i < 20 && p.UnitsInStock == 0);

            #endregion
            #region Transformation [Projection] select , SelectMany
            //var Results = ProductList.Select(p => p.ProductName);

            //Results = from p in ProductList
            //          select p.ProductName;
            // var Results = CustomerList.SelectMany(c => c.Orders);

            // var Results = ProductList.Select(p => new { productID = p.ProductID, productName = p.ProductName });

            //var DescountedProducts = ProductList.Where(p => p.UnitsInStock > 0)
            //                                           .Select(p => new
            //                                           {
            //                                               Id = p.ProductID,
            //                                               Name = p.ProductName,
            //                                               oldPrice = p.UnitPrice,
            //                                               NewPrice = p.UnitPrice - (p.UnitPrice*0.1M)
            //                                           });
            //var Results = ProductList.Where(p => p.UnitsInStock > 0)
            //     .Select((p, i) => new
            //     {
            //         Index = i,
            //         ProductName = p.ProductName,
            //     });

            #endregion

            #region Operators - Immediate Ececution

            //var MinLength = ProductList.Min(p => p.ProductName.Length);

            //var Results = (from p in ProductList
            //              where p.ProductName.Length == MinLength
            //              select p).FirstOrDefault();
            //Console.WriteLine(Results);
            #endregion



            #endregion
            #region Linq01
            //List<string> result = new List<string>();
            //ProductList.ForEach(c => {
            //    string ListItem = $"{c.Category} => {ProductList.Count(p => p.Category == c.Category)}";
            //    if(!result.Contains(ListItem))
            //        result.Add(ListItem);
            //});
            //foreach(string item in result)
            //{
            //    Console.WriteLine(item);
            //}

            //var UnitOfStock = from P in ProductList
            //                  group P by P.Category into pc
            //                  select new
            //                  {
            //                      Category = pc.Key,
            //                      unitInStock = pc.Sum(x => x.UnitsInStock)
            //                  };
            //foreach (var item in UnitOfStock)
            //{
            //    Console.WriteLine(item);
            //}

            //var Result = from p in ProductList
            //             where p.UnitsInStock != 0
            //             group p by p.Category
            //             into category
            //             where category.Count() > 10
            //             select new 
            //             {
            //                 categoryName = category.Key,
            //                 countOfProduct = category.Count(),
            //             }; 
            #endregion

            #region Groping
            //var result = ProductList.GroupBy(P => P.Category)
            //       .Where(P => P.All(P => P.UnitsInStock != 0))
            //       .Select(P => new { Category = P.Key, product = P });


            //foreach (var item in result)
            //{
            //    Console.WriteLine(item.Category);
            //    foreach (var i in item.product)
            //    {
            //        Console.WriteLine(i);
            //    }

            //} 
            #endregion

            #region IEqualityComparer
            //string[] Arr = { "Form", "SaLt", "From", "last", "near", "earn" };

            //var result = Arr.GroupBy(A => A.Trim().ToLower(), new custumEqualityCompare());
            //foreach (var item in result)
            //{
            //    foreach (var subItem in item)
            //    {
            //        Console.WriteLine(subItem);
            //    }
            //    Console.WriteLine("........");
            //} 
            #endregion

            #region IComparer
            //string[] Arr = { "aPPLE", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry", "AbAcUs" };

            //var result = Arr.OrderBy(A => A, new CaseInSenstive());

            //foreach (var x in result)
            //    Console.WriteLine(x); 
            #endregion


            //var ProductInStock = from P in ProductList
            //                     .Where(P => P.UnitsInStock > 0 & P.UnitsInStock > 3)
            //                     select P;

            //foreach (var item in ProductInStock)
            //{
            //    Console.WriteLine(item);
            //}

            var result = from P in ProductList
                         where P.UnitsInStock != 0
                         group P by P.Category
                         into Category
                         where Category.Count() < 10
                         select Category;


            foreach (var Category in result)
            {
                Console.WriteLine(Category.Key);
                foreach(var item in Category)
                    Console.WriteLine($"                  {item.ProductName}");
            }


        }
    }
}