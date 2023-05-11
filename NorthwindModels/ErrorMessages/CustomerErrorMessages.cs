namespace NorthwindModels.ErrorMessages
{
    public static class CustomerErrorMessages
    {
        //Operation Error Messages
        public const string Success = "Success";
        public const string CustomerCreated = "The Customer has been created!";
        public const string CustomerUpdated = "The Customer has been updated!";
        public const string CustomerDeleted = "The Customer is successfuly deleted!";
        public const string CustomerNotDeleted = "An error occured during the deletion!";
        public const string CustomerExists = "This Customer already exists!";
        public const string NotFound = "Customer not found";
        
        //Validation Error Messages
        public const string BadIdLength = "The Id lenght must be exactly 5 characters";
        public const string IdRequired = "The Customer ID field is required.";
        public const string CompanyNameRequired = "The Company Name field is required.";
        public const string ValidationError = "Validation Error";
        public const string ModelNotValid = "The Customer model is not valid";
    }
}
