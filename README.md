# XLSTAT Ready to run library
XLSTAT Ready to run library is a small .NET library written in C#, to create Microsoft Excel files in the XLSM format with an XLSTAT preconfigured analysis.  
The file generated with the library contains a **Start** sheet which displays a button to launch the preconfigured XLSTAT analysis. It also has a **Data** sheet that contains the exported data.
XLSTAT is available at www.xlstat.com.

## Requirements
XLSTAT Ready to run is based on the .NET Framework 4.7.2 and uses the ClosedXML library (https://github.com/ClosedXML/ClosedXML).  
Visual Studio 2019 or higher is required.

## How to use it?
To integrate XLSTAT Ready to run features you need to add a reference to the library into your project or install the nuget package.  
XLSTAT Ready to run uses a generic data structure which contains a vector of labels and a matrix of data.  
Definition (C#) of the main data structure: 

    public class Data<T>
    {
        public string[] Labels { get; set; }        /*Label vector*/
        public T[][] Table { get; set; }            /*Data matrix*/
    }

Then, you need to configure your analysis by calling one of the available methods.  

    //Example to initialize a linear regression
    XLSTAT.Models.Functionalities.REG analyze = new();

Then, you need to setup the compulsory data fields (Y and X the linear regression).

    analyze.Y = new XLSTAT.Models.Data<double>();
    analyze.X = new XLSTAT.Models.Data<double>();

Here, you can fill in your own data.

After the configuration of the analysis, you only need to call the function to retrieve your xlsm file in *base64* format.  

    XLSTAT.Utilitties.Result<string> result = XLSTAT.ReadyToRun.GenerateXLSTATFile(analysis);

Finally, you can convert the result into an xlsm file.

A full example of a linear regression is available in the *Samples* section below.

*You can hide the dialog box when the user runs the generated file by setting the following variable to true:* **hideUserForm**

## Features list

### ANCOVA
Use this module to model a quantitative dependent variable by using quantitative and qualitative dependent variables as part of a linear model. 

Model:  

    XLSTAT.Models.Functionalities.ANC

Required data:  
* **Y**: quantitative dependent variable(s)
* **X**: quantitative explanatory variable(s)
* **Q**: qualitative explanatory variables

Optional data:  
* **ObsLabels**: observation labels
* **W**: observation weights
* **Wr**: regression weights
* **Interaction**: interactions / level

### ANOVA
Use this model to carry out ANOVA (ANalysis Of VAriance) on one or more balanced or unbalanced factors. The advanced options enable you to choose the constraints on the model and to take into account interactions between the factors. Multiple comparison tests can be calculated.  

Model:  

    XLSTAT.Models.Functionalities.ANO

Required data:  
* **Y**: quantitative dependent variable(s)
* **Q**: qualitative explanatory variable(s)

Optional data:  
* **ObsLabels**: observation labels
* **W**: observation weights
* **Wr**: regression weights
* **Interaction**: interactions / level

### CATA data analysis

Use this function to analyse CATA (check-all-that-apply) data quickly and efficiently. If the CATA survey includes preference data, this tool can be used to identify drivers of liking or on the opposite, attributes that consumers consider as negative.

Model:  

    XLSTAT.Models.Functionalities.CATA

Required data:  
* **T**: the CATA data (0/1)
* **P**: the data corresponding to the tested products
* **J**: the data corresponding to the assessors

Optional data:  
* **S**: Preference data
* **Ideal**: Activate this option if the assessors have qualified an ideal product, and specify how the ideal product is named in the Products field

### Descriptive statistics
Use this tool to calculate descriptive statistics.

Model:  

    XLSTAT.Models.Functionalities.UNI

Required data (minimum one of the following fields):  
* **X**: quantitative data  
* **Q**: qualitative data  
* **W**: weights  
* **G**: subsamples  

### External Preference Mapping (PREFMAP)
Use External Preference Mapping (PREFMAP) to model and graphically represent the preference of assessors for a series of objects depending on objective criteria or linear combinations of criteria.

Model:  

    XLSTAT.Models.Functionalities.PFM

Required data:  
* **Y**: preference data  
The table contains the various objects (products) studied in the rows, and the assessors in the columns. This is reversed in transposed mode.

* **X**: configuration  
Select the data corresponding to the objective descriptors or to a 2- or 3-dimensional configuration if a method has already been used to generate the configuration.

Optional data:  
* **ObsLabels**: observation labels

### Free sorting

Use this function to analyze free sorting data in a quick and efficient way.

Model:  

    XLSTAT.Models.Functionalities.FST

Required data:  
* **T**: the different assessors

Optional data:  
* **ObsLabels**: observation labels
* **Method**: Set the value to one of the following option:
    * 0: activate this option if you want to use the STATIS method after an adapted pre-processing
    * 1: on co-occurrence matrix: Activate this option if you want to use a Correspondence Analysis on the product co-occurrence matrix
    * 2: Activate this option if you want to use Multiple Correspondence Analysis on raw data

### Internal Preference Mapping

Use this function to analyze the ratings given on P products by J assessors (consumers, experts, …).

Model:  

    XLSTAT.Models.Functionalities.IPM

Required data:  
* **T**: the quantitative data corresponding to P products described by J assessors

Optional data:  
* **ObsLabels**: observation labels
* **Type**: Set the value to one of the following option to choose the PCA type:
    * 0: correlation (normalized PCA)
    * 1: covariance (non normalized PCA)
    * 2: PCA on a Spearman correlation matrix


### Linear regression

Linear regression is one of the most frequently used statistical modeling methods.  

Model:  

    XLSTAT.Models.Functionalities.REG

Required data:  
* **Y**: quantitative dependent variable(s)
* **X**: quantitative explanatory variable(s)

Optional data:  
* **ObsLabels**: observation labels
* **W**: observation weights
* **Wr**: regression weights

### Multiple Factor Analysis (MFA)

Use the Multiple Factor Analysis (MFA) to simultaneously analyze several tables of variables,
and to obtain results, particularly charts, that allow to study the relationship between the
observations, the variables and the tables. Within a table, the variables must be of the same
type (quantitative table, qualitative table or frequency table), but the tables can be of different
types.

Model:  

    XLSTAT.Models.Functionalities.MFA

Required data:  
* **Data**: Data<Data<string>> of observations/variables tables. Data<Data<string>>::Labels contains tables of labels.

Optional data:  
* **IsFrequency**: true if **Data** are frequency tables
* **ObsLabels**: observation labels
* **W**: observation weights

### PLS regression
Use this module to model and predict the values of one or more dependant quantitative or qualitative variables using a linear combination of one or more explanatory quantitative and/or qualitative variables, without being subject to the constraints of OLS (ordinary least square regression) on the number of variables versus the number of observations.

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

Use Principal Component Analysis to analyze a quantitative observations/variables table or a correlation or covariance matrix. 

Model:  

    XLSTAT.Models.Functionalities.PCA

Required data:  
* **Y**: observations/variables table

Optional data:  
* **ObsLabels**: observation labels
* **W**: observation weights
    
### Projective mapping

The projective mapping (or Napping) task is one of the so-called "rapid" tests that are becoming
increasingly popular in the context of the sensory characterization of products. It consists in
asking each subject to place products on a sheet of paper. The data collected are simply the
coordinates of the products on the x-axis and y-axis of the sheet of paper. Each subject brings a
table with rows (one per product) and columns. These data can be analyzed with the
STATIS method or with Multiple Factor Analysis (MFA). While both methods have the primary
objective of synthesizing information to graphically represent the products, they also allow for
the determination of relationships between the subjects' answers.


Model:  

    XLSTAT.Models.Functionalities.PRM

Required data:  
* **T**: the data that correspond to the different assessors

Optional data:  
* **ObjLabels**: product labels
* **AssesLabels**: subject labels for the display of results
* **Method**: Set the value to one of the following option:
    * 0: activate this option if you want to use the STATIS method
    * 1: activate this option if you want to use Multiple Factor Analysis

## Others exports
### How to generate a simple xlsx file?
The function *GenerateDataFile* returns a xlsx file encode in base64.

### How to generate a simple csv file?
The function *GenerateCSVFile* returns a csv file stored in a string.

## Samples

### LinearRegression
This sample generates a **Template.xlsm** for a linear regression where data is randomly generated.  
It mainly shows how to use and integrate the XLSTAT Ready to Run library into a C# project.

### API
This project builds an API which exposes XLSTAT Ready to run library functionalities.  
A swagger is also available to test and observe the API.
