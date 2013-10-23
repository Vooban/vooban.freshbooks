#Vooban.Freshbooks#
A simple, easy to use, strongly type, C# client for the **Freshbooks** API. This project is built on top of [HastyAPI.Freshbooks](https://github.com/lukesampson/HastyAPI.FreshBooks) used to communicate with [Freshbooks](http://www.freshbooks.com) cloud accounting system.

## Goals ##
The goal for us is to ease developpement using Freshbooks for .NET users, while providing an API that can be used easily.

## Why ##
We are creating this because **HastyAPI.Freshbooks** is great, but you must have the [Freshbooks API](http://developers.freshbooks.com/) by your side in order to use it. Moreover, HastyAPI.Freshbooks uses dynamic objects that reflects 1:1 the xml content of the **Freshbooks API**, which might not always be ideal as **Freshbooks** uses XML, and therefore, there is a lot of uncessary code to do on the dynamic object each time we want to integrate with Freshbooks.

## Dependency Injection Friendly ##
The library is build to be dependecy injection friendly and does include a [UnityContainerExtension](http://msdn.microsoft.com/en-us/library/microsoft.practices.unity.unitycontainerextension%28v=pandp.30%29.aspx) that you can make use of.

## Exemple ##

### Basic API ###
The basic API returns results wrapped inside a Freshbooks response object that you can inspect and work with.

#### This is a very basic exemple without any DI support ####
```
var freshbooks = new Freshbooks("username", "token");

var testedClass = new StaffApi(freshbooks);

var currentStaffResponse = testedClass.CallGetCurrent();
if(currentStaffResponse.Status)
{
    var currentStaff = currentStaffResponse.Result;
    ...
}
```

#### This is an exemple with DI support ####
If you make use of **Dependency Injection** then there is no need to wire the Freshbooks clients yourselves, just setup your **username** [FreshbooksUsername] and **token** [FreshbooksApiToken] in the appSettings section of your app.config and you are good to go.
```
var testedClass = Container.Resolve<StaffApi>();

var currentStaffResponse = testedClass.CallGetCurrent();
if(currentStaffResponse.Status)
{
    var currentStaff = currentStaffResponse.Result;
    ...
}
```

### Simplified API ###
The simplified API classes returns Models instead of Freshbooks responses and throws exception when the Freshbooks API fails.

#### This is a very basic exemple without any DI support ####
```
var freshbooks = new Freshbooks("username", "token");

var testedClass = new SimplifiedStaffApi(freshbooks);

var currentStaff = testedClass.Current;
var specificStaff = testedClass.Get("1234);
var allStaff = testedClass.GetAll();
```

#### This is an exemple with DI support ####
If you make use of **Dependency Injection** then there is no need to wire the Freshbooks clients yourselves, just setup your **username** [FreshbooksUsername] and **token** [FreshbooksApiToken] in the appSettings section of your app.config and you are good to go.
```
var testedClass = Container.Resolve<StaffApi>();

var currentStaff = testedClass.Current;
var specificStaff = testedClass.Get("1234);
var allStaff = testedClass.GetAll();

```

