using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


/*
 * This component receives an Entity, it's type and a delete Callback event. It extracts the Entity's properties through the type and stores them in a IList<PropertyInfo> collection. 
 */
namespace NorthwindComponentLibrary
{
    public partial class DisplayEntity
    {
        [Parameter]
        public Object Entity { get; set; } = null!;
        [Parameter]
        public Type EntityType { get; set; } = null!;
        [Parameter]
        public EventCallback<object> DeleteEntity { get; set; }

        private bool _showModal = false;
        private IList<PropertyInfo> Properties = null!;

        protected override void OnInitialized()
        {
            Properties = EntityType.GetProperties();
        }

        /// <summary>
        /// Finds which property is the primary key and invokes the delete method
        /// </summary>
        /// <returns></returns>
        private async Task OnDelete(bool accepted)
        {
            if (accepted)
            {
                _showModal = false;
                PropertyInfo id = Properties.FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)))!;
                await DeleteEntity.InvokeAsync(id.GetValue(Entity));
            }
            else _showModal = false;
        }
    }
}
