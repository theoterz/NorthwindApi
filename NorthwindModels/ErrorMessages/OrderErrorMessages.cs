namespace NorthwindModels.ErrorMessages
{
    public static class OrderErrorMessages
    {
        public static readonly string Success = "Success";
        public static readonly string NotFound = "Order not found";
        public static readonly string OrderCreated = "The Order has been created!";
        public static readonly string OrderUpdated = "The Order has been updated!";
        public static readonly string OrderDeleted = "The Order is successfuly deleted!";
        public static readonly string DeletionError = "An error occured during the deletion!";
        public static readonly string InvalidEmployeeId = "The Customer Id should be greater than zero!";
        public static readonly string EntitiesNotFound = "The customer, the employee or the shipper doesn't exist";
        public static readonly string ModelNotValid = "The Order model is not valid";
        public static readonly string OrderOrEntitesNotFound = "The Order, the Customer, the Employee or the Shipper doesn't exist";
        public static readonly string ValidationError = "Validation Error";
    }
}
