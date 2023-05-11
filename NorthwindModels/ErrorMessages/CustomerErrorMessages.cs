namespace NorthwindModels.ErrorMessages
{
    public static class CustomerErrorMessages
    {
        public static readonly string Success = "Success";
        public static readonly string BadIdLength = "The id lenght must be exactly 5 characters";
        public static readonly string CustomerExists = "This Customer already exists!";
        public static readonly string ValidationError = "Validation Error";
        public static readonly string CustomerCreated = "The Customer has been created!";
        public static readonly string CustomerUpdated = "The Customer has been updated!";
        public static readonly string CustomerDeleted = "The Customer is successfuly deleted!";
        public static readonly string CustomerNotDeleted = "An error occured during the deletion!";
        public static readonly string NotFound = "Customer not found";
        public static readonly string ModelNotValid = "The Customer model is not valid";
    }
}
