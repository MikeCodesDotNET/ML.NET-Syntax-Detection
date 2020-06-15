const express = require('express');
 
const app = express();
 
const port = 3000;
 
const axios = require('axios');
 
app.get('/recipe/:fooditem', async (req, res) => {
 try {
   const fooditem = req.params.fooditem;
   const recipe = await axios.get(`http://www.recipepuppy.com/api/?q=${fooditem}`); 
    return res.status(200).send({
     error: false,
     data: recipe.data.results
   });
  
 } catch (error) {
     console.log(error)
 }
});
 
app.listen(port, () => {
 console.log(`Server running on port ${port}`);
});
 
 
module.exports = app;