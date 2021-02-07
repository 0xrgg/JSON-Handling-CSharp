
I used as a means of parsing to/from JSON easily and uniformly when sending data over websockets to a webserver running Node. This made handling the data easier on both sides.

My scenario was needing to perform a number of actions depending on the event name and process was necessary but was unable to deserialize without knowing what the type was to deserialize it. Unable to simply deserialize as 2 strings to then pick the correct object I decided to encode the entire JSON string and pass that, which will allow you to deserialize to two strings. You can then decode and serialize to the desired Class type. 

I'm sure there are other methods that are more slick but this has worked nicely when testing dummy data back and forth. 
