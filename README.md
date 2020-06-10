# Syntax Detection ML.NET Sample
 
This is a demo project, showing how to I used ML.NET to identify various programming language syntax. It's reporting approximately 70% accuracy having been trained on a using a modest amount of training data of ~1800 snippets.

![screenshot](mlscreenshot.png)

### Supported Syntax
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

### How to use 
If you want to add more training data, then you'll need to copy and paste individual code snippets into the correct syntax directory. Once you've done this, delete the merged.csv file and run the TrainingDataSetBuilder console app. 

The console app will generate a new csv file which can be passed to ML.NET for further training. 
