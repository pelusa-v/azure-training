using crud_practice;
using crud_practice.Models;

var uri = "https://practicecosmos1.documents.azure.com:443/";
var key = "HrMWoJ7e9fUp3bpK7QUNedTGVQF2HagA1uXFMDiFywYGtL2KDBxIkp2Tmu1fy0PuY7WrBbNZBmI0ACDbMu7E9Q==";
var cosmos = new AzureCosmosUtils(uri, key);

// await cosmos.CreateDatabase("crud_practice1");
// await cosmos.CreateContainer("crud_practice1", "documents", "/topic");

var item = new Document
{
    Id = "3",
    Name = "Algebra 2",
    Available = true,
    Topic = "Math"
};
// await cosmos.CreateItem("crud_practice1", "documents", item);
// await cosmos.ExecuteSP("crud_practice1", "documents", "createDocumentStoredProc", item.Topic, item);