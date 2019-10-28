## Pointers 

A pointer is an integer value that represents a memory address. 

The simplest pointer that we can make in C++ is a `void pointer`. 
```cpp
int var = 8;
void *myPtr = &var;
```
Here, `myPtr` is a pointer variable that stores the memory address of our int variable `var`.

Void pointers are the purest form of pointers, they can point to anywhere in memory, regardless of the type of variable that it is pointing to. Types are meaningless as it pertains to void pointers.

Although pointers only represent a memory address, we can *dereference* a pointer to read or write to the value stored in the memory that our pointer is pointing to. We can do so like this:
```cpp
*(int*)myPtr = 8;
// Cast the pointer to the `int *` type and then dereference it. 
``` 
Because our myPtr variable is of type `void *`, in order to read from or write to our `myPtr` variable, we need to cast it to the appropriate type (which is `int *` in this case) and then dereference it using the `*` operator. 

If we try to access the memory of `myPtr` without casting it to the `int *` type then we will get an error, because the `void` type does not let the compiler know anything about the data being stored in that memory, all in knows is that `myPtr` points to a memory address. 

Void pointers are very useful in a lot of cases, but in order to access the values stored at the memory address that the void pointer variables points to, it must be type-cast to an appropriate type.  

If we go back to our original example, rather than create a void pointer, we could instead create an integer pointer. 
```cpp
int var = 8;
int *myPtr = &8;

// Now to dereference that value stored at the memory address that myPtr is pointing to, we do not need to cast. 
*myPtr = 10;
``` 



