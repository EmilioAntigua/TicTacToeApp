using BlazorBootstrap;

namespace TicTacToeApp.Extensors;

public static class ToastServiceExtensions
{
  
    public static ToastMessage ShowToast(this ToastService toastService, ToastType toastType, string title, string? customMessage = null)
    {
        var message = new ToastMessage()
        {
            Type = toastType,
            Title = title,
            Message = customMessage ?? $"A las {DateTime.Now:hh:mm tt}"
        };
        toastService.Notify(message);
        return message;
    }
    
    public static ToastMessage ShowSuccess(this ToastService toastService, string? customMessage = null, string title = "�xito")
    {
        return toastService.ShowToast(ToastType.Success, title, customMessage);
    }

    public static ToastMessage ShowWarning(this ToastService toastService, string? customMessage = null, string title = "Advertencia")
    {
        return toastService.ShowToast(ToastType.Warning, title, customMessage);
    }

    public static ToastMessage ShowError(this ToastService toastService, string? customMessage = null, string title = "Error")
    {
        return toastService.ShowToast(ToastType.Danger, title, customMessage);
    }
}
