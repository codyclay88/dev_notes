### "The Infinite Looping Trigger"

#### This is what NOT to do!

```apex
trigger Infinity on Opportunity (after update) {
    for (Opportunity opp : Trigger.new) {
        opp.Amount = 100;
        update opp;
    }
}
```

Note that this is an "after update" trigger that also explicitly saves the opp at the end of the trigger. 

This code will run over and over and over, because we are updating a record within a loop and then updating the record, which then triggers an update loop and then updates it again in the trigger, and so on and so forth. 

To fix this, we should have instead used the "before update" event, and we can now remove the explicit update command.