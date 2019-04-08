# Exploring Sequences in F#

### Or "A Study in Lazy vs. Eager Evaluation"

In a previous post I broke down how Lists are implemented in F#. 

In this post I'd like to talk about a different, more general, type of collection, the Sequence.

We'll start by breaking down the definition from the [Microsoft docs](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/sequences):

The first line says: "*A sequence is a logical series of elements all of one type.*" This is pretty standard *collection* talk, letting us know that we cannot mix and match types within a sequence, aside from the word "*logical*" being thrown in there.. I checked the documentation for the other types and didn't find that word thrown.. Interesting..

The next part says: "*Sequences are particularly useful when you have a large, ordered collection of data but do not necessarily expect to use all the elements. Individual sequences are computed only as required, so a sequence can provide better performance than a list in situations in situations where not all elements are used.*" 

Let's look at an example of this, by creating a list and then creating a sequence:
```fsharp
let aList = [for i in 1..100 do if i % 2 = 0 then yield i]
//=> val aList : int list =
//  [2; 4; 6; 8; 10; 12; 14; 16; 18; 20; 22; 24; 26; 28; 30; 32; 34; 36; 38; 40;
//   42; 44; 46; 48; 50; 52; 54; 56; 58; 60; 62; 64; 66; 68; 70; 72; 74; 76; 78;
//   80; 82; 84; 86; 88; 90; 92; 94; 96; 98; 100]

let aSeq  = seq {for i in 1..100 do if i % 2 = 0 then yield i}
//=> val aSeq : seq<int>
```
What's interesting to me here is the return values. After entering the line for `simpleList` FSI returns the signature for the value and the data, while the output for `simpleSeq` just shows a signature. Where is the data? 

What we are seeing here is eager evaluation (list) vs. lazy evaluation (sequence).

Let me try and explain the difference between these two with an *extremely* carefully crafted metaphor. 

### *A Tale of Two McDonald's*
It's Monday morning and you are on your way to work. You feel your stomach gurgling and decide that coffee alone isn't gonna cut it. Luckily, you see a McDonald's about 50 yards up the road. You plan to go through the drive-thru but as you pull in you see that the line is about 10 cars deep, so you park and go inside to order. Unfortunately, the line inside is more of the same, aside from the grumbling of voice's instead of mufflers. 
After a minute or two the manager comes out and says, "I apologize but as you can see we are a bit backed up. It'll be at least 15 minutes until we get any more biscuits ready." 
One of the other people in line say out loud, "You LAZY bum! I've been coming to this McDonald's three times a week for the last 8 years and Monday morning is ALWAYS super crowded!" 
"Yea, what a dummy", you say to appease the other grumpy people in line. You then walk out to your car and go to the next McDonald's, where you get in and out in about 4 minutes. 

***FAST-FORWARD to weekend***

It's 9:45 Saturday morning and you just do not feel like cooking, but your stomach demands sustenance. You remember the "Southern Style Chicken Biscuit" you got earlier that week at McDonald's and your mouth starts to water. You drive past the first McDonald's that you come across, remembering the chaos that ensued last time. You proceed on to the next one and pull up to the drive-thru window. 
"I'll have one Southern Style Chicken Biscuit please", you politely say to the metal box. 
"Oh, I'm sorry", the box replies, "we are out of biscuits this morning". 
With a sigh, you say "No problem, so you guys have been pretty busy this morning?"
"No, actually, our manager got really EAGER and had us prepare all of the biscuits at 4 AM, but we just didn't have many people show up this morning. We ended up having to throw most of them out."
You are sickened by the thought of all those buttery, delicious biscuits, *wasted*. 
You pull out and head back to the first McDonald's, where after a few minutes you chomp down on a fresh, fluffy, golden biscuit. 




// TYPE DEFINITION
```fsharp
type seq<'T> = System.Collections.Generic.IEnumerable<'T>
```