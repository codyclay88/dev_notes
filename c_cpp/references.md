## References

References are very similar to pointers, the way the computer interprets references and pointers are very similar, but the way we semantically use them is different. 

For example, to create a pointer to an `int` variable, we would do this:
```cpp
int a = 5;
int *aPtr = &a;
```
To create a reference to an `int` variables, we would do this:
```cpp
int b = 5;
int& bRef = b;
```
You can think of a reference as an *alias*, it doesn't really create a new variable, it insteads provides a means to access the value stored in `b` as though it was `b`. 

To change the values of both `aPtr` (which is a pointer) and `bRef` (which is a reference) to 6, we would do the following:
```cpp
// aPtr is a pointer
*aPtr = 6;

// bPtr is a reference
bRef = 6;
```
Notice that we don't have to do any kind of dereferencing with `bRef` like we do for `aPtr`. We can treat `bRef` exactly like we would `b`. 



