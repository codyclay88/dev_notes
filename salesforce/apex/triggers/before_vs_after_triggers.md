### When do we use "before" vs. "after" triggers?


Pros of Before Triggers:
- No need to explicitly save your work, the save event is coming

Cons of Before Triggers:
- System level fields are not available, they have yet to be populated by the database

Pros of After Triggers:
- System fields now available:
    - Record ID (insert)
    - Created Date (insert)
    - Last Modified Date (insert)

### The RECORD ID system field is the important delineation, if you need to access the Record ID in an insert trigger, you need to use the "after" trigger

Cons of After Triggers:
- You have to explicitly save your changes
- Potentially create infinite loops

### When in doubt, use a "before" trigger
