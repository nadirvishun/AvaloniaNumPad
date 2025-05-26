using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using AvaloniaNumPad.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ursa.Controls;

namespace AvaloniaNumPad.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    public WindowToastManager? ToastManager { get; set; }

    //输入值
    [ObservableProperty] private string _inputValue = string.Empty;


    /// <summary>
    /// 确认
    /// </summary>
    [RelayCommand]
    public void EnterPressed()
    {
        ToastManager?.Show("输入的数字为：" + InputValue, NotificationType.Success);
    }

    /// <summary>
    /// 弹窗
    /// </summary>
    [RelayCommand]
    public async Task Dialog()
    {
        //有多个则展示弹窗
        var options = new OverlayDialogOptions()
        {
            Title = "数字键盘",
            Mode = DialogMode.None,
            Buttons = DialogButton.None,
            IsCloseButtonVisible = true,
            CanDragMove = true,
            CanResize = false,
            StyleClass = null,
        };
        //获取对话框实例，正常应该从依赖注入中获取
        var numPadDialogViewModel = new NumPadDialogViewModel();
        numPadDialogViewModel.Initialize("");
        await OverlayDialog.ShowModal<NumPadDialogView, NumPadDialogViewModel>(numPadDialogViewModel, options: options);
        var isEnter = numPadDialogViewModel.IsEnter; //是否确认关闭
        var inputValue = numPadDialogViewModel.InputValue;
        //如果存在车牌简写，则直接请求
        if (!string.IsNullOrWhiteSpace(inputValue) && isEnter)
        {
            ToastManager?.Show("输入的数字为：" + inputValue, NotificationType.Success);
        }
    }
}