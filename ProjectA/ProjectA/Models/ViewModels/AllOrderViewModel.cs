using Mysqlx.Crud;

namespace ProjectA.Models.ViewModels
{
    public class AllOrderViewModel
    {
        public ClientModel Client { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}
