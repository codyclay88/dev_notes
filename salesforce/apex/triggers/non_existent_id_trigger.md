### "The Non-Existent ID"

#### This is what NOT to do!

```apex
trigger NonExistentId on Case (before insert) {
    for (Case myCase : Trigger.new) {
        CaseComment cc = new CaseComment();
        cc.CommentBody = 'Case received by Agent';
        cc.ParentID    = myCase.Id;
        insert cc;
    }
}
```

This is a big no-no because we are trying to access the 'Id' field on the myCase object, which will not exist at this point because we are using a "before insert" trigger in this case. 

We can fix this by using the "after insert" trigger, where we will be able to access the Id of the newly inserted Case. 