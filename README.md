# Ason

Ason stands for Azrael's Simple Object Notation. It is a simple data format that is easy to read and write for humans. It is inspired by JSON, but it is not a subset of JSON. It is designed to be a data format that is easy to read and write for humans, and easy to parse for computers.

```txt
{ "Normal" };
str -> "String": "String";
integer -> "Integer": 2;
flt -> "Float": 3.14;
bool -> "Boolean": True;
null -> "Null": null;
{ "Array" };
str[] -> "StringArray": ["String1","String2","String3"];
int[] -> "IntegerArray": [1, 2, 3];
flt[] -> "FloatArray": [1.1, 2.2, 3.3];
bool[] -> "BooleanArray": [True, False, True];
```

```txt
Section - Normal
String: String
Float: 3.14
Null:
Section - Array
String Array: "String1", "String2", "String3"
Integer Array: 1, 2, 3
Float Array: 1.1, 2.2, 3.3
Boolean Array: True, False, True
```
