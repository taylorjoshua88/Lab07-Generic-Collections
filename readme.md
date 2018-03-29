# GenericCollections

**Author**: Joshua Taylor
**Version**: 1.0.0

## Overview

GenericCollections demonstrates the creation of a generic collection class. Our
demo collection represents a deck of cards, and it is able to double its
internal array's capacity as new cards are added past its initial capacity.
Similarly, the Deck collection will automatically shrink its internal array's
capacity when cards are removed to the point that there are fewer cards than
one third of the internal array's capacity.

Additionally, Deck collections can be shuffled randomly in their ordering.
Note that as generic collections, Deck objects can hold types other than Deck
by providing a different type when they are being declared and initialized.
Value and reference types are supported.

## Getting Started

GenericCollections targets the .NET Core 2.0 platform. The .NET Core 2.0 SDK can
be downloaded from the following URL for Windows, Linux, and macOS:

https://www.microsoft.com/net/download/

The dotnet CLI utility would then be used to build and run the application:

    cd GenericCollections
    dotnet build
    dotnet run

Additionally, users can build, run, and perform unit testing using Visual
Studio 2017 or greater by opening the solution file at the root of this
repository. All testing is performed using [xUnit](https://xunit.github.io/),
which is included in the test project as a NuGet dependency.

## Example

#### Deck Instantiation Screenshot ####
![Instantiation Screenshot](/assets/instanceScreenshot.JPG)
#### Shuffling Deck Screenshot ####
![Shuffling Screenshot](/assets/shufflingScreenshot.JPG)

## Architecture

GenericCollections is composed of two classes, Deck<T> and Card. The generic
collection being demonstrated is Deck<T> with Card acting as an example of
reference type data to be added, removed, and shuffled within the Deck<T>
collection.

### Deck<T>

Deck<T> is a generic collection based on an array with a default capacity
of 10. This initial capacity can be changed by passing an argument to the
Deck<T> constructor to be prevent unnecessary resizing of the internal
array when the caller has some idea of the amount of data Deck<T> should hold
ahead of time.

Deck<T> provides a Length property which contains a running count of the
number of items which has been added to the collection. In addition, a
Capacity property returns the length of the internal array used by this
collection and represents the number of items that can be added to the
collection before the internal array must be doubled in capacity.

Adding items will automatically trigger a doubling of the internal array's
capacity when attempting to insert items past the current capacity of the
internal array. Item removal that would cause the length of the collection
to be less than one third of the internal array's capacity will automatically
trigger a halving of the internal array's capacity.

Deck implements the generic GetEnumerator() method and the IEnumerable<T>
interface, providing the capability to use collection initialization and
foreach loops with the Deck class.

### Card

Card represents a single playing card from the standard 52-card French playing
card deck (minus the Joker cards). The card's suit is stored as a byte-based
enumeration with values 0 - 3 represented by the labels Clubs, Spades, Hearts,
and Diamonds (respectively). The card's rank is also representing by a
byte-based enumeration with labels Ace, Two, Three, Four, Five, Six, Seven,
Eight, Nine, Ten, Jack, Queen, and King representing values 1-13.

Card implements the IEquatable<Card> interface allowing the use of Card.Equals()
to compare Card objects by value and to create hashes.

### Data Model

All data is stored in memory on the heap by instantiating classes using the
*new* keyword and by storing references through interfaces. Deck<T> collections
store items inside of an internally-managed array of type T. No data persistence is supported by this application.

### Command Line Interface (CLI)

GenericCollections uses a command line interface to demonstrate the
instantiation of Deck<T> collections, adding items to the collection, removing
items from the collection, and shuffling the items' ordering within the
collection. Simple Console.ReadKey() calls provide pauses throughout the
demonstration to provide the user with a chance to read the program's output.

## Change Log

* 3.28.2018 [Joshua Taylor](mailto:taylor.joshua88@gmail.com) - Initial
release. All tests are passing.
