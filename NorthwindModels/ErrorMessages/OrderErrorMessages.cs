namespace NorthwindModels.ErrorMessages
{
    public static class OrderErrorMessages
    {
        //Operation Error Messages
        public const string Success = "Success";
        public const string NotFound = "Order not found";
        public const string OrderCreated = "The Order has been created!";
        public const string OrderUpdated = "The Order has been updated!";
        public const string OrderDeleted = "The Order is successfuly deleted!";
        public const string DeletionError = "An error occured during the deletion!";       
        public const string EntitiesNotFound = "The customer, the employee or the shipper doesn't exist";
        public const string OrderOrEntitesNotFound = "The Order, the Customer, the Employee or the Shipper doesn't exist";

        //Validation Error Messages
        public const string ValidationError = "Validation Error";
        public const string InvalidEmployeeId = "The Employee Id should be greater than 0!";
        public const string InvalidShipperId = "The Shipper Id must be greater than 0!";
        public const string ModelNotValid = "The Order model is not valid";
    }
}
