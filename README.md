

1. [Introduction](#introduction)
2. [Application Domain](#app-domain)

## Introduction <a name="introduction"></a>
This is a Personality test application that allows users to see if they are an introvert or an extrovert. 
The test is dynamically configured. Multiple tests can be added and the application is not limited to Personality tests only, 
any psychology tests can be added. 

The application is built using .Net Core 6 and Entity Framework Core with an in-memory database.

##Application Domain <a name="introduction"></a>

###Test Score Calculation


###TestTemplate
At the core of the application domain is the `TestTemplate` entity. As the name suggests, this represents a template
for the actual tests. This contains the `Title`, `Description` and a list of possible answers and possible results of the 
test.

| Property    |  Type  |                       Description |
|-------------|:------:|----------------------------------|
| Title       | string |            The title of the test. |
| Description | string |  A short description of the test. |
| Questions   |  List  |          A list of all questions. |
|PossibleResults|  List  | The list of all possible results. |

###TestQuestion
The `TestQuestion` contains beside the `Title` a `Weight`. This is used for computing the test result, 
and represents how much does the question weight in the score calculation.

| Property |  Type   |                                                                 Description |
|----------|:-------:|----------------------------------------------------------------------------|
| Title    | string  |                                                  The title of the question. |
| Weight   | decimal | Represents how much does the question weight in the test score calculation. |
|MaxScore | decimal |                                           The maximum score of all answers. |
| Answers  |  List   |                                    A list of all question possible answers. |

###TestAnswer
The `TestAnswer` is composed of the answer `Content` and the answer `Score` which will is used in calculating the test result.

| Property |  Type  |                                                                               Description |
|----------|:------:|------------------------------------------------------------------------------------------|
| Content  | string |                                                                 The content of the aswer. |
| Score    |  int   | The score of the answer. This will be used in the test calculation to compute the result. |

###PossibleTestResult
Represents a possible result of the test. It contains the `Name` and `Description` of the result, but also the `MinScore`
and the `MaxScore` that are used to choose the result for the test from the list of all possible results.

##Architecture

##Project structure

##Future development