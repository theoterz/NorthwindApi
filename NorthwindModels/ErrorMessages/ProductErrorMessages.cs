namespace NorthwindModels.ErrorMessages
{
    public static class ProductErrorMessages
    {
        //Operation Error Messages
        public const string Success = "Success";
        public const string ProductCreated = "The Product has been created!";
        public const string ProductUpdated = "The Product has been updated!";
        public const string ProductDeleted = "The Product is successfuly deleted!";
        public const string DeletionError = "An error occured during the deletion!";
        public const string NotFound = "Product not found";
        public const string EntitiesNotFound = "The supplier or the category doesn't exist";
        public const string ProductOrEntitiesNotFound = "The product, the supplier or the category doesn't exist";

        //Validation Error Messages
        public const string ValidationError = "Validation Error";
        public const string ModelNotValid = "The Product model is not valid";
        public const string ProductNameRequired = "The Product Name field is required.";
        public const string SupplierIdRange = "The Supplier Id should be greater than 0!";
        public const string CategoryIdRange = "The Category Id should be greater than 0!";
        public const string UnitPriceRange = "The Unit Price should be greater than 0!";
    }
}
