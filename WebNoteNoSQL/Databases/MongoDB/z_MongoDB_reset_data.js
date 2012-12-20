db.Notes.drop();
db.Categories.drop();

db.Notes.save(
  {
    "Title" : "Testeintrag",
    "Message" : "Ein gruener Postit",
    "Added" : new Date(2012, 05, 13),
    "Categories" : [
    
      { "Name" : "Normal Importance", "Color" : "green" },
      { "Name" : "Private", "Color" : "gray" }
      
    ]  
  });

db.Notes.save(
  {
    "Title" : "Testeintrag 2",
    "Message" : "Ein roter Postit",
    "Added" : new Date(2012, 05, 14),
    "Categories" : [
    
      { "Name" : "High Importance", "Color" : "red" }
      
    ]  
  });

db.Notes.save(
  {
    "Title" : "Testeintrag 3",
    "Message" : "Ein privater Postit",
    "Added" : new Date(2012, 05, 14),
    "Categories" : [
    
      { "Name" : "Private", "Color" : "gray" }
      
    ]  
  });

db.Categories.save(
  {
    "Name" : "Normal Importance",
    "Color" : "green"  
  });
  
db.Categories.save(
  {
    "Name" : "High Importance",
    "Color" : "red"  
  });
  
db.Categories.save(
  {
    "Name" : "Private",
    "Color" : "gray"  
  });