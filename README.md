# XLSTAT Ready to run library
XLSTAT Ready to run library is a small .NET library written in C#, to create Microsoft Excel files in the XLSM format with a XLSTAT preconfigured analyse.  
The file generated with the library contains a **Start** sheet which has a button to launch the preconfigured XLSTAT analysis. It also has sheet for data.  
XLSTAT is available on www.xlstat.com.  

## Requirements
XLSTAT Ready to tun is based on .NET 5.0 and use ClosedXML library (https://github.com/ClosedXML/ClosedXML).  
Visual Studio 2019 or higher is required.

## How to use it?
To integrate XLSTAT Ready to run features you need to add a reference to the project into your project.  
XLSTAT Ready to run use a generic data structure which contains a vector of labels and a matrix of data.  
Definition of the main data structure: 

    public class Data<T>
    {
        public string[] Labels { get; set; }        /*Label vector*/
        public T[][] Table { get; set; }            /*Data matrix*/
    }

Then, you need to configure your analysis by calling one of the method available.  

    //Example to initialize a linear regression
    XLSTAT.Models.Functionalities.REG analyze = new();

Then, you need to setup the compulsory data (Y and X for the linear regression).

    analyze.Y = new XLSTAT.Models.Data<double>();
    analyze.X = new XLSTAT.Models.Data<double>();

Here, you can fill your data set by your own data.  
After the analyse configuration, you just need to call the function to get back your xlsm file in *base64* format.  

    XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(analyze);

Finally, you can convert the result to the xlsm file.

A full example of a linear regression is available into samples.

## Features list
### Linear regression
Linear regression is, without doubt, one of the most frequently used statistical modeling methods.  

Model:  

    XLSTAT.Models.Functionalities.REG

Required data:  
* **Y**: quantitative dependent variables
* **X**: quantitative explanatory variables

Optional data:  
* **ObsLabels**: observation labels
* **W**: observation weights
* **Wr**: regression weights


### ANOVA
Use this model to carry out ANOVA (ANalysis Of VAriance) of one or more balanced or unbalanced factors. The advanced options enable you to choose the constraints on the model and to take account of interactions between the factors. Multiple comparison tests can be calculated.  

Model:  

    XLSTAT.Models.Functionalities.ANO

Required data:  
* **Y**: quantitative dependent variables
* **Q**: qualitative explanatory variables

Optional data:  
* **ObsLabels**: observation labels
* **W**: observation weights
* **Wr**: regression weights
* **Interaction**: interactions / level

### ANCOVA
Use this module to model a quantitative dependent variable by using quantitative and qualitative dependent variables as part of a linear model. 

Model:  

    XLSTAT.Models.Functionalities.ANC

Required data:  
* **Y**: quantitative dependent variables
* **X**: quantitative explanatory variables
* **Q**: qualitative explanatory variables

Optional data:  
* **ObsLabels**: observation labels
* **W**: observation weights
* **Wr**: regression weights
* **Interaction**: interactions / level

### External Preference Mapping (PREFMAP)
Use External Preference Mapping (PREFMAP) to model and represent graphically the preference of assessors for a series of objects depending on objective criteria or linear combinations of criteria.

Model:  

    XLSTAT.Models.Functionalities.PFM

Required data:  
* **Y**: preference data  
The table contains the various objects (products) studied in the rows and the assessors in the columns. This is reversed in transposed mode. If column headers have been selected, check that the "Variable labels" option has been activated.
* **X**: configuration  
Select the data corresponding to the objective descriptors or to a 2- or 3-dimensional configuration if a method has already been used to generate the configuration. If column headers have been selected, check that the "Variable labels" option has been activated.

Optional data:  
* **ObsLabels**: observation labels

### Descriptive statistics
Use this tool to calculate descriptive statistics.

Model:  

    XLSTAT.Models.Functionalities.UNI

Required data (minimum one of the following fields):  
* **X**: quantitative data  
* **Q**: qualitative data  
* **W**: weights  
* **G**: subsamples  

### PLS regression
Use this module to model and predict the values of one or more dependant quantitative or qualitative variables using a linear combination of one or more explanatory quantitative and/or qualitative variables, without facing the constraints of OLS (ordinary least square regression) on the number of variables versus the number of observations.

Model:  

    XLSTAT.Models.Functionalities.PLS

Required data:  
* **Y**: quantitative dependent variables
* **X**: quantitative explanatory variables
* **Q**: qualitative explanatory variables

*Both X and Q are not required*

Optional data:  
* **ObsLabels**: observation labels
* **W**: observation weights

### Principal Component Analysis (PCA)
Use Principle Component Analysis to analyze a quantitative observations/variables table or a correlation or covariance matrix. 

Model:  

    XLSTAT.Models.Functionalities.PCA

Required data:  
* **Y**: observations/variables table

Optional data:  
* **ObsLabels**: observation labels
* **W**: observation weights

## Samples
### CliSample
This sample generates a **Template.xlsm** for a linear regression where data is randomly generated.  
It mainly shows how to use and integrate XLSTAT Ready to run library into a C# project.

### SampleAPI
This project build an API which exposes XLSTAT Ready to run library functionnalities.  
A swagger is also available to test and see the API.
