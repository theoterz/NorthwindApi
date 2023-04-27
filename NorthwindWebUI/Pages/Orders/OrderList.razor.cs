using Microsoft.AspNetCore.Components;
using NorthwindModels.DTOs;
using NorthwindWebUI.Services;

namespace NorthwindWebUI.Pages.Orders
{
    public partial class OrderList
    {
        [Inject]
        private IOrderService _orderService { get; set; } = null!;

        private List<OrderDTO>? orders { get; set; }

        private string _searchOption = "Search By Id";
        private bool _showGetAllOrdersBtn = false;

        //Search by Id
        private string _orderId = string.Empty;
        private string? _searchByIdErrorMessage;
        private bool _showSearchByIdLabel = false;

        //Search by Customer Id
        private string _customerIdA = string.Empty;
        private bool _showSearchByCustomerLabel = false;
        private string _searchByCustomerErrorMessage = string.Empty;

        //Search By Customer And Employee
        private string _customerIdB = string.Empty;
        private string _employeeId = string.Empty;
        private string _searchByCustomerAndEmployeeErrorMessage = string.Empty;
        private bool  _showSearchByCustomerAndEmployeeLabel = false;

        protected override async Task OnInitializedAsync()
        {
            IEnumerable<OrderDTO>? fetchedOrders = await _orderService.GetAll();
            if (fetchedOrders is not null) orders = fetchedOrders.ToList();
            else orders = new List<OrderDTO>();
        }

        private async Task SearchById()
        {   
            _searchByIdErrorMessage = string.Empty;
            _showSearchByIdLabel = false;

            if (_orderId is not null)
            {
                if (Int32.TryParse(_orderId, out int id) && id > 0)
                {

                    orders!.Clear();
                    OrderDTO? fetchedOrder = await _orderService.GetById(id);

                    if (fetchedOrder is not null) orders.Add(fetchedOrder);
                    else
                    {
                        _showSearchByIdLabel = true;
                        _searchByIdErrorMessage = "No Order matches this id";
                    }

                    _showGetAllOrdersBtn = true;
                }
                else
                {
                    _showSearchByIdLabel = true;
                    _searchByIdErrorMessage = "The Order id should be a positive integer";
                }
            }

            _orderId = string.Empty;
            
        }

        private async Task SearchByCustomerId()
        {
            _searchByCustomerErrorMessage = string.Empty;
            _showSearchByCustomerLabel = false;
            
            if (_customerIdA is not null)
            {
                orders!.Clear();
                IEnumerable<OrderDTO>? fetchedOrders = await _orderService.GetByCustomerId(_customerIdA);
                if (fetchedOrders is not null) orders.AddRange(fetchedOrders);
                else
                {
                    _searchByCustomerErrorMessage = "No Order matches this Customer Id";
                    _showSearchByCustomerLabel = true;
                }

                _showGetAllOrdersBtn = true;
            }
            else
            {
                _searchByCustomerErrorMessage = "The Customer id should not be null";
                _showSearchByCustomerLabel = true;
            }
           
            _customerIdA = string.Empty;
        }

        private async Task SearchByCustomerAndEmployee()
        {
            _searchByCustomerAndEmployeeErrorMessage = string.Empty;
            _showSearchByCustomerAndEmployeeLabel = false;

            if (!_customerIdB.Equals(string.Empty) && !_employeeId.Equals(string.Empty))
            {
                if(Int32.TryParse(_employeeId, out int id) && id > 0)
                {
                    orders!.Clear();

                    IEnumerable<OrderDTO>? fetchedOrders = await _orderService.GetByCustomerAndEmployee(_customerIdB, id);
                    if (fetchedOrders is not null) orders.AddRange(fetchedOrders);
                    else
                    {
                        _searchByCustomerAndEmployeeErrorMessage = "No Orders meet the criteria";
                        _showSearchByCustomerAndEmployeeLabel = true;
                    }

                    _showGetAllOrdersBtn = true;
                }
                else
                {
                    _searchByCustomerAndEmployeeErrorMessage = "The Employee id should be a positive integer";
                    _showSearchByCustomerAndEmployeeLabel= true;
                }
            }
            else
            {
                _searchByCustomerAndEmployeeErrorMessage = "The fields shouldn't be empty";
                _showSearchByCustomerAndEmployeeLabel = true;
            }
        }

        private async Task GetAllOrders()
        {
            _showGetAllOrdersBtn = false;
            _showSearchByIdLabel = false;
            _showSearchByCustomerLabel = false;
            _showSearchByCustomerAndEmployeeLabel = false;

            orders!.Clear();

            IEnumerable<OrderDTO>? fetchedOrders = await _orderService.GetAll();
            if (fetchedOrders is not null) orders.AddRange(fetchedOrders);

        }
        private async Task DeleteOrder(object id)
        {
            bool result = await _orderService.Delete(Convert.ToInt32(id));
            if (result) await GetAllOrders();
        }
    }
}
