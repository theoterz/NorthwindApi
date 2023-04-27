using Microsoft.AspNetCore.Components;

namespace NorthwindComponentLibrary
{
    public partial class ConfirmationComponent
    {
        [Parameter]
        public string Text { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<bool> OnClose { get; set; }

        [Parameter]
        public ModalDialogType DialogType { get; set; }

        private Task ModalCancel()
        {
            return OnClose.InvokeAsync(false);
        }

        private Task ModalAccept()
        {
            return OnClose.InvokeAsync(true);
        }

        public enum ModalDialogType
        {
            Delete
        }
    }
}
