<br/>
<div align="center">
  <img src="resources/project-logo.png" width="110" height="110" alt="Project Logo"/>
</div>
<br/>

# Syntax Detection [ML.NET](https://dotnet.microsoft.com/learn/ml-dotnet/get-started-tutorial/intro?WT.mc_id=personal-blog-mijam) Sample
[![GitHub license](https://img.shields.io/github/license/sultan99/react-on-lambda.svg)](https://github.com/sultan99/react-on-lambda/blob/master/LICENSE)
 
 > A example project demonstating how to use ML.NET to detect different programming languages from small snippets.
<br/>

This is a demo project can identify various programming language syntax with minimal input. It's reporting approximately 70% accuracy having been trained on a modest amount of training data (~1800 snippets).

![screenshot](resources/mlscreenshot.png)

## Supported Syntax
* C#
* CSV
* DockerFiles 
* Go
* HTML
* Java
* JavaScript
* JSON
* Kotlin
* Python
* Ruby
* Rust
* Bash 
* SQL
* Swift
* TypeScript
* YAML

## Getting started

If you want to run this yourself then you'll need to want Visual Studio's model builder to the merged.csv file found in the /resources/Data directory. 

### Prerequisites
* [Visual Studio 2019 16.6.1 or later](https://www.visualstudio.com/thank-you-downloading-visual-studio/?sku=community&rel=16&WT.mc_id=ml.netsyntaxdetection-github-mijam)
* [Model Builder VS Extension](https://marketplace.visualstudio.com/items?itemName=MLNET.07&WT.mc_id=ml.netsyntaxdetection-github-mijam)

**Test input**
You can modify the [test input](https://github.com/MikeCodesDotNET/ML.NET-Syntax-Detection/blob/9e12eee9744fd55d649acd79cc4c36b8c579f84e/LanguageDetectionMLML.ConsoleApp/Program.cs#L16) in the ConsoleApp for quick testing. 
```cs
//Passes in a JS snippet.
ModelInput sampleData = new ModelInput()
    {
        Content = "'const arr = [\"This\", \"Little\", \"Piggy\"]; const first = arr.pop(); console.log(first);')"
    };
```
**Add more training data**

The training data is organised by into directories named after the syntax contained within. I've written a [small console app](https://github.com/MikeCodesDotNET/ML.NET-Syntax-Detection/blob/9e12eee9744fd55d649acd79cc4c36b8c579f84e/TrainingDataSetBuilder/Program.cs#L9) which will use the directry>snippet.txt structure to produce a CSV file that can be used with ML.NET model builder. Once it's generated a new merged.csv file, you can retrain and see if you've improved the accuracy. 

## Learn More 
* [Announcement Blog post](https://devblogs.microsoft.com/dotnet/ml-net-model-builder-is-now-a-part-of-visual-studio/?WT.mc_id=ml.netsyntaxdetection-github-mijam)
* [Model Builder Repo](https://github.com/dotnet/machinelearning-modelbuilder)
* [ML.NET Customer Showcase](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet/customers/scancam?WT.mc_id=ml.netsyntaxdetection-github-mijam)


## Feedback 
Any questions or suggestions?

You are welcome to discuss in an [Issue](https://github.com/MikeCodesDotNET/ML.NET-Syntax-Detection/issues/new?body=**Feature%20Suggestion:**%0A%20I%20woud%20love%20to%20see..%20%0A%0A%0A---%0A%20) or [Tweet](https://twitter.com/mikecodesdotnet) at me. 

