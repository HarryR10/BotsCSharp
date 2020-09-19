using System;
namespace FinanceBot.Models.EntityModels
{
    public class Category
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public bool IsBasic { get; set; }
        public UserAccount Author { get; set; }
        public bool IsMounthly { get; set; }
    }
}
