using crud_practice;
using crud_practice.Models;

var uri = "";
var key = "";
var cosmos = new AzureCosmosCrud(uri, key);

// await cosmos.CreateDatabase("crud_practice1");
// await cosmos.CreateContainer("crud_practice1", "documents", "/topic");

var item = new Document
{
    Id = "1",
    Name = "Calculus",
    Available = true,
    Topic = "Math"
};
await cosmos.CreateItem("crud_practice1", "documents", item);