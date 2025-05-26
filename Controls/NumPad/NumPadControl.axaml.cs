using System;
using System.Collections.Generic;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace AvaloniaNumPad.Controls;

public partial class NumPadControl : UserControl
{
    public NumPadControl()
    {
        InitializeComponent();
    }

    //定义输入值属性，用于展示已有的数据
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<NumPadControl, string>(nameof(Text), string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    //是否Enter键触发的路由事件
    public static readonly RoutedEvent<RoutedEventArgs> EnterPressedEvent =
        RoutedEvent.Register<NumPadControl, RoutedEventArgs>(nameof(EnterPressed), RoutingStrategies.Direct);

    public event EventHandler<RoutedEventArgs> EnterPressed
    {
        add => AddHandler(EnterPressedEvent, value);
        remove => RemoveHandler(EnterPressedEvent, value);
    }

    //mvvm方式的Enter键触发
    public static readonly StyledProperty<ICommand?> EnterCommandProperty =
        AvaloniaProperty.Register<NumPadControl, ICommand?>(nameof(EnterCommand));

    public ICommand? EnterCommand
    {
        get => GetValue(EnterCommandProperty);
        set => SetValue(EnterCommandProperty, value);
    }

    /// <summary>
    /// 字典
    /// </summary>
    private static readonly Dictionary<Key, string> KeyInputMapping = new()
    {
        [Key.NumPad0] = "0",
        [Key.NumPad1] = "1",
        [Key.NumPad2] = "2",
        [Key.NumPad3] = "3",
        [Key.NumPad4] = "4",
        [Key.NumPad5] = "5",
        [Key.NumPad6] = "6",
        [Key.NumPad7] = "7",
        [Key.NumPad8] = "8",
        [Key.NumPad9] = "9",
    };

    /// <summary>
    /// 按键
    /// </summary>
    /// <param name="key"></param>
    public void ProcessClick(Key key)
    {
        if (KeyInputMapping.TryGetValue(key, out var s))
        {
            Text += s;
        }
        else if (Key.Decimal == key)
        {
            //不能以点开头
            if (Text == string.Empty)
            {
                return;
            }

            //不能有多个点
            if (Text.Contains('.'))
            {
                return;
            }

            Text += '.';
        }
        else if (Key.Back == key)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                Text = Text[..^1];
            }
        }
        else if (Key.Enter == key)
        {
            //如果以.结尾，则去掉
            if (Text.EndsWith('.'))
            {
                Text = Text[..^1];
            }

            //执行command
            if (EnterCommand?.CanExecute(null) == true)
            {
                EnterCommand.Execute(null);
            }

            //触发事件
            var args = new RoutedEventArgs(EnterPressedEvent);
            RaiseEvent(args);
        }
    }
}