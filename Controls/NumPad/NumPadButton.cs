using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace AvaloniaNumPad.Controls;

public class NumPadButton : Button
{
    // 定义新的 StyledProperty（支持样式、绑定、动画等）
    public static readonly StyledProperty<Key> KeyProperty = AvaloniaProperty.Register<NumPadButton, Key>(nameof(Key));

    // 属性包装器
    public Key Key
    {
        get => GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }
}