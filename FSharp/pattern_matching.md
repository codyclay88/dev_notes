
Pattern matching examples:


// Pattern Matching over a list: from "Get Programming With F#" by Issac Abraham
```fsharp
let summary =
    match playerNames with
    | ["Joe"; _ ] -> "Two items, first is Joe"
    | first :: _ when first.Length = 2 -> 
        "The first entry has a two letter name!"
    | ["Joe", "Joe", "Joe"] -> "All Joe!"
    | _ -> "Other people"
```