
open System
open System.IO

type Data = {
    COND: float
    DEPTH: float
    BAT: int
    SPEC_COND: float
    TEMP: float
    pH: float
}

type Packet = {
    ts: int64
    data: Data
}

module DataConverter =
    let readLines (filePath: string) = seq {
        use reader = new StreamReader(filePath)
        while not reader.EndOfStream do
            yield reader.ReadLine()
    }  

    let convertLineToData (line: string) =
        let elements = line.Split [|','|]  
        {
            COND = float elements.[1]
            DEPTH = float elements.[2]
            BAT = int elements.[3]
            SPEC_COND = float elements.[4]
            TEMP = float elements.[5]
            pH = float elements.[6]
        }
    
    
            

           