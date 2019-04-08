
open System
open System.Text.RegularExpressions

// Needs to be constrained to between 0 and 120
type Age = Age of int  // this is a "single case union"

// Obviously needs constrained to match a certain regex
type SSN = SSN of string  // this is a "single case union"

type FirstLastName = private {  
    FirstName: string
    LastName: string
}

type FullName = private {
    FirstName: string
    MiddleNames: string list
    LastName: string
}

type SymbolName = private {
    Name: string
}

type Name = 
| FirstLast of FirstLastName
| Full of FullName 
| Symbol of SymbolName 
with
    override this.ToString() =
        match this with
        | FirstLast name -> name.FirstName + " " + name.LastName
        | Symbol name -> name.Name
        | Full name ->
            let middleNames = List.reduce (fun x y -> x + " " + y + " ") name.MiddleNames 
            name.FirstName + middleNames + " " + name.LastName


        

// Person is an Entity type, so we want to compare equality via the SSN,
// not by matching each of the properties of the person
[<NoEquality;NoComparison>]
type Person = {
    SSN: SSN
    Name: Name
    Age: Age
}

type PresentStudent =
| Started of Name
| Waiting of Name 
| Called of Name 
| Ready of Name
| Staged of Name 
| Loaded of Name

type Student = 
| Absent of Name
| Present of PresentStudent

[<RequireQualifiedAccess>]
module Student =
    // Students are Absent until proven present
    let init name =
        Absent name

    let markPresent student =
        match student with
        | Absent name       -> Present (Waiting name)
        | student           -> student 

    let markAbsent student = 
        match student with
        | Absent _                  -> student
        | Present (Started name)    -> Absent name
        | Present (Waiting name)    -> Absent name
        | Present (Called name)     -> Absent name
        | Present (Ready name)      -> Absent name
        | Present (Staged name)     -> Absent name
        | Present (Loaded name)     -> Absent name

    let forwardToWaiting student =
        match student with
        | Absent _                  -> student
        | Present (Started name)    -> Present (Waiting name)
        | Present (Waiting _)       -> student
        | _                         -> student            

[<RequireQualifiedAccess>]   
module Name =
    let parse (name: string) =
        let elements = name.Split(' ')
        match elements with
        | [||] -> Error "Name cannot be empty"
        | [|h|] -> Ok (Symbol { Name = h })
        | names when names.Length = 2 -> 
             Ok (FirstLast { FirstName = names.[0]; LastName = names.[1] })
        | names -> 
            Ok (Full { 
                FirstName = names.[0]; 
                MiddleNames = List.ofArray names.[1..(names.Length - 2)]; 
                LastName = names.[names.Length - 1] })

    let value (name: Name) : string = name.ToString()       

module Age = 
    // this is a "smart constructor" 
    let create (age: int) = 
        if age < 0 then
            Error "Age cannot be negative!"        
        else if age > 120 then
            Error "I doubt this is true..."
        else 
            Ok (Age age)    

    // this is more of a "destructor" to get the int value out of an Age instance
    let value (Age age) = age     

module SSN =
    // This is an example of an "Active Pattern"
    // This example allows us to use SSN in pattern matching 
    // as seen in the create method below
    let (|SSNRegex|_|) pattern input =
        let m = Regex.Match(input, pattern)
        if(m.Success) then 
            Some m.Groups.[1].Value
        else None         


    let ssnRegex = @"^(\d{3}-?\d{2}-?\d{4}|XXX-XX-XXXX)$"

    // this is a "smart constructor"
    let create (ssn: string) = 
        match ssn with
        | SSNRegex ssnRegex value -> Ok (SSN value)
        | _ -> Error "This is not a valid SSN" 

    // Destucturer...    
    let value (SSN ssn) = ssn

[<RequireQualifiedAccess>]
module Result =
    let map = Result.map
    let mapError = Result.mapError
    let bind = Result.bind

module Testing =  
    let names = [
        "Kristian Cody Clay"
        "Hayley Belle Clay"
        "Beyonce"
        "Shmitty Werber Men Jensen"
        "Owen Clay"
    ]    


    let studentInitMapped input = input |> Result.map Student.init    

    let me = 
        names.[0]
        |> Name.parse
        |> studentInitMapped
        

