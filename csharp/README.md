# Tell Don't Ask
A kata for refactoring legacy code, focused on violating the Tell, Don't Ask principle and the anemic domain model.

## Instructions
Here, you will find a simple order flow application. It is capable of creating orders, performing some calculations (totals and taxes), and managing them (approval/rejection and shipping).

The previous development team did not take the time to build a proper domain model. Instead, they preferred a procedural style, leading to this anemic domain model. Fortunately, at least they took the time to write unit tests for the code.

Your new CTO, after experiencing many errors caused by this application, has asked you to refactor the code to make it easier to maintain and more reliable.

## Focus Areas
As the title of the kata suggests, the focus is, of course, on the Tell, Don't Ask principle. You should aim to remove all setters that shift behavior into domain objects.

### Disclaimer
This kata was suggested by the Software Craftsmanship group in Barcelona and was originally written for Java. I have taken the liberty of translating the instructions and adapting it to my natural language, C#.

You can find the original kata at: https://github.com/gabrieletondi/tell-dont-ask-kata

Since C# does not have a built-in BigDecimal class, I have used the third-party library dmath:
https://github.com/deveel/deveel-math








