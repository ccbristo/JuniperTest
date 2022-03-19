# JuniperTest
This is my coding test result for Juniper.

A few notes on my approach -

I wanted to keep things as simple and as close to the current problem as I could. The requirements mentioned that the tax service would need to decide which calculator to use based on the customer "in the future", so I provided the order to the TaxService, but did not put any logic around it.

I wanted to use this test as an opportunity to play with a few ideas.

First, I NSubstitute instead of my tried and true mocking library, Moq. NSubstitute was easy to adjust to and did it's job well. I'd defintely consider it again in the future.

Second, I wanted to experiment with reducing primitive obsession, so instead of using 'float' to return back the tax percentage and amounts, I created Money and Percentage types. If I were writing genuine production code, these classes would need to be more fully developed - obvious things like comparisons and basic mathematic operators are not included yet.

Third, I used XUnit as my test library. It has some excellent features like the [Theory] attribute and its companions, e.g. [InlineData] that made adding a lot of very similar test cases a breeze.

I see a lot of opportunity for improvement, especially in the testing area. More tests could be added for cases where the HttpClient throws an exception when communicating with TaxJar. Also, the fluent interface for stubbing the response from a particular endpoint could use some polish. With some additional features, I think this is something that could make a useful Nuget library to ease the burden of testing HTTP calls.

Thanks for giving me the opportunity to do this. I appreciate any feedback you have for me.
