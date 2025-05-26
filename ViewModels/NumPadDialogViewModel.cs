using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;

namespace AvaloniaNumPad.ViewModels;

public partial class NumPadDialogViewModel : ViewModelBase, IDialogContext
{
    //输入值
    [ObservableProperty] private string _inputValue = string.Empty;

    //是否按回车确认
    public bool IsEnter;

    /// <summary>
    /// 初始化时赋值
    /// </summary>
    /// <param name="inputValue"></param>
    public void Initialize(string inputValue)
    {
        IsEnter = false;
        InputValue = inputValue;
    }

    /// <summary>
    /// 确认
    /// </summary>
    [RelayCommand]
    public void EnterPressed()
    {
        IsEnter = true;
        Close();
    }

    /// <summary>
    /// 弹窗关闭
    /// </summary>
    public void Close()
    {
        RequestClose?.Invoke(this, null);
    }

    public event EventHandler<object?>? RequestClose;
}