

# WPF.UI

> The following is for reference only. For more details, please refer to the link: http://xiamu.3vkj.vip/wpf.ui

The WPF. UI package is a toolkit used to build the Windows Presentation Foundation (WPF) user interface.
Implement property extension for WPF controls to support more custom properties and break through the limitations of the original properties of the control.
Packaging duplicate styles and templates into a universal resource dictionary greatly improves the convenience of templating development and enhances the reusability of code.

**In order to be compatible with other style packages, this package uses a style based approach, so users do not have to worry about conflicts with other resource packages.**

### Installation Tutorial

1. Right click on the project in the solution bar ->Manage NuGet Package
2. Find WPF in the NuGet package manager UI and install

### Instruction For Use

#### App.xaml

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/WPF.UI;component/WPFUI.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

#### XAML

**1. Add reference :**

```xaml
<!--The name can be customized-->
xmlns:wpfui="WPF.UI"
```

**2. Can be used in resource dictionaries, styles, and controls**：

> **Using in Styles**

```xml
<Style ··· >
	<Setter Property="wpfui:WPFUI.CornerRadius" Value="10 0 0 10"/>
    <Setter Property="wpfui:WPFUI.MouseOverBackground" Value="#FFC1C1C1"/>
    <Setter Property="wpfui:WPFUI.MouseOverBorderBrush" Value="#FFC1C1C1"/>
    <Setter Property="wpfui:WPFUI.CheckedBackground" Value="#FF848484"/>
    ···
</Style>
```

> **Using in Controls**

```xaml
<Button wpfui:WPFUI.Tag="WPF.UI" 
        wpfui:WPFUI.Background="AntiqueWhite" 
        wpfui:WPFUI.Text="{Binding ...}"
        ···/>
```

> **Using In Control Templates：**
>
> > When using control templates and trigger templates, the compiler lacks code intelligence prompt functionality. Developers need to manually write code, and caution is required here.

```xaml
<ControlTemplate TargetType="Button">
    <Border x:Name="border"
        Width="{TemplateBinding Width}"
        Height="{TemplateBinding Height}"
        Margin="{TemplateBinding Margin}"
        Background="{TemplateBinding Background}"
        BorderBrush="{TemplateBinding BorderBrush}"
        BorderThickness="{TemplateBinding wpfui:WPFUI.BorderThickness}"
        CornerRadius="10">
        <Grid>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="50" />
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="50" />
            </Grid.ColumnDefinitions>
            <Border Background="{TemplateBinding wpfui:WPFUI.Background}" CornerRadius="{TemplateBinding wpfui:WPFUI.CornerRadius}" />
            <Label
                HorizontalAlignment="Center"
                VerticalAlignment="Center"
                Content="{TemplateBinding wpfui:WPFUI.Tag}"
                FontFamily="Webdings"
                FontSize="{TemplateBinding wpfui:WPFUI.FontSize}" />
            <ContentControl Grid.Column="1"
                HorizontalAlignment="Center"
                VerticalAlignment="Center"
                Content="{TemplateBinding Content}" />
            <Border Grid.Column="2"
                Background="{TemplateBinding wpfui:WPFUI.Background}"
                CornerRadius="{TemplateBinding wpfui:WPFUI.MouseOverCornerRadius}" />
        </Grid>
    </Border>
    <ControlTemplate.Triggers>
        <Trigger Property="IsMouseOver" Value="True">
            <Setter TargetName="border" 
                 Property="BorderBrush" 
                 Value="{Binding Path=(wpfui:WPFUI.MouseOverBorderBrush), RelativeSource={RelativeSource TemplatedParent}}" />
            <Setter TargetName="border" 
                 Property="Background" 
                 Value="{Binding Path=(wpfui:WPFUI.MouseOverBackground), RelativeSource={RelativeSource TemplatedParent}}" />
        </Trigger>
        <Trigger Property="IsPressed" Value="True">
            <Setter TargetName="border" 
                 Property="BorderBrush" 
                 Value="{Binding Path=(wpfui:WPFUI.CheckedBorderBrush), RelativeSource={RelativeSource TemplatedParent}}" />
            <Setter TargetName="border"
                 Property="Background"
                 Value="{Binding Path=(wpfui:WPFUI.CheckedBackground), RelativeSource={RelativeSource TemplatedParent}}" />
        </Trigger>
    </ControlTemplate.Triggers>
</ControlTemplate>

```

