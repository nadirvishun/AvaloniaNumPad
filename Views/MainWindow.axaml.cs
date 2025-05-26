using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaNumPad.ViewModels;
using Ursa.Controls;

namespace AvaloniaNumPad.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        //初始化加载
        Loaded += OnWindowLoaded;
    }

    /// <summary>
    /// 加载时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnWindowLoaded(object? sender, RoutedEventArgs e)
    {
        //正常用依赖注入获取同一实例
        _viewModel = DataContext as MainWindowViewModel;

        var topLevel = GetTopLevel(this);
        _viewModel.ToastManager = new WindowToastManager(topLevel) { MaxItems = 3 };
    }

    /// <summary>
    /// 关闭时
    /// </summary>
    /// <param name="e"></param>
    protected override void OnClosed(EventArgs e)
    {
        _viewModel.ToastManager?.Uninstall();
        base.OnClosed(e);
    }
}