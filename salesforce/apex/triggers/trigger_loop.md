## What is the trigger loop?

```apex
for (Lead l : Trigger.new) {
    // We're inside the trigger loop
    // Your main trigger logic will always be inside here
    // Every trigger uses a trigger loop
}
```

All triggers will use this trigger loop. 

`Trigger.new` is the list of all records entering a trigger. 

`Lead` is the type of record that we'll be iterating on. 

We use a trigger loop because in many cases records will be edited in bulk. 