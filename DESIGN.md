<img width="100%" src="nuru-logo.png" alt="nuru dot net"> 

This project is going through it's fourth re-iteration on the design. After learning about pitfalls as I went along I have been rethinking my approach and as a result have been throwing the baby out with the bathwater. This time around I will also be thinking more about the design up front. 

# Design diagrams

## Image

So as I am starting over, I toyed a bit with the idea of the image. Previously I had no way to differentiate between ANSI4, ANSI8 and TrueColor for example. Also the previous implementation had no good way of supporting keys. This time around I am placing the burden of accessing the correct color methods on the library user. Any ideas on how to improve on this is greatly appreciated, though. 

![Class diagram](http://www.plantuml.com/plantuml/proxy?cache=no&src=https://github.com/SauceChord/nuru-dot-net/raw/redesign/puml/image1.puml)

Further looking at IO, adding a simple layer that reads and writes data in nurus format with respect to big endian and string lengths of 7 looks like this:

![Class diagram](http://www.plantuml.com/plantuml/proxy?cache=no&src=https://github.com/SauceChord/nuru-dot-net/raw/redesign/puml/format1.puml)
