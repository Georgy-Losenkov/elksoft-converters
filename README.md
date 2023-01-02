# Elksoft.Converters
This library contains classes and interfaces suitable for setting up converters in the DI manner.

## Usage
```Csharp
    IConverterFinder finder = DefaultConverterFinder.Create(useCheckedConversions: true);
    IConverter<String, DayOfWeek> converter = finder.FindConverter<String, DayOfWeek>();
    String sundayString = converter.Convert(DayOfWeek.Sunday, CultureInfo.InvariantCulture);
    String mondayString = converter.Convert(DayOfWeek.Monday, CultureInfo.InvariantCulture);
```

## Introduction

Library defines the following converters:
- from any type to its' base types (base class and any implemented interface)
- from `Object` to any type (`Object` is treated as `VARIANT`)
- from value type to its' nullable type and vice verse
- between primitive types. Those are
  - binary types: `Byte[]` and `Guid`
  - date/time types: `DateTime`, `DateTimeOffset`, `TimeSpan`, `DateOnly`, `TimeOnly`
  - numeric types such as `Boolean`, `Byte`, `Char`, `Int32`, etc. and all enums
  - `String`
- between user-defined type and any suitable type using implicit/explicit conversion operators defined in the type. 
  Since we usually define conversion operators only to/from few other types library extrapolates those operators
  to all suitable types.<br/>
  Suppose you defined operator `public static implicit operator SampleStruct(Int16 value)`.<br/>
  Then library will provide you with convertes from any numeric type to `SampleStruct`

## Culture specific conversions
Since convertions from `String` and to `String` are culture specific,
library forces you to specify `CultureInfo` when converting values.

## Reference type conversions
`null` is special value for reference types. And your conversion operator may either accept it or reject it.
In order to construct proper converters library have to know your decision. To inform library that `null` is
prohibited value for your conversion operator you have to mark your operator using `RejectsNullAttribute`.

## Notes
- Method `ToString()` is assumed as conversion operator only for primitive types since they reverse operation as well.
- Library uses reflection to construct converter, but created converters do not use reflection except `Object → TOut`
  converter that uses reflection to construct inner converters `TIn → TOut`.
- Library does not use boxing/unboxing inside converters except special cases like
  - `Object → struct → TOut`
  - `struct → Object`
  - boxing/unboxing inside user defined conversion operators