**Note: The syntax for binding triggers may vary**

```xaml
<Trigger Property="IsMouse" Value="True">
    <Setter TargetName="border" 
            Property="BorderBrush" 
            Value="{Binding Path=(wpfui:WPFUI.MouseOverBorderBrush),RelativeSource={RelativeSource TemplatedParent}}"/>
    <Setter TargetName="border" 
            Property="Background" 
            Value="{Binding Path=(wpfui:WPFUI.MouseOverBackground),RelativeSource={RelativeSource TemplatedParent}}"/>
</Trigger>
```


---

> Complete Example ( For reference only )：

```xaml
<Style TargetType="Button">
    <Setter Property="Height" Value="75" />
    <Setter Property="Width" Value="200" />
    <Setter Property="Tag" Value="10" />
    <Setter Property="FontSize" Value="24" />
    <Setter Property="wpfui:WPFUI.FontSize" Value="35" />
    <Setter Property="wpfui:WPFUI.Tag" Value="=" />
    <Setter Property="wpfui:WPFUI.CornerRadius" Value="10 0 0 10" />
    <Setter Property="wpfui:WPFUI.MouseOverCornerRadius" Value="0 10 10 0" />
    <Setter Property="wpfui:WPFUI.BorderThickness" Value="1" />
    <Setter Property="wpfui:WPFUI.Background" Value="Gray" />
    <Setter Property="wpfui:WPFUI.MouseOverBackground" Value="#FFC1C1C1" />
    <Setter Property="wpfui:WPFUI.MouseOverBorderBrush" Value="#FFC1C1C1" />
    <Setter Property="wpfui:WPFUI.CheckedBackground" Value="#FF848484" />
    <Setter Property="wpfui:WPFUI.CheckedBorderBrush" Value="#FF848484" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Border x:Name="border"
                    Width="{TemplateBinding Width}"
                    Height="{TemplateBinding Height}"
                    Margin="{TemplateBinding Margin}"
                    Background="{TemplateBinding Background}"
                    BorderBrush="{TemplateBinding BorderBrush}"
                    BorderThickness="{TemplateBinding wpfui:WPFUI.BorderThickness}"
                    CornerRadius="10">
                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="50" />
                            <ColumnDefinition Width="*" />
                            <ColumnDefinition Width="50" />
                        </Grid.ColumnDefinitions>
                        <Border Background="{TemplateBinding wpfui:WPFUI.Background}" 
                                CornerRadius="{TemplateBinding wpfui:WPFUI.CornerRadius}" />
                        <Label
                            HorizontalAlignment="Center"
                            VerticalAlignment="Center"
                            Content="{TemplateBinding wpfui:WPFUI.Tag}"
                            FontFamily="Webdings"
                            FontSize="{TemplateBinding wpfui:WPFUI.FontSize}" />
                        <ContentControl Grid.Column="1"
                            HorizontalAlignment="Center"
                            VerticalAlignment="Center"
                            Content="{TemplateBinding Content}" />
                        <Border Grid.Column="2"
                            Background="{TemplateBinding wpfui:WPFUI.Background}"
                            CornerRadius="{TemplateBinding wpfui:WPFUI.MouseOverCornerRadius}" />
                    </Grid>
                </Border>
                <ControlTemplate.Triggers>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter TargetName="border" Property="BorderBrush" Value="{Binding Path=(wpfui:WPFUI.MouseOverBorderBrush), RelativeSource={RelativeSource TemplatedParent}}" />
                        <Setter TargetName="border" Property="Background" Value="{Binding Path=(wpfui:WPFUI.MouseOverBackground), RelativeSource={RelativeSource TemplatedParent}}" />
                    </Trigger>
                    <Trigger Property="IsPressed" Value="True">
                        <Setter TargetName="border" Property="BorderBrush" Value="{Binding Path=(wpfui:WPFUI.CheckedBorderBrush), RelativeSource={RelativeSource TemplatedParent}}" />
                        <Setter TargetName="border" Property="Background" Value="{Binding Path=(wpfui:WPFUI.CheckedBackground), RelativeSource={RelativeSource TemplatedParent}}" />
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
 
```
------

