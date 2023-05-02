using Microsoft.AspNetCore.Components;
using Radzen;
using System.ComponentModel;
using System.Reflection;

/*
 * This component receives an Entity, it's type and an opration Callback event. It extracts the Entity's properties through the type and stores them in a IList<PropertyInfo> collection.
 * Moreover, the component maps the property values using a dictionary.
 */
namespace NorthwindComponentLibrary
{
    public partial class EntityForm
    {
        [Parameter]
        public OperationType OperationName { get; set; }
        [Parameter]
        public Object Entity { get; set; } = null!;
        [Parameter]
        public Type EntityType { get; set; } = null!;
        [Parameter]
        public EventCallback<Object> Operation { get; set; }
        [Inject]
        private DialogService DialogService { get; set; } = null!;

        private IList<PropertyInfo> Properties { get; set; } = null!;
        private Dictionary<string, string?> propertyValues = new();

        public enum OperationType
        {
            Create,
            Edit
        }

        protected override void OnInitialized()
        {
            Properties = EntityType.GetProperties();

            foreach (var property in Properties)
            {
                propertyValues.Add(property.Name, property.GetValue(Entity)?.ToString());
            }
        }

        /// <summary>
        /// This method is executed when the create button is clicked. For each property that belongs to the Entity, the program gets the value assigned to it
        /// from the dictionary, then it gets the property's type and it converts the value.
        /// </summary>
        /// <returns></returns>
        private async Task OnSaveClick()
        {
            foreach (var property in Properties)
            {
                var propertyValue = propertyValues[property.Name];

                if (propertyValue is not null)
                {
                    Type targetType = property.PropertyType;
                    //The TypeDescriptor.GetConverter method is used to get a type converter for the target type.
                    var convertedValue = TypeDescriptor.GetConverter(targetType).ConvertFromString(propertyValue);
                    property.SetValue(Entity, convertedValue);
                }
                else property.SetValue(Entity, null);
            }

            await Operation.InvokeAsync(Entity);
        }

        private void OnCancelClick()
        {
            DialogService.Close();
        }
    }
}
