- [Introduction <a name="introduction"></a>](#introduction--a-name--introduction----a-)
- [Application Domain <a name="introduction"></a>](#application-domain--a-name--introduction----a-)
    * [Test Score Calculation](#test-score-calculation)
    * [TestTemplate](#testtemplate)
    * [TestQuestion](#testquestion)
    * [TestAnswer](#testanswer)
    * [PossibleTestResult](#possibletestresult)
- [Architecture](#architecture)
- [Project structure](#project-structure)
- [Future development](#future-development)

## Introduction <a name="introduction"></a>
This is a Personality test application that allows users to see if they are an introvert or an extrovert. 
The test is dynamically configured. Multiple tests can be added and the application is not limited to Personality tests only, 
any psychology tests can be added. 

The application is built using .Net Core 6 and Entity Framework Core with an in-memory database.

## Application Domain <a name="introduction"></a>

### Test Score Calculation
The score of the test is calculated using the Weighted Sum Method: https://www.geeksforgeeks.org/weighted-sum-method-multi-criteria-decision-making/.

Every possible answer for all questions has a score configured which is an `int`, for example if we want to test
if the person is an introvert or an extrovert, then a low score meas that the person is an introvert, and
a high score means the person is an extrovert. The user can select only one anwer for every question,
and the score of the selected answer will be used in the calculation.

The score of the test is a sum of the individual score of each question. Each question has a weight, this represents how much the score of that specific question weights in the final
test score. This means that some questions can be more important than the others, and the system administrator 
can choose which one is which.

Before the test score is computed all the results must be normalized. This will ensure us that if a question
has 5 possible answers and one has only 3, this will not influence the result of the test. The normalization is
done by dividing the score of each question by the maximum score possible for that question.

Then the weighted score of the question is computed by multiplying the question score by the weight of the question. Then the final test
score is computed by adding the weighted scores for all questions.

The tests has configured a list of possible results. Every possible result has a `MinScore` and a `MaxScore`. The test score is compared
with these values to select the test result that corresponds to the computed score.

### TestTemplate
At the core of the application domain is the `TestTemplate` entity. As the name suggests, this represents a template
for the actual tests. This contains the `Title`, `Description` and a list of possible answers and possible results of the 
test.

| Property    |  Type  |                       Description |
|-------------|:------:|----------------------------------|
| Title       | string |            The title of the test. |
| Description | string |  A short description of the test. |
| Questions   |  List  |          A list of all questions. |
|PossibleResults|  List  | The list of all possible results. |

### TestQuestion
The `TestQuestion` contains beside the `Title` a `Weight`. This is used for computing the test result, 
and represents how much does the question weight in the score calculation.

| Property |  Type   | Description                                                                 |
|----------|:-------:|-----------------------------------------------------------------------------|
| Title    | string  | The title of the question.                                                  |
| Weight   | decimal | Represents how much does the question weights in the test score calculation. |
|MaxScore | decimal | The maximum score of all answers. This is used for normalization.                    |
| Answers  |  List   | A list of all question possible answers.                                    |

### TestAnswer
The `TestAnswer` is composed of the answer `Content` and the answer `Score` which will is used in calculating the test result.

| Property |  Type  |                                                                               Description |
|----------|:------:|------------------------------------------------------------------------------------------|
| Content  | string |                                                                 The content of the aswer. |
| Score    |  int   | The score of the answer. This will be used in the test calculation to compute the result. |

### PossibleTestResult
Represents a possible result of the test. It contains the `Name` and `Description` of the result, but also the `MinScore`
and the `MaxScore` that are used to choose the result for the test from the list of all possible results.

## Architecture
The application is based on the `Clean Architecture` pattern (also known as `Onion Architecture`). This means that the application is made in
layers and each layer has it's own responsibility. 

![onion-architecture](https://user-images.githubusercontent.com/7803254/182377344-d9f9352d-bdc8-478a-8034-7e776314a531.png)

At the core of the application are the `Domain` and the `Application` layers. The
domain contains business model and types and the application layer contains application specific business rules.
The `Infrastructure` layer is responsible for implementing all the IO operations or external services integration.
The most important thing is that all the dependencies are towards the center, so nothing in the inner layer knows anything about something in an outer layer.

## Project structure

```
|--src
  |--Domain
  |--Application
  |--Infrastructure
  |--WebAPI
  |--WebApp
|--tests
  |--Application.IntegrationTests
  |--Application.UnitTests
  |--Domain.UnitTests
  
  
```

The project is structured based on the architecture diagram with some small changes. We have one project for `Domain`, `Application` and `Infrastructure`.
For simplicity purposes the `Persistence` is in the same project as `Infrastructure`. The `Presentation` is splitted into two
projects, we have one project for the `WebAPI` and one project for the `WebApp` which is the React frontend application.

## Future development
1. Add user authentication
2. Add user auditing for entities
3. Add proper logging
