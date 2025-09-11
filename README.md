# XamlConverters
Converters for WPF (.NET Framework 4.6.2 / .NET 8 / .NET 9)

![Nuget](https://img.shields.io/nuget/v/CP.Xaml.Converters) ![Nuget](https://img.shields.io/nuget/dt/CP.Xaml.Converters)

A rich collection of ready-to-use value & multi-value converters for WPF plus some numeric markup extensions.

---
## Install
```
PM> Install-Package CP.Xaml.Converters
```

---
## Quick Start (All Converters at Once)
```xaml
<Application x:Class="App" ...
             xmlns:converters="https://github.com/ChrisPulman/XamlConverters">
    <Application.Resources>
        <converters:ConvertersDictionary />
    </Application.Resources>
</Application>
```
Use in bindings:
```xaml
<TextBlock Text="{Binding Title, Converter={StaticResource ToUpperConverter}}"/>
```

---
## Lightweight Usage (Singletons)
Some frequently used stateless converters are exposed through `ConvertersRegistry` so you can avoid resource keys:
```xaml
xmlns:c="clr-namespace:CP.Xaml.Converters"
<TextBlock Visibility="{Binding IsBusy, Converter={x:Static c:ConvertersRegistry.BoolToVisibility}}"/>
```
Available singletons (see ConvertersRegistry.cs): Not, BoolToVisibility, BoolToVisibilityAdv, NotNullToVisibility, NotNullToBool, NullToBool, NullCoalesce, InvertVisibility, BgToReadable, Percentage, Arithmetic, Math, Equality, Comparison, And, Or, Xor, MultiFormat.

---
## Markup Numeric Extensions
Allow inline numeric objects in XAML without x:Static:
```xaml
xmlns:c="clr-namespace:CP.Xaml.Converters"
<Setter Property="Tag" Value="{c:Int32 42}"/>
<Setter Property="Tag" Value="{c:Double 2.5}"/>
```
Available: Int16, Int32, Single (float), Double.

---
## Converter Catalogue
(Each listed as Resource Key = Class Name when using ConvertersDictionary)

### Arithmetic & Math
- ArithmeticConverter – Parameter: simple expression "<op><number>" e.g. "+5", "-2", "*3", "/10". Works with int/double.
  ```xaml
  <TextBlock Text="{Binding Count, Converter={StaticResource ArithmeticConverter}, ConverterParameter='*2'}"/>
  ```
- MathConverter – Parameter: arithmetic expression over one or many bound values. Single binding uses value as {0}/x; MultiBinding supports {0},{1}… or a,b,c,d.
  ```xaml
  <!-- Scale * Percentage example -->
  <TextBlock>
    <TextBlock.Text>
      <MultiBinding Converter="{StaticResource MathConverter}" ConverterParameter="({0} * {1}) / 100">
        <Binding Path="Base"/>
        <Binding Path="Percent"/>
      </MultiBinding>
    </TextBlock.Text>
  </TextBlock>
  ```
- PercentageConverter – Parameter: either "50%" or decimal factor "0.5" multiplies numeric value.
  ```xaml
  <RowDefinition Height="{Binding ActualWidth, ElementName=Root, Converter={StaticResource PercentageConverter}, ConverterParameter=25%}" />
  ```
- MultiplierConverter – Parameter: numeric factor; ConvertBack divides.
  ```xaml
  <Rectangle Width="{Binding BaseWidth, Converter={StaticResource MultiplierConverter}, ConverterParameter=1.5}"/>
  ```
- ValueGtXConverter – Rounds `double` to 1 decimal place if > parameter else 2.
  ```xaml
  <TextBlock Text="{Binding Speed, Converter={StaticResource ValueGtXConverter}, ConverterParameter=40}"/>
  ```
- ValueGreaterThanXToBoolConverter / ValueLessThanXToBoolConverter – Parameter: threshold (default 0). Returns bool. ConvertBack adjusts relative to threshold.
  ```xaml
  <CheckBox IsChecked="{Binding ItemsCount, Converter={StaticResource ValueGreaterThanXToBoolConverter}, ConverterParameter=5}" Content=">5?"/>
  ```
- IsGreaterThanOrEqualToConverter – (a) Single binding + parameter (b) MultiBinding with two inputs.
  ```xaml
  <CheckBox IsChecked="{Binding Value, Converter={StaticResource IsGreaterThanOrEqualToConverter}, ConverterParameter=100}"/>
  ```

### Boolean Logic / Aggregation
- BoolNegationConverter – Inverts bool.
- MultiBooleanAndConverter – MultiBinding; optional parameter "invert".
- MultiBooleanOrConverter – MultiBinding; optional parameter "invert".
- BooleanXorConverter – MultiBinding exclusive OR (true if odd number true).
- InverseValueToBoolConverter – True if numeric <= 0.
- NullToBoolConverter – Use `NullToBoolConverter.IsNull` or `.NotNull` static singletons; or set `ReturnTrueIfNull` property in XAML.
- ValueNotNullToBoolConverter – Parameter: "reverse" or "true" to invert.
- ObjectEqualsParameterConverter – Parameter: string to compare; prefix ! to invert.
- EqualityConverter – Parameter: value to compare; prefix ! to invert.
- ComparisonConverter – Parameter: comparison expression e.g. ">5", "<= 10", "!=X", "!>=3" (leading ! inverts result). Works with numeric or string.
- EnumToBooleanConverter – Parameter: enum member name; useful for RadioButtons.
  ```xaml
  <RadioButton Content="Large" IsChecked="{Binding Size, Converter={StaticResource EnumToBooleanConverter}, ConverterParameter=Large}"/>
  ```

### Visibility
- BoolToVisibilityConverter – Parameter: "reverse" or "true" to invert.
- BoolToVisibilityAdvancedConverter – Parameter tokens: ! (invert), H (Hidden instead of Collapsed). Examples: "!H", "H".
- BoolToVisibilityConverterNegate – Opposite of normal: true => Collapsed/Hidden, false => Visible. Parameter: "hidden" to use Hidden.
- InvertVisibilityConverter – Toggles Visible <-> Collapsed (parameter containing "hidden" toggles Visible <-> Hidden).
- IntToVisibilityConverter – Shows when int > 0. Parameter: "hidden" to hide instead of collapse.
- ValueNotNullToVisibilityConverter – Parameter: "reverse" or "true" to invert.
- NullToVisibilityConverter – Shows when null; parameter "inverse" reverses.
- StringNullOrEmptyToVisibilityConverter – Collapses when null/empty. Parameter: "invert", "hidden", or "hiddeninvert".
- CollectionSizeToVisibilityConverter – Parameter: either integer exact size OR "reverse" (see CollectionSizeToBoolConverter). Shows when size matches.
- CountToVisibilityConverter – Parameter: comparison expression like ">0", "==0", may append H to use Hidden (e.g. ">0H").
- ValueCompareVisibilityConverter – Shows when numeric > 0 (Hidden when <=0).
- VisibilityFromNumberConverter – Parameter: integer threshold; Visible when value >= threshold.
- VisibilityFromNumberEqualsConverter – Visible when value == parameter.
- ZeroToVisibilityConverter – Visible when value == 0.

### Collection Size / Count
- CollectionSizeToBoolConverter – Parameter: integer required size OR "reverse". True when count equals size (default 0).
- CountToBooleanConverter – Parameter: comparison (>,>=,<,<=,==,!=) e.g. ">0" (default >0 if omitted).
  ```xaml
  <CheckBox IsChecked="{Binding Items, Converter={StaticResource CountToBooleanConverter}, ConverterParameter='>5'}" Content=">5 Items"/>
  ```

### Text & Formatting
- ToUpperConverter / ToLowerConverter – Case conversion.
- MultiStringFormatConverter – MultiBinding; parameter is composite format string: "{0} - {1:0.00} ({2})".
  ```xaml
  <TextBlock>
    <TextBlock.Text>
      <MultiBinding Converter="{StaticResource MultiStringFormatConverter}" ConverterParameter="{0} - {1:0.0} ({2})">
        <Binding Path="Name"/>
        <Binding Path="Price"/>
        <Binding Path="Code"/>
      </MultiBinding>
    </TextBlock.Text>
  </TextBlock>
  ```
- NullCoalesceConverter – If bound value is null/empty string returns ConverterParameter (fallback).
  ```xaml
  <TextBlock Text="{Binding Description, Converter={StaticResource NullCoalesceConverter}, ConverterParameter='(none)'}"/>
  ```
- BoolToStringTickCrossConverter – true => "P", false => "O" (style with a symbol font if desired).

### Colors / Brushes
- BackgroundColorToBwForegroundConverter – Input background hex (#RRGGBB) -> White or Black foreground for readability.
- HexStringToColorConverter – Accepts `#RRGGBB` or `RRGGBB` returns Color.
- HexStringToSolidColorBrushConverter – Same but returns SolidColorBrush.
- ColorToBrushConverter – Hex/String Color -> SolidColorBrush.
- BrushToColorConverter – Brush/Color -> Color (or Red fallback).
- FallbackBrushConverter – Brush or Color; fallback is Red.
- ValueToBrushConverter – Parameter optional tokens: contains "BackPressure" or "Pressure" to choose palette; default DarkGreen if value>0 else LightYellow.
- ValueCompareVisibilityConverter (see Visibility) – also for numeric state color with Hidden.
- LineStrokeLevelConverter – Parameter: "low-high" (e.g. "50-80"). Returns Lime (<low), Yellow (>=low), Red (>=high).
- ValueGtXConverter – Rounds numeric (see above) – helpful for numeric UI formatting.

### Numeric / Layout
- IntToThicknessConverter – Optionally sets specific sides via instance variants (BottomOnly, LeftOnly, etc.) when using registry or custom resource. Parameter may be a base Thickness.
  ```xaml
  <Border Padding="{Binding Spacer, Converter={StaticResource IntToThicknessConverter}, ConverterParameter='4,2,4,2'}"/>
  ```
- ThicknessUniformConverter – Parameter tokens control sides: L,R,T,B or H (Left+Right), V (Top+Bottom). Empty => all sides.
  ```xaml
  <Border Padding="{Binding Gap, Converter={StaticResource ThicknessUniformConverter}, ConverterParameter=HV}"/>
  ```
- MultiplierConverter / PercentageConverter – see Arithmetic section.
- InvertSignConverter / InvertValueConverter – Negate numeric; InvertSignConverter coerces to target type.
- DoubleToCurrencyStringConverter – Formats double/decimal to currency using current culture.
- ValueCompareVisibilityConverter – Numeric <=0 hidden else visible.
- InverseValueToBoolConverter – Numeric <=0 => true.

### Type / Object Info
- ObjectToTypeNameConverter – Parameter: "full" for full type name.
- ObjectEqualsParameterConverter / EqualityConverter / ComparisonConverter – See Boolean section.
- EnumConverter – Parameter: Type (x:Type) to parse a string value to enum.
  ```xaml
  <ComboBox SelectedValue="{Binding SelectedStatus}" SelectedValuePath="Tag">
    <ComboBoxItem Content="Open" Tag="{Binding 'Open', Converter={StaticResource EnumConverter}, ConverterParameter={x:Type local:TicketStatus}}"/>
  </ComboBox>
  ```

### Chaining Converters
Use MultiConverter to pipe the output of one into the next:
```xaml
<converters:MultiConverter x:Key="InvertVisibilityChain">
  <converters:BoolNegationConverter/>
  <converters:BoolToVisibilityConverter/>
</converters:MultiConverter>
<TextBlock Visibility="{Binding IsClosed, Converter={StaticResource InvertVisibilityChain}}"/>
```

### MultiBinding Boolean Examples
```xaml
<!-- Show only when all are true -->
<TextBlock Text="Ready" Visibility="{Binding Path=., Converter={x:Static c:ConvertersRegistry.BoolToVisibility}}">
  <TextBlock.Visibility>
    <MultiBinding Converter="{x:Static c:ConvertersRegistry.And}">
      <Binding Path="IsLoaded"/>
      <Binding Path="IsValid"/>
      <Binding Path="HasPermission"/>
    </MultiBinding>
  </TextBlock.Visibility>
</TextBlock>
```

---
## Error & Edge Notes
- Many converters throw if value types mismatch (fail fast). Ensure proper binding types.
- MathConverter caches parsed expressions internally.
- Comparison expressions (ComparisonConverter / CountToBooleanConverter) ignore malformed parameters and return default semantics.
- Some visibility converters support Hidden vs Collapsed selection via parameter tokens (H / hidden).

---
## Minimal Example Window
```xaml
<Window ... xmlns:conv="https://github.com/ChrisPulman/XamlConverters">
  <Window.Resources>
    <conv:ConvertersDictionary />
  </Window.Resources>
  <StackPanel Margin="20" DataContext="{Binding SampleViewModelInstance}">
    <TextBlock Text="{Binding Title, Converter={StaticResource ToUpperConverter}}"/>
    <TextBlock Text="{Binding Amount, Converter={StaticResource DoubleToCurrencyStringConverter}}"/>
    <ProgressBar Value="{Binding Progress}" Maximum="100" Height="20"/>
    <TextBlock Text="{Binding Progress, Converter={StaticResource ArithmeticConverter}, ConverterParameter='*2'}"/>
    <TextBlock Foreground="{Binding HexColour, Converter={StaticResource BackgroundColorToBwForegroundConverter}}"
               Background="{Binding HexColour, Converter={StaticResource HexStringToSolidColorBrushConverter}}"
               Text="Contrast Demo" Padding="8"/>
    <CheckBox Content="Is Active" IsChecked="{Binding IsActive}"/>
    <TextBlock Text="Active" Visibility="{Binding IsActive, Converter={StaticResource BoolToVisibilityConverter}}"/>
  </StackPanel>
</Window>
```

---
## Contributing
PRs welcome. Please keep converters small, focused, documented and unit-tested.

---
## License
MIT

---

**XamlConverters** - By Chris Pulman - Empowering Industrial Automation with Reactive Technology ⚡🏭
