#Vooban.Freshbooks#
A simple, easy to use, strongly type, C# client for the **Freshbooks** API. This project is built on top of [HastyAPI.Freshbooks](https://github.com/lukesampson/HastyAPI.FreshBooks) used to communicate with [Freshbooks](http://www.freshbooks.com) cloud accounting system.

## Goals ##
The goal for us is to ease developpement using Freshbooks for .NET users, while providing an API that can be used easily.

## Why ##
We are creating this because **HastyAPI.Freshbooks** is great, but you must have the [Freshbooks API](http://developers.freshbooks.com/) by your side in order to use it. Moreover, HastyAPI.Freshbooks uses dynamic objects that reflects 1:1 the xml content of the **Freshbooks API**, which might not always be ideal as **Freshbooks** uses XML, and therefore, there is a lot of uncessary code to do on the dynamic object each time we want to integrate with Freshbooks.

## Exemple ##

### Basic API ###
The basic API returns results wrapped inside a Freshbooks response object that you can inspect and work with.

#### This is a very basic exemple ####
```
var freshbooks = new Freshbooks("username", "token");

var testedClass = new StaffApi(freshbooks);

var currentStaff = testedClass.GetCurrent();

// TODO : Work with the strongly typed StaffModel object
```

#### Getting list of items ####
This methods allows you to get a list of items from Freshbooks. This methods supports paging, so you can passing the page number and number of items per page you want to get. In the following example, we're asking for the first page, with 25 items per page.

```
var freshbooks = new Freshbooks("username", "token");
var testedClass = new StaffApi(freshbooks);
FreshbooksPagedResponse<StaffModel> result = testedClass.GetList(1, 25);

if(result.Success)
{
	var totalItems = result.TotalItems;
	var totalPages = result.TotalPages;
	var itemPerPages = result.ItemsPerPage;
	var currentPage = result.Page;

	var currentPageItems = result.Result;
	// Iterate over the results
	foreach(var staff in result)
		// TODO: Your code here
}
```

#### Getting all pages at once ####
```
var freshbooks = new Freshbooks("username", "token");
var testedClass = new StaffApi(freshbooks);
IEnumerable<StaffModel> result = testedClass.GetAllPages();

// Iterate over the results
foreach(var staff in result)
	// TODO: Your code here
```
