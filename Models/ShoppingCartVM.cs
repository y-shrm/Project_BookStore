namespace Project_BookStore.Models
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

        public OrderHeader OrderHeader { get; set; }
        //public double? OrderTotal { get; set; }\
        public int TotalItemCount { get; set; }

    }
}
