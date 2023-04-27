﻿using NorthwindModels.DTOs;

namespace NorthwindWebUI.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>?> GetAll();
        public Task<OrderDTO?> GetById(int id);
        public Task<IEnumerable<OrderDTO>?> GetByCustomerId(string customerId);
        public Task<IEnumerable<OrderDTO>?> GetByCustomerAndEmployee(string customerId, int employeeId);
        public Task<string> Create(OrderCreateDTO orderDTO);
        public Task<bool> Delete(int id);
    }
}
