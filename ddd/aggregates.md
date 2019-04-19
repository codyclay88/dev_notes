An aggregate is a cluster of associated objects that we treat as a unit for the purpose of data changes. 

Aggregates are composed of an aggregate root and child components. The aggregate root maintains these children and prohibits external sources from modifying them directly. If an external source could modify a child of the aggregate root, then we cannot prove that the integrity of the aggregate is still valid. 

You cannot interact with a component of an aggregate without going through the aggregate root. 

Two aggregates can reference each other by communicating through their aggregate roots. 

Avoid bi-directional relationships from within an aggregate. The root should be able to reference a child component directly, but allowing a child component to directly access it's root can lead to accidental complexity. 

Aggregates are also convenient for persistence concerns, saving our aggregate root to the database should also save the state of all the child components. 