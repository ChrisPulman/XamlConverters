# XamlConverters
Converters for WPF

Converters available are:

-  ArithmeticConverter
-  BackgroundColorToBwForegroundConverter
-  BoolNegationConverter
-  BoolToOpacityConverter
-  BoolToStringTickCrossConverter
-  BoolToVisibilityConverter
-  BoolToVisibilityConverterNegate
-  CollectionSizeToBoolConverter
-  CollectionSizeToVisibilityConverter
-  ColorToBrushConverter
-  DoubleToCurrencyStringConverter
-  EnumConverter
-  HexStringToColorConverter
-  HexStringToSolidColorBrushConverter
-  IntToThicknessConverter
-  IntToVisibilityConverter
-  InverseValueToBoolConverter
-  InvertSignConverter
-  InvertValueConverter
-  InvertVisibilityConverter
-  IsGreaterThanOrEqualToConverter
-  LineStrokeLevelConverter
-  ToLowerConverter
-  MathConverter
-  MultiConverter
-  MultiplierConverter
-  NullToBoolConverter
-  NullToVisibilityConverter
-  ToUpperConverter
-  ValueCompareVisibilityConverter
-  ValueGreaterThanXToBoolConverter
-  ValueGtXConverter
-  ValueLessThanXToBoolConverter
-  ValueNotNullToBoolConverter
-  ValueNotNullToVisibilityConverter
-  ValueToBrushConverter
-  VisibilityFromNumberConverter
-  VisibilityFromNumberEqualsConverter
-  ZeroToVisibilityConverter

## Usage


Add the namespace to your App.xaml file:

```xaml
xmlns:converters="https://github.com/ChrisPulman/XamlConverters"
```

Then add the ConvertersDictionary to your resources in App.xaml:

```xaml
<Application.Resources>
    <converters:ConvertersDictionary />
</Application.Resources>
```

This will make all of the converters available to your application.
Then use it by selecting a `Converter={StaticResource #CONVERTER#}}` such as:

```xaml
<TextBlock Text="{Binding MyProperty, Converter={StaticResource ToUpperConverter}}" />
```
