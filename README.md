# Quantities
A library to safely handle various types of quantities, typically physical quantities.

## Project Goals
Dealing with quantities (Metre, Yard, etc.) is not trivial. There are many areas where things can go wrong, such as forgetting to convert from one unit to the next or converting the wrong way.
This library set's out to remove that burden from developers and API designers alike.

### A Generic API 
This is primarily a project that lets me explore how far one can push C#'s generics in an API. The goal is to create an api where generics apply naturally and enhance readability.
On the flip side, some implementation details in this library are plain out scary and weird, but heaps of fun to explore.

### Why Physical Quantities?
Using physical quantities as test subject seemed appropriate, as there are a limited number of units and SI-prefixes. Using generics, these prefixes and units can be combined neatly to create all sorts of representations of quantities. The generic constraints then allow for the API to restrict the prefixes and units to a subset that actually make sense on a particular quantity.
A concrete example helps to illustrate that point: A length may be represented in the SI-unit of metres or imperial units feet, but not with a unit that is used to represent time. Furthermore, it is standard usage to use the SI-units with prefixes, such as "Kilo" or "Milli", but not on imperial units. Hence, the generic constraints are set accordingly.

## Should I Use It?
It's a library that is still evolving rapidly. Try at your own risk or - even better - contribute :-)
