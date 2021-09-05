
I used as a means of parsing to/from JSON easily and uniformly when sending data over websockets to a webserver running Node. This made handling the data easier on both sides.

The need for this was to perform a number of actions depending on the event name and process payload as necessary but was unable to deserialize without knowing the type to deserialize it. Unable to simply deserialize as 2 strings to then pick the correct object I decided to encode the payload JSON and pass that. You can then decode and serialize to the desired Class type. 

I'm sure there are other methods that are more slick but this has worked nicely for me.
